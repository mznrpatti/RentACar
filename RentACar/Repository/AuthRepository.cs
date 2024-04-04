using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Interfaces;
using RentACar.Models;

namespace RentACar.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context; 
        }
        public async Task<AuthResponseModel> Login(UserLoginModel userLoginModel)
        {
            var user = await _context.Users!.FirstOrDefaultAsync(x => x.Username.ToLower().Equals(userLoginModel.Username.ToLower()));
            if(user == null)
            {
                throw new Exception("Wrong username or password!");
            }
            else
            {
                if (user.Password == userLoginModel.Password)
                {
                    return new AuthResponseModel {
                        Username = user.Username
                    };
                }
                else
                {
                    throw new Exception("Wrong username or password!");
                }
            }


        }
    }
}
