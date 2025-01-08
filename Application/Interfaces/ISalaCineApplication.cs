using POS.Application.Commons.Bases.Request;
using POS.Application.Commons.Bases.Response;
using POS.Application.Commons.Bases.Select.Response;
using POS.Application.Dtos.SalaCine.Request;
using POS.Application.Dtos.SalaCine.Response;

namespace POS.Application.Interfaces
{
    public interface ISalaCineApplication
    {
        Task<BaseResponse<IEnumerable<SalaCineResponseDto>>> ListSalaCine(BaseFiltersRequest filters);
        Task<BaseResponse<IEnumerable<SelectResponse>>> ListSelectSalaCine();
        Task<BaseResponse<SalaCineResponseDto>> SalaCineById(int salaId);
        Task<BaseResponse<bool>> RegistrarSalaCine(SalaCineRequestDto requestDto);
        Task<BaseResponse<bool>> EditSalaCine(int salaId, SalaCineRequestDto requestDto);
        Task<BaseResponse<bool>> RemoveSalaCine(int salaId);
    }
}
