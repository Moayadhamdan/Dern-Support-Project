namespace DernSupport_BackEnd.Models
{
    public class TechnicianTask
    {
        public int TechnicianTaskId { get; set; }
        public string userId { get; set; }
        public ApplicationUser applicationUser { get; set; }
        public string UserName { get; set; }
        public int TechnicianId { get; set; }
        public Technician technician {  get; set; } 

        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
