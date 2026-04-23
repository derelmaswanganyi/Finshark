using api.Models;

namespace api.Interfaces
{
    public interface ITokenServices
    {
         string CreateToken(AppUser user);
    }
}