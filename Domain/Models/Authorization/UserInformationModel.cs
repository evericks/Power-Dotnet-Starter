namespace Domain.Models.Authorization;

public class UserInformationModel
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = null!;

    public string Role { get; set; } = null!;
}