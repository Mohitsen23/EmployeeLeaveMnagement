using System.ComponentModel.DataAnnotations;

namespace Practice.Models
{
    public class Document
    {
        [Key]
        public int id { get; set; }
        public string documentName { get; set;}

        [Required]
        public byte[] File { get; set; }
        public int Emplid  { get; set; }

    }
}
