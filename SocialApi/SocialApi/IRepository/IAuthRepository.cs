using System.IdentityModel.Tokens.Jwt;
using ZwajApp.api.Models;
using ZwajApp.api.ViewModels;

namespace ZwajApp.api.IRepository
{
    public interface IAuthRepository
    {
        Task<AuthModel> LogIn(UserViewModel Login);
        Task<AuthModel> Register(RegisterModel Register);
        Task<JwtSecurityToken> CreateToken(User user);
        Task<User> GetUser(string Id);
    }
}