using E_commers.Domain.Identity;
using E_commers.Domain.Interface.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Org.BouncyCastle.Bcpg;
using Microsoft.VisualBasic;


namespace E_commerce.Infrastructure.Repo.Authentication
{
    public class RoleManagment(UserManager<AppUser> _userManager) : IRoleManagment
    {
        public async Task<bool?> AddUserToRole(AppUser user, string roleName) =>
            (await _userManager.AddToRoleAsync(user, roleName)).Succeeded;
       

        public async Task<string?> GetUserRole(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            return (await _userManager.GetRolesAsync(user)).FirstOrDefault();
        }
    }
}
