using System.ComponentModel.DataAnnotations;

namespace Practice.Models
{
    public class Profile
    {
        [Key]
        public int id { get; set; }
        public int Emplid { get; set; }
        public byte[] img { get; set; }


    }

}