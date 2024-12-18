using System.Security.Claims;

namespace E_commers.Domain.Interface.Authentication
{
    public interface ITokenManagment
    {
        string GetRefreshToken();
        List<Claim> GetUserClaimsFromToken(string token);
        Task<bool> ValidateRefreshToken(string refreshToken);
        Task<string> GetUserIdByRefreshToken(string refreshToken);
        Task<int> AddRefreshToken(string userId,string refreshToken);
        Task<int> UpdateRefreshToken(string userId,string refreshToken);
        string GenerateToken(List<Claim> claims);  

    }
}
