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

        // Foreign key to associate the message with the sender (User).
        public string SenderId { get; set; }
        public virtual User Sender { get; set; }

        // Foreign key to associate the message with the receiver (User).
        public string ReceiverId { get; set; }
        public virtual User Receiver { get; set; }
    }
}
