using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskAPI.Models
{
    public class Assignee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string Name { get; set; }
      //  [ForeignKey("TaskId")]
        public int TaskId { get; set; }
      //  public virtual Tasks Tasks { get; set; }

        public Assignee()
        {
        }

        public Assignee(string name, int taskId)
        {
            Name = name;
            TaskId = taskId;
        }
    }
}
