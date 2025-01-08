using AutoMapper;
using POS.Application.Commons.Bases.Select.Response;
using POS.Application.Dtos.Pelicula.Request;
using POS.Application.Dtos.SalaCine.Request;
using POS.Application.Dtos.SalaCine.Response;
using POS.Domain.Entities;
using POS.Utilities.Static;

namespace POS.Application.Mappers
{
    public  class SalaCineMappingsProfile: Profile
    {
        public SalaCineMappingsProfile()
        {

            CreateMap<SalaCine, SalaCineResponseDto>()
                .ForMember(x => x.Id_sala, x => x.MapFrom(y => y.Id))
                .ForMember(x => x.StateCategory, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Active) ? "Activo" : "Inactivo"))
                .ReverseMap();


            CreateMap<SalaCineRequestDto, SalaCine>();

            CreateMap<SalaCine, SelectResponse>()
                .ForMember(x => x.Description, x => x.MapFrom(y => y.Nombre))
                .ReverseMap();
        }
    }
}
