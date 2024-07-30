using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.EntityRepositories.Interfaces;
using Data.UnitOfWorks.Interfaces;
using Domain.Models.Views;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Implementations;

public class StudentService: BaseService, IStudentService
{
    private readonly IStudentRepository _studentRepository;
    public StudentService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        _studentRepository = unitOfWork.Student;
    }

    public async Task<ICollection<StudentViewModel>> GetStudents()
    {
        var students = await _studentRepository
            .GetAll()
            .ProjectTo<StudentViewModel>(Mapper.ConfigurationProvider)
            .ToListAsync();
        return students;
    }
    
    public async Task<StudentViewModel?> GetStudent(Guid id)
    {
        var student = await _studentRepository
            .Where(x => x.Id.Equals(id))
            .ProjectTo<StudentViewModel>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
        return student;
    }
}