// Models/Conge.cs
namespace WebApplication3.Models
{
    public class Conge
    {
        public int Id { get; set; }
        public string? Type { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Reason { get; set; } 
        public string? Status { get; set; } // Added '?' to make it nullable
    }
}