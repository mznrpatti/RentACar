using RentACar.Models;

namespace RentACar.Interfaces
{
    public interface IAuthRepository
    {
        Task<AuthResponseModel> Login(UserLoginModel userLoginModel);
    }
}
