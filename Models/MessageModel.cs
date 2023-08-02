using System.ComponentModel.DataAnnotations;

namespace Practice.Models
{
    public class MessageModel
    {
        [Key]
        public int id { get; set; }
        public int senderid { get; set; }
        public int receiverid { get; set; }
        public string Message { get; set; }
        public string ReadorNot { get; set; }
        public DateTime timestamp { get; set; }
    }
}
