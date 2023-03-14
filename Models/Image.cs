using System.ComponentModel.DataAnnotations;

namespace TaskAPI.Models
{
    public class Image
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int TaskID { get; set; }
    }
}
