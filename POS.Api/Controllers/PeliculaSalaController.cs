using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using POS.Application.Dtos.Pelicula_SalaCine.Request;
using POS.Application.Interfaces;
using POS.Application.Services;

namespace POS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculaSalaController : ControllerBase
    {
        private readonly IStoredProcedureService _service;
        private readonly IPeliculaSalaApplication _application;

        public PeliculaSalaController(IStoredProcedureService service, IPeliculaSalaApplication application)
        {
            _service = service;
            _application = application;
        }

        [HttpGet("contar-peliculas-por-fecha")]
        public async Task<IActionResult> ContarPeliculasPorFecha(DateTime fechaPublicacion)
        {
            var cantidad = await _service.ContarPeliculasPorFechaAsync(fechaPublicacion);
            return Ok(new { cantidad });
        }

        [HttpGet("buscar-pelicula")]
        public async Task<IActionResult> BuscarPeliculaPorNombreYSala(string nombre, int idSalaCine)
        {
            var peliculas = await _service.BuscarPeliculaPorNombreYSalaAsync(nombre, idSalaCine);
            return Ok(peliculas);
        }

        [HttpGet("verificar-disponibilidad")]
        public async Task<IActionResult> VerificarDisponibilidadSala(string nombreSala)
        {
            var disponibilidad = await _service.VerificarDisponibilidadSalaAsync(nombreSala);
            return Ok(new { disponibilidad });
        }

        [HttpGet("Select")]
        public async Task<IActionResult> ListSelectPeliculaSala()
        {
            var response = await _application.ListSelectPeliculasSala();
            return Ok(response);
        }

        [HttpGet("{peliculaSalaId:int}")]
        public async Task<IActionResult> PeliculaSalaById(int peliculaSalaId)
        {
            var response = await _application.PeliculasSalaById(peliculaSalaId);
            return Ok(response);
        }

        [HttpPost("RegistrarPeliculaSala")]
        public async Task<IActionResult> RegistrarPeliculaSala([FromBody] PeliculaSalaRequestDto requestDto)
        {
            try
            {
                await _service.RegistrarPeliculaSalaAsync(
                    requestDto.Id_sala_cine,
                    requestDto.Id_pelicula,
                    requestDto.Fecha_publicacion,
                    requestDto.FechaFin
                );

                return Ok(new { IsSuccess = true, Message = "Película registrada correctamente en la sala." });
            }
            catch (SqlException ex)
            {
                return BadRequest(new { IsSuccess = false, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { IsSuccess = false, Message = ex.Message });
            }
        }

        [HttpPut("Edit/{peliculaSalaId:int}")]
        public async Task<IActionResult> EditPeliculaSala(int peliculaSalaId, [FromBody] PeliculaSalaRequestDto requestDto)
        {
            var response = await _application.EditPeliculaSala(peliculaSalaId, requestDto);
            return Ok(response);
        }

        [HttpPut("Remove/{peliculaSalaId:int}")]
        public async Task<IActionResult> RemovePeliculaSala(int peliculaSalaId)
        {
            var response = await _application.RemovePeliculaSala(peliculaSalaId);
            return Ok(response);
        }
    }
}
