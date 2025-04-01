using ASP_.NET_MVC.Models;
using ASP_.NET_MVC.Repository;
using System.IO.Pipes;
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

        public IEnumerable<Person> FilterPerson(string query,int year)
        {
            if (string.IsNullOrEmpty(query)) return _personRepository.GetAll();
            var person = new List<Person>();
            switch(query){
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

        public IEnumerable<Person> GetAllPerson()
        {
            return _personRepository.GetAll();
        }

        public Person GetOldestPerson()
        {
            return _personRepository.GetAll().OrderByDescending(i => i.GetAge()).FirstOrDefault();
        }

        public Person GetPersonById(int id)
        {
            var person = _personRepository.GetById(id);
            if(person == null) throw new Exception("Person not found"); 
            return person;
        }

        public IEnumerable<Person> GetPersonIsMale()
        {
            var persons = _personRepository.GetAll().Where(x => x.Gender  == GenderType.Male).ToList();
            return persons;
        }

        public void UpdatePerson(int id,Person person)
        {
            var _person = _personRepository.GetById(id);
            if (_person == null) throw new Exception("Person not found");
            _personRepository.Update(person);


        }
    }
}
