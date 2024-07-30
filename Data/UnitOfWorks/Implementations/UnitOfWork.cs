using Data.EntityRepositories.Implementations;
using Data.EntityRepositories.Interfaces;
using Data.UnitOfWorks.Interfaces;
using Domain.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace Data.UnitOfWorks.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly StudentManagementDbContext _context;
    private IDbContextTransaction _transaction = null!;

    public UnitOfWork(StudentManagementDbContext context)
    {
        _context = context;
    }

    private IAccountRepository? _account;

    public IAccountRepository Account
    {
        get { return _account ??= new AccountRepository(_context); }
    }

    private IStudentRepository? _student;

    public IStudentRepository Student
    {
        get { return _student ??= new StudentRepository(_context); }
    }

    public void BeginTransaction()
    {
        _transaction = _context.Database.BeginTransaction();
    }

    public void Commit()
    {
        try
        {
            _transaction.Commit();
        }
        finally
        {
            _transaction.Dispose();
            _transaction = null!;
        }
    }

    public void Rollback()
    {
        try
        {
            _transaction.Rollback();
        }
        finally
        {
            _transaction.Dispose();
            _transaction = null!;
        }
    }

    public void Dispose()
    {
        _transaction.Dispose();
        _context.Dispose();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}