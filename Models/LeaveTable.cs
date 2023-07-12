using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Practice.Models;

public partial class LeaveTable
{

   
 
    public int Id { get; set; }
    [Key]
  
    public int Leaveid { get; set; }

    public int Employeeid { get; set; }

   
}
