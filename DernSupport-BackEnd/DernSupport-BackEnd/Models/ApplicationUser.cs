using Microsoft.AspNetCore.Identity;

namespace DernSupport_BackEnd.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string AccountType { get; set; }
        public ICollection<TechnicianTask> technicianTasks { get; set; }  // An employee can have many tasks

    }
}
