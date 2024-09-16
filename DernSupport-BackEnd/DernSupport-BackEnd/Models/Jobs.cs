namespace DernSupport_BackEnd.Models
{
    public class Jobs
    {
        public int JobsId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ScheduledDate { get; set; }
        public string Priority { get; set; } 
        public string Cost { get; set; }
    }
}
