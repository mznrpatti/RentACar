using AutoMapper;
using RentACar.Models;
using RentACar.Models.Entities;

namespace RentACar.Repository.Mappings
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            //User to UserModel
            CreateMap<User, UserModel>();
        }
    }
}
