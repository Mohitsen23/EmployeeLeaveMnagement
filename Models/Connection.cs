using System.ComponentModel.DataAnnotations;

namespace Practice.Models
{
    public class Connection
    {
        [Key]
        public int id { get; set; }
        public int userid { get; set; }
        public UserIdentity identity { get; set; }
    }
}
