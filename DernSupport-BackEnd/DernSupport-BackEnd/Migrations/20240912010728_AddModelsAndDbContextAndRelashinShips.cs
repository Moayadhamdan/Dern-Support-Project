using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DernSupport_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddModelsAndDbContextAndRelashinShips : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccountType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobsDb",
                columns: table => new
                {
                    JobsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScheduledDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobsDb", x => x.JobsId);
                });

            migrationBuilder.CreateTable(
                name: "JobSparePartDb",
                columns: table => new
                {
                    JobSparePartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuantityInStock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSparePartDb", x => x.JobSparePartId);
                });

            migrationBuilder.CreateTable(
                name: "KnowledgeBases",
                columns: table => new
                {
                    KnowledgeBaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnowledgeBases", x => x.KnowledgeBaseId);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Budget = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hours = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                });

            migrationBuilder.CreateTable(
                name: "technician",
                columns: table => new
                {
                    TechnicianId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_technician", x => x.TechnicianId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TechnicianProjectsDBset",
                columns: table => new
                {
                    TechnicianId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicianProjectsDBset", x => new { x.ProjectId, x.TechnicianId });
                    table.ForeignKey(
                        name: "FK_TechnicianProjectsDBset_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TechnicianProjectsDBset_technician_TechnicianId",
                        column: x => x.TechnicianId,
                        principalTable: "technician",
                        principalColumn: "TechnicianId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TechnicianTasksDb",
                columns: table => new
                {
                    TechnicianTaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TechnicianId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicianTasksDb", x => x.TechnicianTaskId);
                    table.ForeignKey(
                        name: "FK_TechnicianTasksDb_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TechnicianTasksDb_technician_TechnicianId",
                        column: x => x.TechnicianId,
                        principalTable: "technician",
                        principalColumn: "TechnicianId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "admin", "00000000-0000-0000-0000-000000000000", "Admin", "ADMIN" },
                    { "user", "00000000-0000-0000-0000-000000000000", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "JobSparePartDb",
                columns: new[] { "JobSparePartId", "Category", "Description", "Name", "QuantityInStock" },
                values: new object[,]
                {
                    { 1, "GPU", "NVIDIA GeForce GTX 1650", "Graphics Card", 6 },
                    { 2, "Cooling", "Laptop Cooling Fan Assembly", "Cooling Fan", 7 },
                    { 3, "Storage", "DVD-RW Optical Drive", "Optical Drive", 5 },
                    { 4, "Input", "Laptop Touchpad Assembly", "Touchpad", 9 },
                    { 5, "Camera", "HD Webcam for Laptops", "Webcam", 11 },
                    { 6, "Power", "500W Power Supply Unit", "Power Supply", 8 },
                    { 7, "Enclosure", "Mid-Tower Computer Case", "Case", 13 },
                    { 8, "Accessory", "7-Port USB 3.0 Hub", "USB Hub", 14 }
                });

            migrationBuilder.InsertData(
                table: "JobsDb",
                columns: new[] { "JobsId", "Cost", "Description", "Priority", "ScheduledDate", "Title" },
                values: new object[,]
                {
                    { 1, "300$", "Perform a backup of the company’s critical data.", "High", "", "Data Backup" },
                    { 2, "150$", "Set up email accounts for new employees.", "High", "", "Email Configuration" },
                    { 3, "400$", "Update server operating system and software.", "Medium", "", "Server Maintenance" },
                    { 4, "90$", "Replace damaged network cables in office.", "Medium", "", "Network Cable Replacement" },
                    { 5, "500$", "Test the backup system to ensure it is functioning correctly.", "High", "", "Backup System Testing" },
                    { 6, "30$", "Update all software to the latest versions.", "Low", "", "Software Update" },
                    { 7, "350$", "Take inventory of all IT equipment in the office.", "Medium", "", "IT Equipment Inventory" }
                });

            migrationBuilder.InsertData(
                table: "KnowledgeBases",
                columns: new[] { "KnowledgeBaseId", "Category", "Description", "Title" },
                values: new object[,]
                {
                    { 1, "Hardware", "To install RAM, you must open your computer case, locate the RAM slots on the motherboard, and carefully insert the RAM sticks into the available slots. Ensure they are properly aligned before pressing them into place. After installation, close the case and restart your computer to check if the new RAM is recognized by the system.", "How do I install RAM in my computer?" },
                    { 2, "Software", "Configuring Windows Firewall involves managing inbound and outbound rules for applications. This allows you to block unwanted traffic and only permit trusted software to communicate over the network. It's important to ensure that you regularly review and adjust firewall settings to protect your system from unauthorized access.", "How do I configure Windows Firewall for better security?" },
                    { 3, "Hardware", "Replacing a hard drive requires removing the old drive from its bay, disconnecting the power and data cables, and installing a new one in its place. After installation, you’ll need to reinstall the operating system or restore your files from a backup. This process is crucial if you're upgrading to a larger drive or replacing a failed one.", "What is the process for replacing a hard drive?" },
                    { 4, "Software", "To install Microsoft Office, download the installer from Office.com or use an installation disc. After the setup process, you’ll need to sign in with your Microsoft account to activate the product. You can then customize settings for applications like Word, Excel, and PowerPoint based on your preferences and needs.", "How do I install and set up Microsoft Office?" },
                    { 5, "Hardware", "Troubleshooting printer issues involves checking for physical connection problems, verifying that the correct printer drivers are installed, and ensuring that there is enough ink or toner. Additional steps may include running diagnostic tests through the printer's software and resolving any network-related issues if the printer is connected wirelessly.", "How can I troubleshoot common printer issues?" },
                    { 6, "Software", "If you've forgotten your Windows password, you can reset it using a password reset disk, another administrator account, or by using Microsoft's online account recovery tool. In some cases, you may need to boot into Safe Mode to reset the password manually. It's important to ensure you have recovery options set up beforehand to avoid being locked out.", "How do I reset a forgotten Windows password?" },
                    { 7, "Software", "Updating drivers involves visiting the hardware manufacturer’s website, downloading the latest drivers, and installing them on your system. Alternatively, Windows Update can automatically download and install driver updates. Keeping drivers up to date ensures that your hardware components perform efficiently and remain compatible with the latest software.", "How do I update my computer's drivers?" },
                    { 8, "Hardware", "To clean dust from your computer, turn off the system and disconnect it from power. Open the case and use compressed air to blow dust from the fans, heat sinks, and other components. Be careful not to touch internal parts with your hands. Regular cleaning prevents overheating and helps your computer run more efficiently.", "How do I clean dust from my computer?" }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "ProjectId", "Budget", "Hours", "ProjectName" },
                values: new object[,]
                {
                    { 1, "5000 JD", 150.0, "Project A" },
                    { 2, "10000 JD", 180.0, "Project B" },
                    { 3, "12000 JD", 200.0, "Project C" }
                });

            migrationBuilder.InsertData(
                table: "technician",
                columns: new[] { "TechnicianId", "Email", "FirstName", "LastName", "Phone" },
                values: new object[,]
                {
                    { 1, "hamadanjo@gmail.com", "Moayad", "Hamdan", "1245698785" },
                    { 2, "aya@gmail.com", "Aya", "Al-Wahidi", "65489785425" }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { -1990583211, "permission", "Create", "admin" },
                    { -666972313, "permission", "Delete", "user" },
                    { 1580734, "permission", "Delete", "admin" }
                });

            migrationBuilder.InsertData(
                table: "TechnicianProjectsDBset",
                columns: new[] { "ProjectId", "TechnicianId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicianProjectsDBset_TechnicianId",
                table: "TechnicianProjectsDBset",
                column: "TechnicianId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicianTasksDb_TechnicianId",
                table: "TechnicianTasksDb",
                column: "TechnicianId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicianTasksDb_userId",
                table: "TechnicianTasksDb",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "JobsDb");

            migrationBuilder.DropTable(
                name: "JobSparePartDb");

            migrationBuilder.DropTable(
                name: "KnowledgeBases");

            migrationBuilder.DropTable(
                name: "TechnicianProjectsDBset");

            migrationBuilder.DropTable(
                name: "TechnicianTasksDb");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "technician");
        }
    }
}
