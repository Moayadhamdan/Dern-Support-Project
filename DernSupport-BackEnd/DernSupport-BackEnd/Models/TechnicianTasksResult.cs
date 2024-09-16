using DernSupport_BackEnd.Models.DTO;

namespace DernSupport_BackEnd.Models
{
    public class TechnicianTasksResult
    {
        public List<TechnicianDTO> Tasks { get; set; }
        public List<string> UserNames { get; set; }
    }
}
