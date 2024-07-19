using System.ComponentModel.DataAnnotations;

public class CreateApply{

    [Required]
    public string? MatricId {get; set;}
    [Required]
    public string? StaffId {get; set;}
}