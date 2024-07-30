using Data.EntityRepositories.Interfaces;
using Data.Repositories.Implementations;
using Domain.Context;
using Domain.Entities;

namespace Data.EntityRepositories.Implementations;

public class StudentRepository: Repository<Student>, IStudentRepository
{
    public StudentRepository(StudentManagementDbContext context) : base(context)
    {
    }
}