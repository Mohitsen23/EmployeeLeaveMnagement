using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Practice.Models
{
    public class User:IdentityUser
    {
        [Key]
        public int id;
        public virtual ICollection<Messages> SentMessages { get; set; }

        // Navigation property to store received messages by this user.
        public virtual ICollection<Messages> ReceivedMessages { get; set; }
    }
}
