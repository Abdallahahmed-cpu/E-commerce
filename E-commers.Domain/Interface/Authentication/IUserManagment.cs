using E_commers.Domain.Identity;
using System.Security.Claims;

namespace E_commers.Domain.Interface.Authentication
{
    public interface IUserManagment
    {
        Task<bool> CreateUser(AppUser user);
        Task<bool> LoginUser(AppUser user);
        Task<AppUser> GetUserByEmail(string email);
        Task<AppUser> GetUserById(string id);
        Task<IEnumerable<AppUser>> GetALLUsers();
        Task<int>RemoveUserByEmail(string email);   
        Task<List<Claim>> GetUserClaim(string email);
    }
}
