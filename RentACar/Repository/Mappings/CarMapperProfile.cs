using AutoMapper;
using RentACar.Models;
using RentACar.Models.Entities;

namespace RentACar.Repository.Mappings
{
    public class CarMapperProfile : Profile
    {
        public CarMapperProfile() 
        {
            //Car to CarModel
            CreateMap<Car, CarModel>()
                .ForMember(dst => dst.CategoryName, opt => opt.MapFrom((src, dst) => src.Category.Name));
        }
    }
}
