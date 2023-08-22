using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ZwajApp.api.Helper;
using ZwajApp.api.IRepository;
using ZwajApp.api.Models;
using ZwajApp.api.ViewModels;

namespace ZwajApp.api.RepositoryEmplmentation
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;

        public AuthRepository(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
        }

        public async Task<JwtSecurityToken> CreateToken(User user)
        {
            IList<Claim> userClaims = await _userManager.GetClaimsAsync(user);
            IList<string> roles = await _userManager.GetRolesAsync(user);
            List<Claim> roleClaims = new List<Claim>();

            foreach (string role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }

            IEnumerable<Claim> claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        public async Task<User> GetUser(string Id)
        {
            User? User = await _userManager.FindByIdAsync(Id);
            if (User is null)
            {
                return null;
            }
            return User;
        }

        public async Task<AuthModel> LogIn(UserViewModel model)
        {
            AuthModel authModel = new AuthModel();

            User? user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "Email or Password is incorrect!";
                return authModel;
            }

            JwtSecurityToken jwtSecurityToken = await CreateToken(user);
            IList<string> rolesList = await _userManager.GetRolesAsync(user);

            authModel.isAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.UsreName = user.UserName;
            authModel.Roles = rolesList.ToList();
            authModel.Name = user.Name;
            return authModel;
        }

        public async Task<AuthModel> Register(RegisterModel model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
            {
                return new AuthModel { Message = "Email is Already Regitsered" };
            }

            if (await _userManager.FindByEmailAsync(model.UserName) is not null)
            {
                return new AuthModel { Message = "UserName is Already Regitered" };
            }

            User User = new User()
            {
                UserName = model.UserName,
                Email = model.Email,
                Name = model.Name,
                Gender=model.Gender
            };
            IdentityResult Result = await _userManager.CreateAsync(User, model.Password);

            if (!Result.Succeeded)
            {
                string Error = string.Empty;
                foreach (IdentityError error in Result.Errors)
                {
                    Error += $"{error.Description},";
                }
                return new AuthModel { Message = Error };
            }

            _ = await _userManager.AddToRoleAsync(User, "User");
            JwtSecurityToken Jwt = await CreateToken(User);

            AuthModel Model = new AuthModel
            {
                Email = User.Email,
                //ExpireOn = Jwt.ValidTo,
                isAuthenticated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(Jwt),
                UsreName = User.UserName,
                Name = User.Name
            };
            return Model;
        }
    }
}