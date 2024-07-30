using Data.EntityRepositories.Interfaces;

namespace Data.UnitOfWorks.Interfaces;

public interface IUnitOfWork
{
    public IAccountRepository Account { get; }
    public IStudentRepository Student { get; }
    
    void BeginTransaction();
    
    void Commit();
    
    void Rollback();
    
    void Dispose();
    
    Task<int> SaveChangesAsync();
}