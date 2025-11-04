using AutoMapper;


namespace RentCar.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Ejemplo de mapeo entidad → DTO
            //CreateMap<Cliente, ClienteDto>()
            //    .ForMember(
            //    dest => dest.NombreCompleto,
            //    opt => opt.MapFrom(src => $"{src.Nombre} {src.Apellidos}"));
        }
    }
}