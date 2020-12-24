using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MyShop.API.Data;
using MyShop.API.Domain;
using MyShop.API.Settings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
namespace MyShop.API.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly DataContext _dataContext; 
        private readonly UserManager<IdentityUser> _userManager;
        public IdentityService(DataContext dataContext, UserManager<IdentityUser> userManager,JwtSettings jwtSettings)
        {
            _dataContext = dataContext;
            _userManager = userManager;
            _jwtSettings = jwtSettings;

        }
        public async  Task<AuthenticationResult> RegisterAsync(string email,string password)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);
            if(existingUser is not null)
            {
                return new AuthenticationResult
                {
                    IsSuccess = false,
                    Errors = new string[]
                    {
                     "User with that mail already exists"
                    }

                };
            }

            else
            {
                var user = new IdentityUser
                {
                    Email = email,
                    UserName = email
                };

                var result = await _userManager.CreateAsync(user, password);
               if(result.Succeeded is false)
                {
                    new AuthenticationResult
                    {
                        IsSuccess = false,
                        Errors = result.Errors.Select(x => x.Description)
                    };
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {

                        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        new Claim("id", user.Id)
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return new AuthenticationResult
                {
                    IsSuccess = true,
                    Token = tokenHandler.WriteToken(token)

                };
            }
        }
    }
}
