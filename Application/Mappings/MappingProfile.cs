using AutoMapper;
using Domain.Entities;
using Domain.Models.Authorization;
using Domain.Models.Views;

namespace Application.Mappings;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<Student, StudentViewModel>();
        CreateMap<Student, UserContextModel>();
    }
}