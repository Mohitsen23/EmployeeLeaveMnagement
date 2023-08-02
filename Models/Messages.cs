using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace Practice.Models
{
    public class Messages
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime SentAt { get; set; }

      
        public string SenderId { get; set; }
       

     
        public string ReceiverId { get; set; }
     
    }
}
