using E_commers.Domain.Identity;

namespace E_commers.Domain.Interface.Authentication
{
    public interface IRoleManagment
    {
        Task<string?> GetUserRole(string userEmail);
        Task<bool?> AddUserToRole(AppUser user,string roleName);
    }
}
