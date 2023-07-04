using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Practice.Models;

public partial class LeaveQuotum
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int Emplid { get; set; }

    public int Remainingleave { get; set; }

    public int Totalleave { get; set; }

    public int Usedleave { get; set; }

    public virtual Employee Empl { get; set; } = null!;
}
