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
    private readonly IMapper _mapper;

    public PersonService(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public void AddPerson(PersonCreateDTO person)
    {
        if (person == null) throw new ArgumentNullException(nameof(person), "Person cannot be null.");
        person.CreatedAt = DateTime.Now;
        _personRepository.Add(_mapper.Map<Person>(person));
    }

    public void DeletePerson(Guid id)
    {
        var person = _personRepository.GetById(id) ?? throw new KeyNotFoundException($"Person with ID {id} not found.");
        _personRepository.Delete(id);
    }

    public IEnumerable<PersonDTO> FilterPerson(string query, int year)
    {
        var persons = _personRepository.GetAll();
        var filteredPersons = query switch
        {
            "BornIn" => persons.Where(x => x.DateOfBirth.Year == year),
            "BornGreater" => persons.Where(x => x.DateOfBirth.Year > year),
            "BornLess" => persons.Where(x => x.DateOfBirth.Year < year),
            _ => null
        };
        return filteredPersons == null ? null : _mapper.Map<List<PersonDTO>>(filteredPersons.ToList());
    }

    public byte[] GenerateExcelFile(IEnumerable<PersonDTO> persons)
    {
        using var memoryStream = new MemoryStream();
        using var workbook = new ClosedXML.Excel.XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Persons");

        // Write headers
        var headers = new[] { "First Name", "Last Name", "Date of Birth", "Gender", "Phone Number", "Birth Place" };
        for (int i = 0; i < headers.Length; i++)
        {
            worksheet.Cell(1, i + 2).Value = headers[i];
        }

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
        return memoryStream.ToArray();
    }

    public IEnumerable<PersonDTO> GetAllPerson(int page)
    {
        const int pageSize = 10;
        var persons = _personRepository.GetAll();
        return PaginatedList<PersonDTO>.Create(_mapper.Map<IEnumerable<PersonDTO>>(persons).AsQueryable(), page, pageSize);
    }

    public List<string> GetFullNameList(IEnumerable<PersonDTO> persons)
    {
        return _personRepository.GetAll().Select(p => $"{p.FirstName} {p.LastName}").ToList();
    }

    public PersonDTO GetOldestPerson()
    {
        var oldestPerson = _personRepository.GetAll().OrderBy(p => p.DateOfBirth).FirstOrDefault()
            ?? throw new Exception("No person in list");
        return _mapper.Map<PersonDTO>(oldestPerson);
    }

    public PersonDTO GetPersonById(Guid id)
    {
        var person = _personRepository.GetById(id);
        return person == null ? null : _mapper.Map<PersonDTO>(person);
    }

    public IEnumerable<PersonDTO> GetPersonIsMale()
    {
        var persons = _personRepository.GetAll().Where(x => x.Gender == GenderType.Male).ToList();
        return _mapper.Map<List<PersonDTO>>(persons);
    }

    public void UpdatePerson(Guid id, PersonUpdatedDTO person)
    {
        var existingPerson = _personRepository.GetById(id) ?? throw new KeyNotFoundException($"Person with ID {id} not found.");
        person.UpdatedAt = DateTime.Now;
        _personRepository.Update(_mapper.Map<Person>(person));
    }
}
