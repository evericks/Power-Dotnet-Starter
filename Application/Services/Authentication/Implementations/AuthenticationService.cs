using Application.Services.Authentication.Interfaces;
using Application.Settings;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Constants;
using Common.Helpers;
using Data.EntityRepositories.Interfaces;
using Data.UnitOfWorks.Interfaces;
using Domain.Models.Authorization;
using Domain.Models.Systems;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Application.Services.Authentication.Implementations;

public class AuthenticationService : BaseService, IAuthenticationService
{
    private readonly AppSettings _appSettings;
    private readonly IAccountRepository _accountRepository;

    public AuthenticationService(IUnitOfWork unitOfWork, IMapper mapper, IOptions<AppSettings> appSettings) : base(
        unitOfWork, mapper)
    {
        _appSettings = appSettings.Value;
        _accountRepository = unitOfWork.Account;
    }

    public async Task<Result<AuthenticationModel>> AuthenticateAsync(
        CertificateModel certificateModel)
    {
        var account = await _accountRepository.Where(x =>
                x.Email.Equals(certificateModel.Email) && x.Password.Equals(certificateModel.Password))
            .Include(x => x.Student)
            .FirstOrDefaultAsync();
        if (account == null)
        {
            return Result<AuthenticationModel>.Failure(404, AppMessages.AccountNotFound);
        }
        if (account.Student == null)
        {
            return Result<AuthenticationModel>.Failure(404, AppMessages.StudentNotFound);
        }
        var accessToken = JwtHelper.GenerateJwtToken(account.Student.Id, UserRoles.Student, _appSettings.Secret);
        var result = new AuthenticationModel()
        {
            AccessToken = accessToken,
            // User information here
            User = null!
        };
        return Result<AuthenticationModel>.Success(result);
    }

    public async Task<UserContextModel?> GetUserInfoAsync(Guid id)
    {
        var user = await _accountRepository.Where(x => x.Student != null && x.Student.Id.Equals(id))
            .Select(x => x.Student)
            .ProjectTo<UserContextModel>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
        return user;
    }
}