using System.ComponentModel.DataAnnotations;

namespace Practice.Models
{
    public class EmployeesRating
    {
        [Key]
        public int id { get; set; }
        public int ratings { get; set; }
       

        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    }
}
