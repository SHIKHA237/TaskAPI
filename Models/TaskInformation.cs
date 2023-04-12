using System.ComponentModel.DataAnnotations;

namespace TaskAPI.Models
{
    public class TaskInformation
    {
        public int TaskId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Team { get; set; }
        public string? AssigneeName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DueDate { get; set; }
        // public List<string> Image { get;set; }
        public string? Status { get; set; }
    }
}
