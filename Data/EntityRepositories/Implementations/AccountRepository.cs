using Data.EntityRepositories.Interfaces;
using Data.Repositories.Implementations;
using Domain.Context;
using Domain.Entities;

namespace Data.EntityRepositories.Implementations;

public class AccountRepository: Repository<Account>, IAccountRepository
{
    public AccountRepository(StudentManagementDbContext context) : base(context)
    {
    }
}