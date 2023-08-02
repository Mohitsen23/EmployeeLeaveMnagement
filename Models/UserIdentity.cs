using System.ComponentModel.DataAnnotations;

namespace Practice.Models
{
    public class UserIdentity
    {
        [Key]
        public int id { get; set; }
        public int UserId { get; set; }
        public string ConnectionId { get; set; }

       
    }

}
