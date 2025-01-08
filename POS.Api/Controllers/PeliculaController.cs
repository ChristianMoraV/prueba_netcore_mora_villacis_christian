using Microsoft.AspNetCore.Mvc;
using POS.Application.Commons.Bases.Request;
using POS.Application.Dtos.Pelicula.Request;
using POS.Application.Interfaces;

namespace POS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculaController : ControllerBase
    {
        private readonly IPeliculaApplication _peliculaApplication;

        public PeliculaController(IPeliculaApplication peliculaApplication)
        {
            _peliculaApplication = peliculaApplication;
        }

        [HttpGet]
        public async Task<IActionResult> ListPeliculas([FromQuery] BaseFiltersRequest filters)
        {
            var response = await _peliculaApplication.ListPeliculas(filters);
            return Ok(response);
        }

        [HttpGet("Select")]
        public async Task<IActionResult> ListSelectPelicula()
        {
            var response = await _peliculaApplication.ListSelectPeliculas();
            return Ok(response);
        }

        [HttpGet("{peliculaId:int}")]
        public async Task<IActionResult> PeliculaById(int peliculaId)
        {
            var response = await _peliculaApplication.PeliculasById(peliculaId);
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterPelicula([FromBody] PeliculasRequestDto requestDto)
        {
            var response = await _peliculaApplication.RegistrarPelicula(requestDto);
            return Ok(response);
        }

        [HttpPut("Edit/{peliculaId:int}")]
        public async Task<IActionResult> EditPelicula(int peliculaId, [FromBody] PeliculasRequestDto requestDto)
        {
            var response = await _peliculaApplication.EditPelicula(peliculaId, requestDto);
            return Ok(response);
        }

        [HttpPut("Remove/{peliculaId:int}")]
        public async Task<IActionResult> RemovePelicula(int peliculaId)
        {
            var response = await _peliculaApplication.RemovePelicula(peliculaId);
            return Ok(response);
        }
    }
}
