using System.ComponentModel.DataAnnotations;
using ASP_.NET_MVC_Day_2.Enum;

namespace ASP_.NET_MVC_Day_2.Models.DTO;

public class PersonUpdatedDTO
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "First Name is required.")]
    [StringLength(50, ErrorMessage = "First Name cannot be longer than 50 characters.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last Name is required.")]
    [StringLength(50, ErrorMessage = "Last Name cannot be longer than 50 characters.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Gender is required.")]
    public GenderType Gender { get; set; }

    [Required(ErrorMessage = "Date of Birth is required.")]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    [Required(ErrorMessage = "Phone Number is required.")]
    [Phone(ErrorMessage = "Invalid phone number format.")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Birth Place is required.")]
    [StringLength(100, ErrorMessage = "Birth Place cannot be longer than 100 characters.")]
    public string BirthPlace { get; set; }

    public bool IsGraduated { get; set; }

    public DateTime? UpdatedAt { get; set; }
}