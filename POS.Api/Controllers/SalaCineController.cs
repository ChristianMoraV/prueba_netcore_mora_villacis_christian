using Microsoft.AspNetCore.Mvc;
using POS.Application.Commons.Bases.Request;
using POS.Application.Dtos.SalaCine.Request;
using POS.Application.Interfaces;

namespace POS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaCineController : ControllerBase
    {
        private readonly ISalaCineApplication _salaCineApplication;

        public SalaCineController(ISalaCineApplication salaCineApplication)
        {
            _salaCineApplication = salaCineApplication;
        }

        [HttpGet]
        public async Task<IActionResult> ListSalaCine([FromQuery] BaseFiltersRequest filters)
        {
            var response = await _salaCineApplication.ListSalaCine(filters);
            return Ok(response);
        }

        [HttpGet("Select")]
        public async Task<IActionResult> ListSelectSalaCine()
        {
            var response = await _salaCineApplication.ListSelectSalaCine();
            return Ok(response);
        }

        [HttpGet("{salaId:int}")]
        public async Task<IActionResult> SalaCineById(int salaId)
        {
            var response = await _salaCineApplication.SalaCineById(salaId);
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterSalaCine([FromBody] SalaCineRequestDto requestDto)
        {
            var response = await _salaCineApplication.RegistrarSalaCine(requestDto);
            return Ok(response);
        }

        [HttpPut("Edit/{salaId:int}")]
        public async Task<IActionResult> EditSalaCine(int salaId, [FromBody] SalaCineRequestDto requestDto)
        {
            var response = await _salaCineApplication.EditSalaCine(salaId, requestDto);
            return Ok(response);
        }

        [HttpPut("Remove/{salaId:int}")]
        public async Task<IActionResult> RemovePelicula(int salaId)
        {
            var response = await _salaCineApplication.RemoveSalaCine(salaId);
            return Ok(response);
        }
    }
}
