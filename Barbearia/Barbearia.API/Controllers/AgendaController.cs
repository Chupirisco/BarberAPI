using Barbearia.Application.DTOs;
using Barbearia.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Barbearia.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendaController : ControllerBase
    {
        private readonly IAgendaService _agendaService;
        public AgendaController(IAgendaService agendaService) => _agendaService = agendaService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AgendaDTO>>> GetAll() =>
            Ok(await _agendaService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<AgendaDTO>> GetById(int id)
        {
            var agenda = await _agendaService.GetByIdAsync(id);
            if (agenda == null) return NotFound();
            return Ok(agenda);
        }

        [HttpGet("contas-receber")]
        public async Task<ActionResult<IEnumerable<AgendaDTO>>> GetContasReceber() =>
            Ok(await _agendaService.GetContasReceberAsync());
    }
}