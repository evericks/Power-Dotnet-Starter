using Application.Services.Authentication.Interfaces;
using Domain.Models.Authorization;
using Microsoft.AspNetCore.Mvc;
using HttpResponse = Common.HttpContexts.HttpResponse;

namespace Presentation.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthenticationController : Controller
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost]
    public async Task<IActionResult> Authenticate([FromBody] CertificateModel certificateModel)
    {
        try
        {
            var result = await _authenticationService.AuthenticateAsync(certificateModel);
            return result.IsSuccess ? HttpResponse.Ok(result.Data!) : HttpResponse.NotFound();
        }
        catch (Exception e)
        {
            return HttpResponse.InternalServerError(e.Message);
        }
    }
}