using Practice.Models;

namespace Practice.NewFolder
{
    public class EmployeeDto
    {


        public int Id { get; set; }

        public string? Firstname { get; set; }

        public string? Lastname { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? Department { get; set; }

        public string? Companyname { get; set; }

        public int? Manager { get; set; }

        public virtual ICollection<LeaveQuotum> LeaveQuota { get; set; } = new List<LeaveQuotum>();

        public virtual Manager? ManagerNavigation { get; set; }
    }
}
