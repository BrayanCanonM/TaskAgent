using System.ComponentModel.DataAnnotations;

namespace TaskAgent.Models
{
    public class TimeTracker
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime DateStart { get; set; }
        [Required]
        public DateTime DateEnd { get; set; }
        [Required]
        public string? Type { get; set; }
        public string? Comment { get; set; }
        public string? IdUser { get; set; }
    }
}
