using ASP_.NET_MVC_Day_2.Models;

namespace ASP_.NET_MVC_Day_2.Repository
{
    public interface IPersonRepository 
    {
        Person GetById(Guid id);
        IEnumerable<Person> GetAll();
        void Add(Person person);
        void Update(Person person);
        void Delete(Guid id);
        
    }
}
