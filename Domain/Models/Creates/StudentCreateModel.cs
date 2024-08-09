using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Creates;

public class StudentCreateModel
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    
    public string Password { get; set; } = null!;
}