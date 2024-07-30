namespace Fit_API.src.Models;

public class UserModel
{
    public required int IdUser { get; set; }
    public required string? Name { get; set; }
    public required string? UserName { get; set; }
    public required string? Email { get; set; }
    public required string? Password { get; set; }
    public required string? Objetivo { get; set; }
}
