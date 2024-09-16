namespace DernSupport_BackEnd.Models
{
    public class JobSparePart
    {
        public int JobSparePartId { get; set; }  
        public string Name { get; set; } 
        public string Category { get; set; } 
        public string Description { get; set; }
        public int QuantityInStock { get; set; }
    }
}
