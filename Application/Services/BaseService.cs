using AutoMapper;
using Data.UnitOfWorks.Interfaces;

namespace Application.Services;

public class BaseService
{
    protected readonly IMapper Mapper;
    protected readonly IUnitOfWork UnitOfWork;

    protected BaseService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        Mapper = mapper;
        UnitOfWork = unitOfWork;
    }
}