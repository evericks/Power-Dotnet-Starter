using Domain.Models.Views;

namespace Application.Services.Interfaces;

public interface IStudentService
{
    Task<ICollection<StudentViewModel>> GetStudents();
    Task<StudentViewModel?> GetStudent(Guid id);
}