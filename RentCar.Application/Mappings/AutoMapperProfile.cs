using AutoMapper;
using RentCar.Application.Dtos;
using RentCar.Application.Dtos.Requests;
using RentCar.Application.Dtos.Responses;
using RentCar.Domain.Entities;


namespace RentCar.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Ejemplo de mapeo entidad → DTO
            CreateMap<Customer, CustomerDto>()
                .ForMember(
                dest => dest.FullName,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            // Taxes
            CreateMap<TaxRequestDto, Tax>();

            CreateMap<Tax, TaxDto>();
        }
    }
}