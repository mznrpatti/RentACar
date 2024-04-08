using AutoMapper;
using RentACar.Models.Entities;
using RentACar.Models;

namespace RentACar.Repository.Mappings
{
    public class RentalMapperProfile : Profile
    {
        public RentalMapperProfile()
        {
            CreateMap<Rental, RentalModel>();
            CreateMap<RentalModel, Rental>();
        }
    }
}
