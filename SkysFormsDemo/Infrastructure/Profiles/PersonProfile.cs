using AutoMapper;
using GoodToHave.Data;

namespace SkysFormsDemo.Infrastructure.Profiles;

public class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<Person, Pages.Person.EditModel>()
            .ReverseMap();
    }
}