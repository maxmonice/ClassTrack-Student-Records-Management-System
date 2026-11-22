using System.ComponentModel.DataAnnotations;

namespace ClassTrack.Models;

// Represents one student record. EF Core will treat "Id" as the primary key by convention.
public class Student
{
    public int Id { get; set; }

    [Required(ErrorMessage = "First name is required.")]
    [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Surname is required.")]
    [StringLength(50, ErrorMessage = "Surname cannot exceed 50 characters.")]
    public string Surname { get; set; } = string.Empty;
}
