using ASP_.NET_MVC_Day_2.Repository;
using ASP_.NET_MVC_Day_2.Models;
using ASP_.NET_MVC_Day_2.Models.DTO;
using AutoMapper;
using ASP_.NET_MVC_Day_2.Helpers;
using ASP_.NET_MVC_Day_2.Enum;

namespace ASP_.NET_MVC_Day_2.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;
    private IMapper _mapper;
    public PersonService(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public void AddPerson(PersonCreateDTO person)
    {
        if (person == null)
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
        if (person == null)
        {
            throw new KeyNotFoundException($"Person with ID {id} not found.");
        }
        _personRepository.Delete(id);
    }

    public IEnumerable<PersonDTO> FilterPerson(string query, int year)
    {
        if (string.IsNullOrEmpty(query)) return _mapper.Map<List<PersonDTO>>(_personRepository.GetAll());
        var person = new List<Person>();
        switch (query)
        {
            case "BornIn":
                person = _personRepository.GetAll().Where(x => x.DateOfBirth.Year == year).ToList();
                break;
            case "BornGreater":
                person = _personRepository.GetAll().Where(x => x.DateOfBirth.Year > year).ToList();
                break;
            case "BornLess":
                person = _personRepository.GetAll().Where(x => x.DateOfBirth.Year < year).ToList();
                break;
            default:
                return null;
        }
        return _mapper.Map<List<PersonDTO>>(person);
    }

    public byte[] GenerateExcelFile(IEnumerable<PersonDTO> persons)
    {
        using (var memoryStream = new MemoryStream())
        {
            using (var workbook = new ClosedXML.Excel.XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Persons");
                // Write headers
                worksheet.Cell(1, 2).Value = "First Name";
                worksheet.Cell(1, 3).Value = "Last Name";
                worksheet.Cell(1, 4).Value = "Date of Birth";
                worksheet.Cell(1, 5).Value = "Gender";
                worksheet.Cell(1, 6).Value = "Phone Number";
                worksheet.Cell(1, 7).Value = "Birth Place";
                // Write data
                var row = 2;
                foreach (var person in persons)
                {
                    worksheet.Cell(row, 2).Value = person.FirstName;
                    worksheet.Cell(row, 3).Value = person.LastName;
                    worksheet.Cell(row, 4).Value = person.DateOfBirth.ToString("yyyy-MM-dd");
                    worksheet.Cell(row, 5).Value = person.Gender.ToString();
                    worksheet.Cell(row, 6).Value = person.PhoneNumber;
                    worksheet.Cell(row, 7).Value = person.BirthPlace;
                    row++;
                }
                workbook.SaveAs(memoryStream);
            }
            return memoryStream.ToArray();
        }
    }

    public IEnumerable<PersonDTO> GetAllPerson(int page)
    {
        var persons = _personRepository.GetAll();
        var pageSize = 10;
        var _person = _mapper.Map<IEnumerable<PersonDTO>>(persons).AsQueryable();
        return PaginatedList<PersonDTO>.Create(_person, page, pageSize);
    }

    public List<string> GetFullNameList(IEnumerable<PersonDTO> persons)
    {
        var fullNames = _personRepository.GetAll().Select(p => $"{p.FirstName} {p.LastName}").ToList();
        return fullNames;
    }

    public PersonDTO GetOldestPerson()
    {
        var oldestPerson = _personRepository.GetAll().OrderBy(p => p.DateOfBirth).FirstOrDefault();
        if (oldestPerson == null) throw new Exception("No person in list");
        return _mapper.Map<PersonDTO>(oldestPerson);
    }

    public PersonDTO GetPersonById(Guid id)
    {
        var person = _personRepository.GetById(id);
        return _mapper.Map<PersonDTO>(person);
    }

    public IEnumerable<PersonDTO> GetPersonIsMale()
    {
        var persons = _personRepository.GetAll().Where(x => x.Gender == GenderType.Male).ToList();
        return _mapper.Map<List<PersonDTO>>(persons);
    }

    public void UpdatePerson(Guid id, PersonUpdatedDTO person)
    {
        var _person = _personRepository.GetById(id);
        if (_person == null)
        {
            throw new KeyNotFoundException($"Person with ID {id} not found.");
        }
        person.UpdatedAt = DateTime.Now;
        var entity = _mapper.Map<Person>(person);
        _personRepository.Update(entity);
    }

}
