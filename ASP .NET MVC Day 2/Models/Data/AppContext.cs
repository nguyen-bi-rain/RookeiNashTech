using ASP_.NET_MVC_Day_2.Enum;
using ASP_.NET_MVC_Day_2.Models;

namespace ASP_.NET_MVC_Day_2.Models.Data
{
    public class AppContext : IAppContext
    {
        public List<Person> _persons = new List<Person> () { 
        new Person(Guid.NewGuid(),"Le","Nguyet", GenderType.Female, DateTime.Parse("2003-05-16"),"0123456789","QuangNinh",false),

            new Person(Guid.NewGuid(),"Vu","Nguyen", GenderType.Male, DateTime.Parse("1999-05-17"),"01234242489","Thanh Hoa",true),

            new Person(Guid.NewGuid(),"Tran","khanh Linh", GenderType.Female, DateTime.Parse("2003-05-18"),"30482303049","Ha Noi",false),

            new Person(Guid.NewGuid(),"Nguyen","Quang Hung", GenderType.Female, DateTime.Parse("2000-05-19"),"34344344523","Ben Tre",true),

            new Person(Guid.NewGuid(),"Truong","Thanh Bao", GenderType.Female, DateTime.Parse("2003-05-20"),"3084380444","Cao Bang",false),
            new Person(Guid.NewGuid(),"Tran Dinh","Quan", GenderType.Male, DateTime.Parse("1996-05-16"),"0123456789","Thai Binh",false),

            new Person(Guid.NewGuid(),"Dinh","Quang Tra", GenderType.Male, DateTime.Parse("1999-05-17"),"01234242489","Ninh Binh",true),

            new Person(Guid.NewGuid(),"Nguyen","Trung Quan", GenderType.Male, DateTime.Parse("2003-05-18"),"30482303049","Ha Nam",false),

            new Person(Guid.NewGuid(),"Nguyen","Quang Hung", GenderType.Female, DateTime.Parse("2000-05-19"),"34344344523","Son La",true),

            new Person(Guid.NewGuid(),"Truong","Thanh Bao", GenderType.Female, DateTime.Parse("2003-05-20"),"3084380444","Lang Son",false),
             };
        public List<Person> Persons { get => _persons; set => _persons = value; }
    }
}
