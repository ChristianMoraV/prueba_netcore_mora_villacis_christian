using POS.Application.Commons.Bases.Request;
using POS.Application.Commons.Bases.Response;
using POS.Application.Commons.Bases.Select.Response;
using POS.Application.Dtos.Pelicula.Request;
using POS.Application.Dtos.Pelicula.Response;

namespace POS.Application.Interfaces
{
    public interface IPeliculaApplication
    {
        Task<BaseResponse<IEnumerable<PeliculaResponseDto>>> ListPeliculas(BaseFiltersRequest filters);
        Task<BaseResponse<IEnumerable<SelectResponse>>> ListSelectPeliculas();
        Task<BaseResponse<PeliculaResponseDto>> PeliculasById(int peliculaId);
        Task<BaseResponse<bool>> RegistrarPelicula(PeliculasRequestDto requestDto);
        Task<BaseResponse<bool>> EditPelicula(int peliculaId, PeliculasRequestDto requestDto);
        Task<BaseResponse<bool>> RemovePelicula(int peliculaId);
    }
}
