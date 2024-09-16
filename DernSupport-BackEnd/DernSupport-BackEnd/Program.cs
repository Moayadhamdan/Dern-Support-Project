using DernSupport_BackEnd.Data;
using DernSupport_BackEnd.Models;
using DernSupport_BackEnd.Repositories.Interfaces;
using DernSupport_BackEnd.Repositories.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace DernSupport_BackEnd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();

            // Get the connection string settings 
            string ConnectionStringVar = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<DernSupportDbContext>(optionsX => optionsX.UseSqlServer(ConnectionStringVar));

            // Add Identity Service
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<DernSupportDbContext>();

            // Add Scopes
            builder.Services.AddScoped<ITechnician, TechnicianService>();
            builder.Services.AddScoped<IUser, IdentitiUserService>();
            builder.Services.AddScoped<JwtTokenService>();

            // add auth service to the app using jwt
            builder.Services.AddAuthentication(
                options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
                ).AddJwtBearer(
                options =>
                {
                    options.TokenValidationParameters = JwtTokenService.ValidateToken(builder.Configuration);
                });


            // Configure Authorization with Claims
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Create", policy => policy.RequireClaim("Permission", "Create"));
                options.AddPolicy("Delete", policy => policy.RequireClaim("Permission", "Delete"));
            });


            // Add CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:3000")
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });

            //swagger configuration
            builder.Services.AddSwaggerGen
                (

                option =>
                {
                    option.SwaggerDoc("DernSupportApi", new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "DernSupport Api Doc",
                        Version = "v1",
                        Description = "Dern-Support to develop a solution to support its business operations and help it expand the services it offers"
                    });

                    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "Please enter user token below."
                    });

                    option.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            Array.Empty<string>()
                        }
                    });


                });




            var app = builder.Build();



            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("AllowReactApp");
            // call swagger service
            app.UseSwagger
                (
                options =>
                {
                    options.RouteTemplate = "api/{documentName}/swagger.json";
                }
                );

            // call swagger UI
            app.UseSwaggerUI
                (
                options =>
                {
                    options.SwaggerEndpoint("/api/DernSupportApi/swagger.json", "DernSupport Api");
                    options.RoutePrefix = "";
                }
                );



            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var exception = exceptionHandlerPathFeature?.Error;

                    context.Response.StatusCode = 500; // Internal Server Error by default
                    context.Response.ContentType = "application/json";

                    var response = new
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = "An unexpected error occurred.",
                        DetailedMessage = exception?.Message,
                        ExceptionType = exception?.GetType().Name
                    };

                    await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
                });
            });


            app.MapControllers();

            app.MapGet("/newpage", () => "Hello World! From Moayad Hamdan");

            app.Run();
        }
    }
}
