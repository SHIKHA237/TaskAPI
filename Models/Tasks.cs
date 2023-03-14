using System.ComponentModel.DataAnnotations;
using System.Data;

namespace TaskAPI.Models
{
    public class Tasks
    {
        [Key]
        [Required]
        public int TaskId { get; }
        [Required]
        public string Title { get; }
        public string Description { get; }
        [Required]
        public string Team { get; }
        public DateTime CreatedDate { get; }
        [Required]
        public DateTime DueDate { get; }
       // public List<string> Image { get; }
        public string Status { get; }

        public Tasks(
            int taskId,
            string title,
            string description, 
            string team, 
            DateTime createdDate, 
            DateTime dueDate, 
            string status)
        {
            TaskId = taskId;
            Title = title;
            Description = description;
            Team = team;
            CreatedDate = createdDate;
            DueDate = dueDate;
            Status = status;
        }
    }



}
