using AutoMapper;
using Microsoft.EntityFrameworkCore;
using POS.Application.Commons.Bases.Request;
using POS.Application.Commons.Bases.Response;
using POS.Application.Commons.Bases.Select.Response;
using POS.Application.Commons.Ordering;
using POS.Application.Dtos.SalaCine.Request;
using POS.Application.Dtos.SalaCine.Response;
using POS.Application.Interfaces;
using POS.Domain.Entities;
using POS.Infraestructure.Persistences.Interfaces;
using POS.Utilities.Static;

namespace POS.Application.Services
{
    public class SalaCineApplication : ISalaCineApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IOrderingQuery _orderingQuery;

        public SalaCineApplication(IUnitOfWork unitOfWork, IMapper mapper, IOrderingQuery orderingQuery)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _orderingQuery = orderingQuery;
        }
        public async Task<BaseResponse<IEnumerable<SalaCineResponseDto>>> ListSalaCine(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<IEnumerable<SalaCineResponseDto>>();

            try
            {
                var salaCine = _unitOfWork.SalaCine.GetAllQueryble();

                if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
                {
                    switch (filters.NumFilter)
                    {
                        case 1:
                            salaCine = salaCine.Where(x => x.Nombre!.Contains(filters.TextFilter));
                            break;

                    }
                }

                if (filters.StateFilter is not null)
                {
                    salaCine = salaCine.Where(x => x.State.Equals(filters.StateFilter));
                }

                if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
                {
                    salaCine = salaCine.Where(x => x.AuditCreateDate >= Convert.ToDateTime(filters.StartDate) && x.AuditCreateDate <= Convert.ToDateTime(filters.EndDate).AddDays(1));
                }

                if (filters.Sort is null) filters.Sort = "Id";
                var items = await _orderingQuery.Ordering(filters, salaCine, !(bool)filters.Download!).ToListAsync();

                response.IsSucces = true;
                response.TotalRecords = await salaCine.CountAsync();
                response.Data = _mapper.Map<IEnumerable<SalaCineResponseDto>>(items);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            catch (Exception ex)
            {
                response.IsSucces = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
            }


            return response;
        }

        public async Task<BaseResponse<IEnumerable<SelectResponse>>> ListSelectSalaCine()
        {
            var response = new BaseResponse<IEnumerable<SelectResponse>>();
            var salaCine = await _unitOfWork.SalaCine.GetAllAsync();

            if (salaCine is not null)
            {
                response.IsSucces = true;
                response.Data = _mapper.Map<IEnumerable<SelectResponse>>(salaCine);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSucces = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            return response;
        }

        public async Task<BaseResponse<SalaCineResponseDto>> SalaCineById(int salaId)
        {
            var response = new BaseResponse<SalaCineResponseDto>();

            try
            {
                var salaCine = await _unitOfWork.SalaCine.GetByIdAsync(salaId);

                if (salaCine is null)
                {
                    response.IsSucces = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                response.IsSucces = true;
                response.Data = _mapper.Map<SalaCineResponseDto>(salaCine);
                response.Message = ReplyMessage.MESSAGE_QUERY;

            }
            catch (Exception ex)
            {
                response.IsSucces = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                return response;
            }

            return response;
        }

        public async Task<BaseResponse<bool>> RegistrarSalaCine(SalaCineRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            var salaCine = _mapper.Map<SalaCine>(requestDto);

            response.Data = await _unitOfWork.SalaCine.RegisterAsync(salaCine);

            if (response.Data)
            {
                response.IsSucces = true;
                response.Message = ReplyMessage.MESSAGE_SAVE;
            }
            else
            {
                response.IsSucces = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }
            return response;
        }

        public async Task<BaseResponse<bool>> EditSalaCine(int salaId, SalaCineRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            var editsalaCine = await SalaCineById(salaId);

            if (editsalaCine.Data is null)
            {
                response.IsSucces = true;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }
            var salacine = _mapper.Map<SalaCine>(requestDto);
            salacine.Id = salaId;
            response.Data = await _unitOfWork.SalaCine.EditAsync(salacine);

            if (response.Data)
            {
                response.IsSucces = true;
                response.Message = ReplyMessage.MESSAGE_UPDATE;
            }
            else
            {
                response.IsSucces = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }
            return response;
        }

        public async Task<BaseResponse<bool>> RemoveSalaCine(int salaId)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var salaCine = await SalaCineById(salaId);
                response.Data = await _unitOfWork.SalaCine.RemoveAsync(salaId);


                if (!response.Data)
                {
                    response.IsSucces = false;
                    response.Message = ReplyMessage.MESSAGE_FAILED;
                    return response;
                }

                response.IsSucces = true;
                response.Message = ReplyMessage.MESSAGE_DELETE;

            }
            catch (Exception ex)
            {
                response.IsSucces = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
            }
            return response;
        }

       
    }
}
