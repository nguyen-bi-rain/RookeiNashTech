using ASP_.NET_MVC.Models;
using ASP_.NET_MVC.Models.Data;

namespace ASP_.NET_MVC.Repository
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

        public void Delete(int id)
        {
            var person = _context.Persons.FirstOrDefault(u => u.Id == id);
            if (person != null)
            {
                _context.Persons.Remove(person);
            }
        }

        public IEnumerable<Person> GetAll()
        {
            return _context.Persons;
        }

        public Person GetById(int id)
        {
            return _context.Persons.FirstOrDefault(u => u.Id == id);
        }

        public void Update(Person person)
        {
            var existingPerson = _context.Persons.FirstOrDefault(u => u.Id == person.Id);
            if (existingPerson != null)
            {
                existingPerson.FirstName = person.FirstName;
                existingPerson.LastName = person.LastName;
                existingPerson.Gender = person.Gender;
                existingPerson.DateOfBirth = person.DateOfBirth;
                existingPerson.PhoneNumber = person.PhoneNumber;
                existingPerson.BirthPlace = person.BirthPlace;
                existingPerson.IsGraduated = person.IsGraduated;
            }
        }
    }
}
