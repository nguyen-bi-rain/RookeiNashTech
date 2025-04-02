using ASP_.NET_MVC.Models;

namespace ASP_.NET_MVC.Services
{
    public interface IPersonService
    {
        void AddPerson(Person person);
        void UpdatePerson(int id,Person person);
        void DeletePerson(int id);
        Person GetPersonById(int id);
        IEnumerable<Person> GetAllPerson();
        Person GetOldestPerson();        
        IEnumerable<Person> GetPersonIsMale();
        IEnumerable<Person> FilterPerson(string query,int year);
        byte [] GenerateExcelFile(IEnumerable<Person> persons); 
        List<string> GetFullNameList(IEnumerable<Person> persons);
    }
}
