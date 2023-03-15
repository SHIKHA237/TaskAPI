using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace TaskAPI.Models
{
    public class Tasks
    {
        [Key]
        public int TaskId { get; set; }
        [Required]
        [Column(TypeName = "varchar(200)")]
        public string Title { get; set; }
        [Column(TypeName = "varchar(500)")]
        public string Description { get; set; }
        [Required]
        [Column(TypeName = "varchar(250)")]
        public string Team { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
       // public List<string> Image { get; }
        public string Status { get; set; }

        public Tasks()
        {

        }
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
