namespace DernSupport_BackEnd.Models
{
    public class Technician
    {
        public int TechnicianId { get; set; } // PK
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public ICollection<TechnicianProjects> technicianProjects { get; set; }

        public ICollection<TechnicianTask> technicianTasks { get; set; }  // An employee can have many tasks
    }
}
