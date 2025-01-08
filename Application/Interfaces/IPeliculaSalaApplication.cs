using POS.Application.Commons.Bases.Request;
using POS.Application.Commons.Bases.Response;
using POS.Application.Commons.Bases.Select.Response;
using POS.Application.Dtos.Pelicula_SalaCine.Request;
using POS.Application.Dtos.Pelicula_SalaCine.Response;

namespace POS.Application.Interfaces
{
    public interface IPeliculaSalaApplication
    {
        Task<BaseResponse<IEnumerable<PeliculaSalaResponseDto>>> ListPeliculaSala(BaseFiltersRequest filters);
        Task<BaseResponse<IEnumerable<SelectResponse>>> ListSelectPeliculasSala();
        Task<BaseResponse<PeliculaSalaResponseDto>> PeliculasSalaById(int peliculaSalaId);
        Task<BaseResponse<bool>> RegistrarPeliculaSala(PeliculaSalaRequestDto requestDto);
        Task<BaseResponse<bool>> EditPeliculaSala(int peliculaSalaId, PeliculaSalaRequestDto requestDto);
        Task<BaseResponse<bool>> RemovePeliculaSala(int peliculaSalaId);
    }
}
