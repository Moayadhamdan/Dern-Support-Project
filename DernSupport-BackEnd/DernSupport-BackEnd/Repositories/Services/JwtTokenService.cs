using DernSupport_BackEnd.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DernSupport_BackEnd.Repositories.Services
{
    public class JwtTokenService
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;
        public JwtTokenService(IConfiguration configuration , SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _signInManager = signInManager;
            _userManager = userManager;

        }


        public static TokenValidationParameters ValidateToken(IConfiguration configuration)
        {
            return new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = GetSecurityKey(configuration),
                ValidateIssuer = false,
                ValidateAudience = false
            };

        }


        private static SecurityKey GetSecurityKey(IConfiguration configuration)
        {
            var secretKey = configuration["JWT:SecretKey"];
            if (secretKey == null)
            {
                throw new InvalidOperationException("Jwt Secret key is not exsist");
            }

            var secretBytes = Encoding.UTF8.GetBytes(secretKey);

            return new SymmetricSecurityKey(secretBytes);
        }


        public async Task<string> GenerateToken(ApplicationUser user, TimeSpan expiryDate)
        {

            var userPrincliple =  await _signInManager.CreateUserPrincipalAsync(user);
            if (userPrincliple == null)
            {
                throw new InvalidOperationException("User m principal error.");
            }


            var claims = userPrincliple.Claims.ToList();
            var userPermissions = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userPermissions);

            var signInKey = GetSecurityKey(_configuration);
            if (signInKey == null)
            {
                throw new InvalidOperationException("Signing key cannot be null.");
            }
            var token = new JwtSecurityToken
                (
                expires: DateTime.UtcNow  +  expiryDate,
                signingCredentials: new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256),
                claims: claims
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
