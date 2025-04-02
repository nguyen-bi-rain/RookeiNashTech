using ASP_.NET_MVC.Models;
using ASP_.NET_MVC.Repository;
using OfficeOpenXml;
using static ASP_.NET_MVC.Models.Person;

namespace ASP_.NET_MVC.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public void AddPerson(Person person)
        {

            var _person = _personRepository.GetById(person.Id);
            if (_person != null)
            {
                throw new Exception("The person is already exist");
            }
            _personRepository.Add(person);
        }

        public void DeletePerson(int id)
        {
            var person = _personRepository.GetById(id);
            if (person == null) throw new Exception("Person not found");
            _personRepository.Delete(id);
        }

        public IEnumerable<Person> FilterPerson(string query, int year)
        {
            if (string.IsNullOrEmpty(query)) return _personRepository.GetAll();
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
            return person;
        }

        public byte[] GenerateExcelFile(IEnumerable<Person> persons)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var workbook = new ClosedXML.Excel.XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Persons");
                    // Write headers
                    worksheet.Cell(1, 1).Value = "ID";
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
                        worksheet.Cell(row, 1).Value = person.Id;
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

        public IEnumerable<Person> GetAllPerson()
        {
            return _personRepository.GetAll();
        }

        public List<string> GetFullNameList(IEnumerable<Person> persons)
        {
            var fullNames = GetAllPerson().Select(p => $"{p.FirstName} {p.LastName}").ToList();
            return fullNames;
        }

        public Person GetOldestPerson()
        {
            var oldestPerson = _personRepository.GetAll().OrderBy(p => p.DateOfBirth).FirstOrDefault();
            if (oldestPerson == null) throw new Exception("No person in list");
            return oldestPerson;

        }

        public Person GetPersonById(int id)
        {
            var person = _personRepository.GetById(id);
            if (person == null) throw new Exception("Person not found");
            return person;
        }

        public IEnumerable<Person> GetPersonIsMale()
        {
            var persons = _personRepository.GetAll().Where(x => x.Gender == GenderType.Male).ToList();
            return persons;
        }

        public void UpdatePerson(int id, Person person)
        {
            var _person = _personRepository.GetById(id);
            if (_person == null) throw new Exception("Person not found");
            _personRepository.Update(person);


        }
    }
}
