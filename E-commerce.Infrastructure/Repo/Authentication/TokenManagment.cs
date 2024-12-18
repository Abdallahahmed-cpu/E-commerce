using E_commers.Domain.Identity;
using E_commers.Domain.Interface.Authentication;
using E_commers.Infrastructure.Data;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;


namespace E_commerce.Infrastructure.Repo.Authentication
{
    public class TokenManagment(AppDbContext _context, IConfiguration _config) : ITokenManagment
    {
        public async Task<int> AddRefreshToken(string userId, string refreshToken)
        {
            _context.refreshTokens.Add(new RefreshToken
            {
                UserId = userId,
                Token = refreshToken
            });
            return await _context.SaveChangesAsync();
        }

        public string GenerateToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var cred = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddHours(2);
            var token = new JwtSecurityToken(
              issuer: _config["JWT:Issuer"],
              audience: _config["JWT:Audience"],
              claims: claims,
              expires: expiration,
              signingCredentials: cred
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GetRefreshToken()
        {
            const int byteSize = 64;
                byte[] randomBytes = new byte[byteSize];
            using(RandomNumberGenerator rng= RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }

        public List<Claim> GetUserClaimsFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            if (jwtToken != null)
                return jwtToken.Claims.ToList();
            else
                return [];
        }

        public async Task<string> GetUserIdByRefreshToken(string refreshToken)
       => (await _context.refreshTokens.FirstOrDefaultAsync(_ => _.Token == refreshToken)).UserId;


        public async Task<int> UpdateRefreshToken(string userId, string refreshToken)
        {
           var  user = await _context.refreshTokens.FirstOrDefaultAsync(_=>_.Token==refreshToken);
            if (user == null) return -1;
            user.Token = refreshToken;
            return await _context.SaveChangesAsync();

         }

        public async Task<bool> ValidateRefreshToken(string refreshToken)
        {
            var user = await _context.refreshTokens.FirstOrDefaultAsync(_=>_.Token== refreshToken);
            return user != null;
        }
    }
}
