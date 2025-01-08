using AutoMapper;
using POS.Application.Commons.Bases.Select.Response;
using POS.Application.Dtos.Pelicula_SalaCine.Request;
using POS.Application.Dtos.Pelicula_SalaCine.Response;
using POS.Domain.Entities;
using POS.Utilities.Static;

namespace POS.Application.Mappers
{
    public class PeliculaSalaMappingsProfile:Profile
    {
        public PeliculaSalaMappingsProfile() 
        {
            CreateMap<PeliculaSala, PeliculaSalaResponseDto>()
                .ForMember(dest => dest.Id_pelicula_sala, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id_sala_cine, opt => opt.MapFrom(src => src.IdSalaCine))
                .ForMember(dest => dest.Id_pelicula, opt => opt.MapFrom(src => src.IdPelicula))
                .ForMember(dest => dest.Fecha_publicacion, opt => opt.MapFrom(src => src.FechaPublicacion))
                .ForMember(dest => dest.Fecha_fin, opt => opt.MapFrom(src => src.FechaFin))
                .ForMember(dest => dest.StateCategory, opt => opt.MapFrom(src => src.State == (int)StateTypes.Active ? "Activo" : "Inactivo"))
                .ReverseMap();



            CreateMap<PeliculaSalaRequestDto, PeliculaSala>();

            CreateMap<PeliculaSala, SelectResponse>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src =>
                src.IdPeliculaNavigation.Nombre)) // Obtener el nombre de la película
                .ReverseMap(); 
        }
    }
}
