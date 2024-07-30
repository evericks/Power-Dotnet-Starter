using Application.Services.Cache.Interfaces;
using Application.Services.Interfaces;
using Common.Constants;
using Infrastructure.Attributes;
using Microsoft.AspNetCore.Mvc;
using HttpResponse = Common.HttpContexts.HttpResponse;

namespace Presentation.Controllers;

[Route("api/students")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;
    private readonly IResponseCacheService _responseCacheService;

    public StudentsController(IStudentService studentService, IResponseCacheService responseCacheService)
    {
        _studentService = studentService;
        _responseCacheService = responseCacheService;
    }

    [HttpGet]
    [Cache(100)]
    public async Task<IActionResult> GetStudents()
    {
        try
        {
            var students = await _studentService.GetStudents();
            return new OkObjectResult(students);
        }
        catch (Exception e)
        {
            return HttpResponse.InternalServerError(e.Message);
        }
    }

    [HttpGet]
    [Authorize]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetStudent([FromRoute] Guid id)
    {
        try
        {
            var student = await _studentService.GetStudent(id);
            return student != null ? HttpResponse.Ok(student) : HttpResponse.NotFound(AppMessages.StudentNotFound);
        }
        catch (Exception e)
        {
            return HttpResponse.InternalServerError(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> PostStudent()
    {
        try
        {
            var students = await _studentService.GetStudents();
            await _responseCacheService.RemoveCacheResponseAsync("GET/api/students");
            return new OkObjectResult(students);
        }
        catch (Exception e)
        {
            return HttpResponse.InternalServerError(e.Message);
        }
    }

}