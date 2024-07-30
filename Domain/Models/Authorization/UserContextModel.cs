namespace Domain.Models.Authorization;

public class UserContextModel
{
    public Guid Id { get; set; }
    

    public string Name { get; set; } = null!;

    public string Role { get; set; } = null!;
}