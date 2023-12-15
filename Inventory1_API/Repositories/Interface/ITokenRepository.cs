using Microsoft.AspNetCore.Identity;

namespace Inventory1_API.Repositories.Interface
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
