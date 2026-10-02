using Barbearia.Application.DTOs;
using Barbearia.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Barbearia.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicoController : ControllerBase
    {
        private readonly IServicoService _servicoService;
        public ServicoController(IServicoService servicoService) => _servicoService = servicoService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServicoDTO>>> GetAll() =>
            Ok(await _servicoService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<ServicoDTO>> GetById(int id)
        {
            var servico = await _servicoService.GetByIdAsync(id);
            if (servico == null) return NotFound();
            return Ok(servico);
        }
    }
}