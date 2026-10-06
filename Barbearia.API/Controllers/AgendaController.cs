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

        [HttpPost]
        public async Task<ActionResult<AgendaDTO>> Create(CreateAgendaDTO dto)
        {
            try
            {
                var agenda = await _agendaService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = agenda.ID_Age }, agenda);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateAgendaDTO dto)
        {
            try
            {
                var sucesso = await _agendaService.UpdateAsync(id, dto);
                if (!sucesso) return NotFound();
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sucesso = await _agendaService.DeleteAsync(id);
            if (!sucesso) return NotFound();
            return NoContent();
        }

        [HttpPatch("{id}/status-agendamento")]
        public async Task<IActionResult> MudarStatusAgendamento(int id, [FromBody] string novoStatus)
        {
            try
            {
                var sucesso = await _agendaService.MudarStatusAgendamentoAsync(id, novoStatus);
                if (!sucesso) return NotFound();
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}/status-pagamento")]
        public async Task<IActionResult> MudarStatusPagamento(int id, [FromBody] string novoStatus)
        {
            try
            {
                var sucesso = await _agendaService.MudarStatusPagamentoAsync(id, novoStatus);
                if (!sucesso) return NotFound();
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}