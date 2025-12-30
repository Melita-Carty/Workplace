using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicApi.Models;

public class Project
{
    [Key, Column("ProjectId")]
    public int Id {get; set;}
    
    [Column("ProjectName")]
    public required string Name {get;set;}

    public int DepartmentId {get; set;}
    [ForeignKey("DepartmentId")]
    public Department? Department {get; set;}

}