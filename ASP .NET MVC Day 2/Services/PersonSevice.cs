using ASP_.NET_MVC_Day_2.Repository;
using ASP_.NET_MVC_Day_2.Models;
using ASP_.NET_MVC_Day_2.Models.DTO;
using AutoMapper;
using ASP_.NET_MVC_Day_2.Helpers;

namespace ASP_.NET_MVC_Day_2.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;
    private IMapper _mapper;
    public PersonService(IPersonRepository personRepository,IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public void AddPerson(PersonCreateDTO person)
    {
        if(person == null)
        {
            throw new ArgumentNullException(nameof(person), "Person cannot be null.");
        }
        person.CreatedAt = DateTime.Now;
        var entity = _mapper.Map<Person>(person);
        _personRepository.Add(entity);
    }

    public void DeletePerson(Guid id)
    {
        var person = _personRepository.GetById(id);
        if(person == null)
        {
            throw new KeyNotFoundException($"Person with ID {id} not found.");
        }
        _personRepository.Delete(id);
    }

    public IEnumerable<PersonDTO> GetAllPerson(int page)
    {
        var persons = _personRepository.GetAll();
        var pageSize = 5;
        var _person = _mapper.Map<IEnumerable<PersonDTO>>(persons).AsQueryable();
        return PaginatedList<PersonDTO>.Create(_person, page, pageSize);
    }

    public PersonDTO GetPersonById(Guid id)
    {
        var person = _personRepository.GetById(id);
        return _mapper.Map<PersonDTO>(person);
    }

    public void UpdatePerson(Guid id,PersonUpdatedDTO person)
    {
        var _person = _personRepository.GetById(id);
        if(_person == null)
        {
            throw new KeyNotFoundException($"Person with ID {id} not found.");
        }
        person.UpdatedAt = DateTime.Now;
        var entity = _mapper.Map<Person>(person);
        _personRepository.Update(entity);
    }

}
