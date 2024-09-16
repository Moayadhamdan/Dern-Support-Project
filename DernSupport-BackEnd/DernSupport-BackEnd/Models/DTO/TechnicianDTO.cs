namespace DernSupport_BackEnd.Models.DTO
{
    public class TechnicianDTO
    {
        public int TechnicianTaskId { get; set; }
        public int TechnicianId { get; set; }
        public string userId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string UserName { get; set; } // Added UserName

    }
}
