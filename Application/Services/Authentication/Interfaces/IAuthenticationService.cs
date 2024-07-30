using Domain.Models.Authorization;
using Domain.Models.Systems;

namespace Application.Services.Authentication.Interfaces;

public interface IAuthenticationService
{
    Task<Result<Domain.Models.Authorization.AuthenticationModel>> AuthenticateAsync(CertificateModel certificateModel);
    Task<UserContextModel?> GetUserInfoAsync(Guid id);
}