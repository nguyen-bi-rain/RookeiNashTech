using ASP_.NET_MVC_Day_2.Models.DTO;

namespace ASP_.NET_MVC_Day_2.Services
{
    public interface IPersonService
    {
        PersonDTO GetPersonById(Guid id);
        IEnumerable<PersonDTO> GetAllPerson(int page);
        void AddPerson(PersonCreateDTO person);
        void UpdatePerson(Guid id,PersonUpdatedDTO person);
        void DeletePerson(Guid id);
    }
}
