using ASP_.NET_MVC_Day_2.Models;
using ASP_.NET_MVC_Day_2.Models.Data;

namespace ASP_.NET_MVC_Day_2.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IAppContext _context;

        public PersonRepository(IAppContext context)
        {
            _context = context;
        }

        public void Add(Person person)
        {
            _context.Persons.Add(person);
        }

        public void Delete(Guid id)
        {
            var person = _context.Persons.FirstOrDefault(p => p.Id == id);
            _context.Persons.Remove(person);
        }

        public IEnumerable<Person> GetAll()
        {
            var persons = _context.Persons.ToList();
            return persons;
        }

        public Person GetById(Guid id)
        {
            var person = _context.Persons.FirstOrDefault(p => p.Id == id);
            if (person == null)
            {
                throw new KeyNotFoundException($"Person with ID {id} not found.");
            }
            return person;
        }

        public void Update(Person person)
        {
            var currentPerson = _context.Persons.FirstOrDefault(p => p.Id == person.Id);
            if (currentPerson == null)
            {
                throw new KeyNotFoundException($"Person with ID {person.Id} not found.");
            }
            currentPerson.FirstName = person.FirstName;
            currentPerson.LastName = person.LastName;
            currentPerson.Gender = person.Gender;
            currentPerson.DateOfBirth = person.DateOfBirth;
            currentPerson.PhoneNumber = person.PhoneNumber;
            currentPerson.BirthPlace = person.BirthPlace;
            currentPerson.IsGraduated = person.IsGraduated;
        }
    }
}
