using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Domain.Entities;

public sealed class User:IdentityUser<String>
{
    public User()
    {
        Id = Guid.NewGuid().ToString();

    }

    public string NameLastName {  get; set; }
    public string RefreshToken { get; set; }
    public DateTime? RefreshTokenExpires { get; set; }




}
