using Microsoft.AspNetCore.Mvc.Localization;
using static ASP_.NET_MVC.Models.Person;

namespace ASP_.NET_MVC.Models.Data
{
    public class AppContext : IAppContext
    {
        public List<Person> _persons = new List<Person> () { 
        new Person(1,"Le","Nguyet", GenderType.Female, DateTime.Parse("2003-05-16"),"0123456789","QuangNinh",false),

            new Person(2,"Vu","Nguyen", GenderType.Male, DateTime.Parse("1999-05-17"),"01234242489","QuangNinh",true),

            new Person(3,"Tran","khanh Linh", GenderType.Female, DateTime.Parse("2003-05-18"),"30482303049","QuangNinh",false),

            new Person(4,"Nguyen","Quang Hung", GenderType.Female, DateTime.Parse("2000-05-19"),"34344344523","QuangNinh",true),

            new Person(5,"Truong","Thanh Bao", GenderType.Female, DateTime.Parse("2003-05-20"),"3084380444","QuangNinh",false),
             };
        public List<Person> Persons { get => _persons; set => _persons = value; }


    }
}
