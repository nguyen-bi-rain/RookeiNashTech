using ASP_.NET_MVC_Day_2.Models;

namespace ASP_.NET_MVC_Day_2.Models.Data
{
    public interface IAppContext
    {
        public List<Person> Persons { get; set; }
    }
}
