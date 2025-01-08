using AutoMapper;
using Microsoft.EntityFrameworkCore;
using POS.Application.Commons.Bases.Request;
using POS.Application.Commons.Bases.Response;
using POS.Application.Commons.Bases.Select.Response;
using POS.Application.Commons.Ordering;
using POS.Application.Dtos.Pelicula.Request;
using POS.Application.Dtos.Pelicula.Response;
using POS.Application.Interfaces;
using POS.Domain.Entities;
using POS.Infraestructure.Persistences.Interfaces;
using POS.Utilities.Static;

namespace POS.Application.Services
{
    public class PeliculaApplication : IPeliculaApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IOrderingQuery _orderingQuery;

        public PeliculaApplication(IUnitOfWork unitOfWork, IMapper mapper, IOrderingQuery orderingQuery)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _orderingQuery = orderingQuery;
        }

        public async Task<BaseResponse<IEnumerable<PeliculaResponseDto>>> ListPeliculas(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<IEnumerable<PeliculaResponseDto>>();

            try
            {
                var peliculas = _unitOfWork.Pelicula.GetAllQueryble();

                if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
                {
                    switch (filters.NumFilter)
                    {
                        case 1:
                            peliculas = peliculas.Where(x => x.Nombre!.Contains(filters.TextFilter));
                            break;

                    }
                }

                if (filters.StateFilter is not null)
                {
                    peliculas = peliculas.Where(x => x.State.Equals(filters.StateFilter));
                }

                if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
                {
                    peliculas = peliculas.Where(x => x.AuditCreateDate >= Convert.ToDateTime(filters.StartDate) && x.AuditCreateDate <= Convert.ToDateTime(filters.EndDate).AddDays(1));
                }

                if (filters.Sort is null) filters.Sort = "Id";
                var items = await _orderingQuery.Ordering(filters, peliculas, !(bool)filters.Download!).ToListAsync();

                response.IsSucces = true;
                response.TotalRecords = await peliculas.CountAsync();
                response.Data = _mapper.Map<IEnumerable<PeliculaResponseDto>>(items);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            catch (Exception ex)
            {
                response.IsSucces = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
            }


            return response;
        }

        public async Task<BaseResponse<PeliculaResponseDto>> PeliculasById(int peliculaId)
        {

            var response = new BaseResponse<PeliculaResponseDto>();

            try
            {
                var pelicula = await _unitOfWork.Pelicula.GetByIdAsync(peliculaId);

                if (pelicula is null)
                {
                    response.IsSucces = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                response.IsSucces = true;
                response.Data = _mapper.Map<PeliculaResponseDto>(pelicula);
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

        public async Task<BaseResponse<bool>> RegistrarPelicula(PeliculasRequestDto requestDto)
        {

            var response = new BaseResponse<bool>();
            var peliculas = _mapper.Map<Pelicula>(requestDto);

            response.Data = await _unitOfWork.Pelicula.RegisterAsync(peliculas);

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
        public async Task<BaseResponse<bool>> EditPelicula(int peliculaId, PeliculasRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            var peliculaEdit = await PeliculasById(peliculaId);

            if (peliculaEdit.Data is null)
            {
                response.IsSucces = true;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }
            var pelicula = _mapper.Map<Pelicula>(requestDto);
            pelicula.Id = peliculaId;
            response.Data = await _unitOfWork.Pelicula.EditAsync(pelicula);

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

        public async Task<BaseResponse<bool>> RemovePelicula(int peliculaId)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var peliculas = await PeliculasById(peliculaId);
                response.Data = await _unitOfWork.Pelicula.RemoveAsync(peliculaId);


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

        public async Task<BaseResponse<IEnumerable<SelectResponse>>> ListSelectPeliculas()
        {

            var response = new BaseResponse<IEnumerable<SelectResponse>>();
            var peliculas= await _unitOfWork.Pelicula.GetAllAsync();

            if (peliculas is not null)
            {
                response.IsSucces = true;
                response.Data = _mapper.Map<IEnumerable<SelectResponse>>(peliculas);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSucces = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            return response;
        }
    }
}
