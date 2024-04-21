using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RentACar.Data;
using RentACar.Interfaces;
using RentACar.Models;
using RentACar.Models.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RentACar.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthRepository(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<AuthResponseModel> Login(UserLoginModel userLoginModel)
        {
            var user = await _context.Users!.AsQueryable()
                .Include(u => u.Roles).ThenInclude(r => r.Role).FirstOrDefaultAsync(x => x.Username.ToLower().Equals(userLoginModel.Username.ToLower()));
            if(user == null)
            {
                throw new Exception("Wrong username or password!");
            }
            else
            {
                if (user.Password == userLoginModel.Password)
                {
                    var token = GenerateJwtToken(user);
                    return new AuthResponseModel {
                        Username = user.Username,
                        Token = token
                    };
                }
                else
                {
                    throw new Exception("Wrong username or password!");
                }
            }


        }

        private string GenerateJwtToken(User user)
        {
            List<Claim> claims =
            [
                new Claim(ClaimTypes.Name, user.Username)
            ];

            user.Roles.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role.Role.Name)));

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
