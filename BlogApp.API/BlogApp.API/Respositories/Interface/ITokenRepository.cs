using Microsoft.AspNetCore.Identity;

namespace BlogApp.API.Respositories.Interface
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
