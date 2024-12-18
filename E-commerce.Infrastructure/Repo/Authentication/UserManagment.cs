using E_commers.Domain.Identity;
using E_commers.Domain.Interface.Authentication;
using E_commers.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace E_commerce.Infrastructure.Repo.Authentication
{
    public class UserManagment(IRoleManagment _roleManagment,UserManager<AppUser> _userManager , AppDbContext _context) : IUserManagment
    {
        public async Task<bool> CreateUser(AppUser user)
        {
            AppUser _user = await GetUserByEmail(user.Email);
            if (_user != null) return false;

            return (await _userManager.CreateAsync(user!, user!.PasswordHash)).Succeeded;
         }

        public async Task<IEnumerable<AppUser>> GetALLUsers() => await _context.Users.ToListAsync();
       

        public async Task<AppUser> GetUserByEmail(string email)
         => await _userManager.FindByEmailAsync(email);


        public async Task<AppUser> GetUserById(string id)
        {
          var user =  await _userManager.FindByIdAsync(id);
            return user;
        }


        public async Task<List<Claim>> GetUserClaim(string email)
        {
          var _user = await GetUserByEmail(email);
            string? roleName = await _roleManagment.GetUserRole(_user!.Email!);

            List<Claim> claims =
                [
                new Claim("FullName",_user!.FullName),
                new Claim(ClaimTypes.NameIdentifier,_user.Id),
                new Claim(ClaimTypes.Email,_user.Email),
                new Claim(ClaimTypes.Role,roleName),

                ];
            return claims;
        }



        public async Task<bool> LoginUser(AppUser user)
        {
            var _user = await GetUserByEmail(user.Email);
            if (_user is null) return false;
            
            string roleName = await _roleManagment.GetUserRole(_user!.Email!);
            if (string.IsNullOrEmpty(roleName)) return false;

            return await _userManager.CheckPasswordAsync(_user, user.PasswordHash!);
        }

        public async Task<int> RemoveUserByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(_ => _.Email == email);
            _context.Users.Remove(user);
            return await _context.SaveChangesAsync();
        }
    }
}
