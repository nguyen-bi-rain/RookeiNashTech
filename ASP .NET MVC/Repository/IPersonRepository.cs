using ASP_.NET_MVC.Models;

namespace ASP_.NET_MVC.Repository
{
    public interface IPersonRepository 
    {
        Person GetById(int id);
        IEnumerable<Person> GetAll();
        void Add(Person person);
        void Update(Person person);
        void Delete(int id);
        
    }
}
