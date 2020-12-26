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
using Microsoft.EntityFrameworkCore;
using MyShop.API.DTO;

namespace MyShop.API.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly DataContext _dataContext;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly UserManager<IdentityUser> _userManager;
        public IdentityService(DataContext dataContext, UserManager<IdentityUser> userManager, JwtSettings jwtSettings, TokenValidationParameters tokenValidationParameters)
        {
            _dataContext = dataContext;
            _userManager = userManager;
            _jwtSettings = jwtSettings;
            _tokenValidationParameters = tokenValidationParameters;

        }
        public async Task<AuthenticationResult> RegisterAsync(string email, string password)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser is not null)
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
                var newUserId = Guid.NewGuid();
                var user = new IdentityUser
                {
                    Id=newUserId.ToString(),
                    Email = email,
                    UserName = email
                };

                var result = await _userManager.CreateAsync(user, password);
                return await GenereteAuthenticationForuser(user);


            }
        }
        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] {
                        "User does not exist"
                    }
                };
            }

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, password);
            if (isPasswordCorrect is false)
            {
                return new AuthenticationResult
                {
                    Errors = new[]
               {
               "The login details provided are not valid"
               }
                };
            }

            return await GenereteAuthenticationForuser(user);


        }

        private ClaimsPrincipal GetPrincipalFromTokenPrincipal(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    return null;
                }
                else
                {
                    return principal;
                }

            }

            catch
            {
                return null;
            }
        }

        public async Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken)
        {
            var validatedToken = GetPrincipalFromTokenPrincipal(token);
            if (validatedToken is null)
            {
                return new AuthenticationResult { Errors = new[] { "Invalid Token" } };
            }

            var expiryDateUnix = long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

            var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(expiryDateUnix);

            if (expiryDateTimeUtc > DateTime.UtcNow)
            {
                return new AuthenticationResult { Errors = new[] { "This token hasn't expired yet" } };
            }

            var jti = validatedToken.Claims.Single(x => x.Type ==JwtRegisteredClaimNames.Jti ).Value;
            var storedRefreshToken = await _dataContext.RefreshTokens.SingleOrDefaultAsync(x => x.Token == refreshToken);
            if (storedRefreshToken is null)
            {
                return new AuthenticationResult { Errors = new[] { "This refresh token doesnt exist" } };


            }

            if (DateTime.UtcNow > storedRefreshToken.ExpiryTime)
            {
                return new AuthenticationResult { Errors = new[] { "this token has expired" } };
            }

            if (storedRefreshToken.Invalidated)
            {
                return new AuthenticationResult { Errors = new[] { "this token is invalidated" } };
            }
            if (storedRefreshToken.JwtId != jti)
            {
                return new AuthenticationResult { Errors = new[] { "this token dont match the jwt token" } };
            }

            storedRefreshToken.Used = true;


            var userId = validatedToken.Claims.Single(x => x.Type == "id").Value;

            _dataContext.RefreshTokens.Update(storedRefreshToken);
            await _dataContext.SaveChangesAsync();
            var user = await  _userManager.FindByIdAsync(userId);


            return await GenereteAuthenticationForuser(user);



        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken)
                    && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCulture);




        }
        private async  Task<AuthenticationResult> GenereteAuthenticationForuser(IdentityUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                        new Claim(JwtRegisteredClaimNames.Iss,"http://localhost:50654"),
                        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        new Claim("id", user.Id)
                    }),
                Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifeTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var refreshToken = new RefreshTokenDTO
            {
                Token = Guid.NewGuid().ToString(),
                JwtId = token.Id,
                UserId = user.Id,
                CreationDate = DateTime.UtcNow,
                ExpiryTime = DateTime.UtcNow.AddMonths(6)
            };

            await _dataContext.RefreshTokens.AddAsync(refreshToken);
            await _dataContext.SaveChangesAsync();
            return new AuthenticationResult
            {
                IsSuccess = true,
                Token = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.Token


            };
        }
    }
}






