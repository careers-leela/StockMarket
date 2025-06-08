using static System.Runtime.InteropServices.JavaScript.JSType;
using StockMarket.Model;
using AutoMapper;

namespace StockMarket.MappingProfiles
{
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Represents the AutoMapper profile for mapping between domain models and DTOs.
        /// </summary>
        public MappingProfile()
        {
            CreateMap<Stock, StockResponseDto>();
            CreateMap<StockRequestDto, Stock>();
        }
    }
}
