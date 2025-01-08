using AutoMapper;
using POS.Application.Commons.Bases.Request;
using POS.Application.Commons.Bases.Response;
using POS.Application.Commons.Bases.Select.Response;
using POS.Application.Commons.Ordering;
using POS.Application.Dtos.Pelicula.Response;
using POS.Application.Dtos.Pelicula_SalaCine.Request;
using POS.Application.Dtos.Pelicula_SalaCine.Response;
using POS.Application.Interfaces;
using POS.Domain.Entities;
using POS.Infraestructure.Persistences.Interfaces;
using POS.Utilities.Static;

namespace POS.Application.Services
{
    public class PeliculaSalaApplication : IPeliculaSalaApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IOrderingQuery _orderingQuery;

        public PeliculaSalaApplication(IUnitOfWork unitOfWork, IMapper mapper, IOrderingQuery orderingQuery)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _orderingQuery = orderingQuery;
        }
        public Task<BaseResponse<IEnumerable<PeliculaSalaResponseDto>>> ListPeliculaSala(BaseFiltersRequest filters)
        {
            throw new NotImplementedException();
        }
        public async Task<BaseResponse<IEnumerable<SelectResponse>>> ListSelectPeliculasSala()
        {
            var response = new BaseResponse<IEnumerable<SelectResponse>>();
            var peliculasSala = await _unitOfWork.PeliculaSala.GetAllAsync();

            if (peliculasSala is not null)
            {
                response.IsSucces = true;
                response.Data = _mapper.Map<IEnumerable<SelectResponse>>(peliculasSala);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSucces = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            return response;
        }
        public async Task<BaseResponse<PeliculaSalaResponseDto>> PeliculasSalaById(int peliculaSalaId)
        {
            var response = new BaseResponse<PeliculaSalaResponseDto>();

            try
            {
                // Obtener PeliculaSala por Id desde el repositorio correspondiente
                var peliculaSala = await _unitOfWork.PeliculaSala.GetByIdAsync(peliculaSalaId);

                if (peliculaSala is null)
                {
                    response.IsSucces = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                // Mapear la entidad PeliculaSala al DTO PeliculaSalaResponseDto
                response.IsSucces = true;
                response.Data = _mapper.Map<PeliculaSalaResponseDto>(peliculaSala);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                response.IsSucces = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                response.Data = null;
            }

            return response;
        }

        public async Task<BaseResponse<bool>> RegistrarPeliculaSala(PeliculaSalaRequestDto requestDto)
        {
            throw new NotImplementedException();
        }


        public async Task<BaseResponse<bool>> EditPeliculaSala(int peliculaSalaId, PeliculaSalaRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();

            try
            {
                // Validar si el registro existe
                var peliculaSalaExistente = await _unitOfWork.PeliculaSala.GetByIdAsync(peliculaSalaId);

                if (peliculaSalaExistente == null)
                {
                    response.IsSucces = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                // Mapear los datos del DTO a la entidad existente
                var peliculaSala = _mapper.Map(requestDto, peliculaSalaExistente);

                // Actualizar la entidad
                response.Data = await _unitOfWork.PeliculaSala.EditAsync(peliculaSala);

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
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                response.IsSucces = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                response.Data = false;
            }

            return response;
        }

        public async Task<BaseResponse<bool>> RemovePeliculaSala(int peliculaSalaId)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var peliculas = await PeliculasSalaById(peliculaSalaId);
                response.Data = await _unitOfWork.PeliculaSala.RemoveAsync(peliculaSalaId);


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
