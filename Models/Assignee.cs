using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskAPI.Models
{
    public class Assignee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        [ForeignKey("TaskId")]
        public int TaskId { get; set; }

        public Assignee(string name, int taskId)
        {
            Name = name;
            TaskId = taskId;
        }
    }
}
