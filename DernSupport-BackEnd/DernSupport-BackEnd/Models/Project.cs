using System.ComponentModel.DataAnnotations.Schema;

namespace DernSupport_BackEnd.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Budget { get; set; }
        public double Hours { get; set; }


        public ICollection<TechnicianProjects> technicianProjects { get; set;}

    }
}
