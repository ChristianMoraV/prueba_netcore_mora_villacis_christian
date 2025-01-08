using AutoMapper;
using POS.Application.Commons.Bases.Select.Response;
using POS.Application.Dtos.Pelicula.Request;
using POS.Application.Dtos.Pelicula.Response;
using POS.Domain.Entities;
using POS.Utilities.Static;

namespace POS.Application.Mappers
{
    public class PeliculaMappingsProfile: Profile
    {
        public PeliculaMappingsProfile()
        {
            CreateMap<Pelicula, PeliculaResponseDto>()
                .ForMember(x => x.Id_pelicula, x => x.MapFrom(y => y.Id))
                .ForMember(x => x.StateCategory, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Active) ? "Activo" : "Inactivo"))
                .ReverseMap();


            CreateMap<PeliculasRequestDto, Pelicula>();

            CreateMap<Pelicula, SelectResponse>()
                .ForMember(x => x.Description, x => x.MapFrom(y => y.Nombre))
                .ReverseMap();
        }
    }
}
