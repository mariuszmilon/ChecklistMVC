using System.ComponentModel.DataAnnotations;

namespace Checklist.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        [Required]
        public string Topic { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public Boolean IsCompleted { get; set; }
        public DateTime Start { get; set; }
        [Required]
        public DateTime End { get; set; }
    }
}
