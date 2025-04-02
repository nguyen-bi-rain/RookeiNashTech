using ASP_.NET_MVC_Day_2.Enum;

namespace ASP_.NET_MVC_Day_2.Models.DTO;
public class PersonDTO
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public GenderType Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public string BirthPlace { get; set; }
    public bool IsGraduated { get; set; }
}
