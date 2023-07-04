using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Practice.Models;

public partial class Employee
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
