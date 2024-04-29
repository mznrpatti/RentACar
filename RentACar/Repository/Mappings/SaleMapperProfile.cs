using AutoMapper;
using RentACar.Models;
using RentACar.Models.Entities;

namespace RentACar.Repository.Mappings
{
    public class SaleMapperProfile : Profile
    {
        public SaleMapperProfile()
        {
            CreateMap<Sale, SaleModel>();
            CreateMap<SaleModel, Sale>();
            CreateMap<CreateSaleModel, Sale>();
        }
    }
}