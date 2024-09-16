using DernSupport_BackEnd.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DernSupport_BackEnd.Models;

namespace DernSupport_BackEnd.Data
{
    public class DernSupportDbContext : IdentityDbContext<ApplicationUser>
    {
        public DernSupportDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Technician> technician { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<TechnicianProjects> TechnicianProjectsDBset { get; set; }

        public DbSet<TechnicianTask> TechnicianTasksDb { get; set; }

        public DbSet<JobSparePart> JobSparePartDb { get; set; }

        public DbSet<Jobs> JobsDb { get; set; }
        public DbSet<KnowledgeBase> KnowledgeBases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TechnicianProjects>().HasKey(pk => new { pk.ProjectId, pk.TechnicianId });

            modelBuilder.Entity<TechnicianProjects>()
                .HasOne(ep => ep.Technician)
                .WithMany(e => e.technicianProjects)
                .HasForeignKey(e => e.TechnicianId);

            modelBuilder.Entity<TechnicianTask>()
                .HasOne(ep => ep.technician)
                .WithMany(e => e.technicianTasks)
                .HasForeignKey(e => e.TechnicianId);

            modelBuilder.Entity<TechnicianTask>()
                .HasOne(ep => ep.applicationUser)
                .WithMany(e => e.technicianTasks)
                .HasForeignKey(e => e.userId);

            modelBuilder.Entity<TechnicianProjects>()
                .HasOne(ep => ep.Project)
                .WithMany(e => e.technicianProjects)
                .HasForeignKey(e => e.ProjectId);

            modelBuilder.Entity<TechnicianProjects>().HasData(
                new TechnicianProjects { TechnicianId = 1, ProjectId = 1 },
                new TechnicianProjects { TechnicianId = 1, ProjectId = 2 },
                new TechnicianProjects { TechnicianId = 2, ProjectId = 1 }

                );


            modelBuilder.Entity<Technician>().HasData(
                new Technician { TechnicianId = 1, FirstName = "Moayad", LastName = "Hamdan", Email = "hamadanjo@gmail.com", Phone = "1245698785" },
                new Technician { TechnicianId = 2, FirstName = "Aya", LastName = "Al-Wahidi", Email = "aya@gmail.com", Phone = "65489785425" }

                );

            modelBuilder.Entity<Project>().HasData(
                new Project { ProjectId = 1, ProjectName = "Project A", Budget = "5000 JD", Hours = 150 },
                new Project { ProjectId = 2, ProjectName = "Project B", Budget = "10000 JD", Hours = 180 },
                new Project { ProjectId = 3, ProjectName = "Project C", Budget = "12000 JD", Hours = 200 }
                );

            modelBuilder.Entity<JobSparePart>().HasData(
                new JobSparePart
                {
                    JobSparePartId = 1,
                    Name = "Graphics Card",
                    Category = "GPU",
                    Description = "NVIDIA GeForce GTX 1650",
                    QuantityInStock = 6
                },
                new JobSparePart
                {
                    JobSparePartId = 2,
                    Name = "Cooling Fan",
                    Category = "Cooling",
                    Description = "Laptop Cooling Fan Assembly",
                    QuantityInStock = 7
                },
                new JobSparePart
                {
                    JobSparePartId = 3,
                    Name = "Optical Drive",
                    Category = "Storage",
                    Description = "DVD-RW Optical Drive",
                    QuantityInStock = 5
                },
                new JobSparePart
                {
                    JobSparePartId = 4,
                    Name = "Touchpad",
                    Category = "Input",
                    Description = "Laptop Touchpad Assembly",
                    QuantityInStock = 9
                },
                new JobSparePart
                {
                    JobSparePartId = 5,
                    Name = "Webcam",
                    Category = "Camera",
                    Description = "HD Webcam for Laptops",
                    QuantityInStock = 11
                },
                new JobSparePart
                {
                    JobSparePartId = 6,
                    Name = "Power Supply",
                    Category = "Power",
                    Description = "500W Power Supply Unit",
                    QuantityInStock = 8
                },
                new JobSparePart
                {
                    JobSparePartId = 7,
                    Name = "Case",
                    Category = "Enclosure",
                    Description = "Mid-Tower Computer Case",
                    QuantityInStock = 13
                },
                new JobSparePart
                {
                    JobSparePartId = 8,
                    Name = "USB Hub",
                    Category = "Accessory",
                    Description = "7-Port USB 3.0 Hub",
                    QuantityInStock = 14
                }
            );

            modelBuilder.Entity<Jobs>().HasData(
                new Jobs
                {
                    JobsId = 1,
                    Title = "Data Backup",
                    Description = "Perform a backup of the company’s critical data.",
                    ScheduledDate = "",
                    Priority = "High",
                    Cost = "300$"
                },

                new Jobs
                {
                    JobsId = 2,
                    Title = "Email Configuration",
                    Description = "Set up email accounts for new employees.",
                    ScheduledDate = "",
                    Priority = "High",
                    Cost = "150$"
                },

                new Jobs
                {
                    JobsId = 3,
                    Title = "Server Maintenance",
                    Description = "Update server operating system and software.",
                    ScheduledDate = "",
                    Priority = "Medium",
                    Cost = "400$"
                },

                new Jobs
                {
                    JobsId = 4,
                    Title = "Network Cable Replacement",
                    Description = "Replace damaged network cables in office.",
                    ScheduledDate = "",
                    Priority = "Medium",
                    Cost = "90$"
                },

                new Jobs
                {
                    JobsId = 5,
                    Title = "Backup System Testing",
                    Description = "Test the backup system to ensure it is functioning correctly.",
                    ScheduledDate = "",
                    Priority = "High",
                    Cost = "500$"
                },

                new Jobs
                {
                    JobsId = 6,
                    Title = "Software Update",
                    Description = "Update all software to the latest versions.",
                    ScheduledDate = "",
                    Priority = "Low",
                    Cost = "30$"
                },

                new Jobs
                {
                    JobsId = 7,
                    Title = "IT Equipment Inventory",
                    Description = "Take inventory of all IT equipment in the office.",
                    ScheduledDate = "",
                    Priority = "Medium",
                    Cost = "350$"
                }
            );

            modelBuilder.Entity<KnowledgeBase>().HasData(
                new KnowledgeBase
                {
                    KnowledgeBaseId = 1,
                    Title = "How do I install RAM in my computer?",
                    Description = "To install RAM, you must open your computer case, locate the RAM slots on the motherboard, and carefully insert the RAM sticks into the available slots. Ensure they are properly aligned before pressing them into place. After installation, close the case and restart your computer to check if the new RAM is recognized by the system.",
                    Category = "Hardware",
                },

                new KnowledgeBase
                {
                    KnowledgeBaseId = 2,
                    Title = "How do I configure Windows Firewall for better security?",
                    Description = "Configuring Windows Firewall involves managing inbound and outbound rules for applications. This allows you to block unwanted traffic and only permit trusted software to communicate over the network. It's important to ensure that you regularly review and adjust firewall settings to protect your system from unauthorized access.",
                    Category = "Software",
                },

                new KnowledgeBase
                {
                    KnowledgeBaseId = 3,
                    Title = "What is the process for replacing a hard drive?",
                    Description = "Replacing a hard drive requires removing the old drive from its bay, disconnecting the power and data cables, and installing a new one in its place. After installation, you’ll need to reinstall the operating system or restore your files from a backup. This process is crucial if you're upgrading to a larger drive or replacing a failed one.",
                    Category = "Hardware",
                },

                new KnowledgeBase
                {
                    KnowledgeBaseId = 4,
                    Title = "How do I install and set up Microsoft Office?",
                    Description = "To install Microsoft Office, download the installer from Office.com or use an installation disc. After the setup process, you’ll need to sign in with your Microsoft account to activate the product. You can then customize settings for applications like Word, Excel, and PowerPoint based on your preferences and needs.",
                    Category = "Software",
                },

                new KnowledgeBase
                {
                    KnowledgeBaseId = 5,
                    Title = "How can I troubleshoot common printer issues?",
                    Description = "Troubleshooting printer issues involves checking for physical connection problems, verifying that the correct printer drivers are installed, and ensuring that there is enough ink or toner. Additional steps may include running diagnostic tests through the printer's software and resolving any network-related issues if the printer is connected wirelessly.",
                    Category = "Hardware",
                },

                new KnowledgeBase
                {
                    KnowledgeBaseId = 6,
                    Title = "How do I reset a forgotten Windows password?",
                    Description = "If you've forgotten your Windows password, you can reset it using a password reset disk, another administrator account, or by using Microsoft's online account recovery tool. In some cases, you may need to boot into Safe Mode to reset the password manually. It's important to ensure you have recovery options set up beforehand to avoid being locked out.",
                    Category = "Software",
                },

                new KnowledgeBase
                {
                    KnowledgeBaseId = 7,
                    Title = "How do I update my computer's drivers?",
                    Description = "Updating drivers involves visiting the hardware manufacturer’s website, downloading the latest drivers, and installing them on your system. Alternatively, Windows Update can automatically download and install driver updates. Keeping drivers up to date ensures that your hardware components perform efficiently and remain compatible with the latest software.",
                    Category = "Software",
                },

                new KnowledgeBase
                {
                    KnowledgeBaseId = 8,
                    Title = "How do I clean dust from my computer?",
                    Description = "To clean dust from your computer, turn off the system and disconnect it from power. Open the case and use compressed air to blow dust from the fans, heat sinks, and other components. Be careful not to touch internal parts with your hands. Regular cleaning prevents overheating and helps your computer run more efficiently.",
                    Category = "Hardware",
                }
            );



            seedRoles(modelBuilder, "Admin", "Create", "Delete");
            seedRoles(modelBuilder, "User", "Delete");

        }

        private static void seedRoles(ModelBuilder modelBuilder, string roleName, params string[] permission)
        {
            var role = new IdentityRole
            {
                Id = roleName.ToLower(),
                Name = roleName,
                NormalizedName = roleName.ToUpper(),
                ConcurrencyStamp = Guid.Empty.ToString()
            };

            var claimRoles =
            permission.Select(
            permission => new IdentityRoleClaim<string>
            {
                Id = Guid.NewGuid().GetHashCode(),
                RoleId = role.Id,
                ClaimValue = permission,
                ClaimType = "permission"

            }).ToArray();


            modelBuilder.Entity<IdentityRole>().HasData(role);
            modelBuilder.Entity<IdentityRoleClaim<string>>().HasData(claimRoles);
        }
    }
}
