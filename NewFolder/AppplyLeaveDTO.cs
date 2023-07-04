using Practice.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Practice.NewFolder
{
    public class AppplyLeaveDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Leavetype { get; set; } = null!;

        public int Leaveid { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string Reason { get; set; } = null!;

        public string Status { get; set; } = null!;

        public int Manager { get; set; }

        public virtual LeaveTable Leave { get; set; } = null!;

    }
}
