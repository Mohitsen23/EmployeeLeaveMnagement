using System.ComponentModel.DataAnnotations;

namespace Practice.Models
{
    public class NotificationModel
    {
        [Key]
        public int id { get; set; }
        public string message { get; set; }
    }
}
