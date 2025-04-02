using ASP_.NET_MVC_Day_2.Models;
using ASP_.NET_MVC_Day_2.Models.DTO;
using AutoMapper;

namespace ASP_.NET_MVC_Day_2.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person,PersonDTO>();
            CreateMap<PersonCreateDTO, Person>();
            CreateMap<PersonUpdatedDTO, Person>();
        }
    }
}
