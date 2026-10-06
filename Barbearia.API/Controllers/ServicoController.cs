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

        [HttpPost]
        public async Task<ActionResult<ServicoDTO>> Create(CreateServicoDTO dto)
        {
            try
            {
                var servico = await _servicoService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = servico.ID_Ser }, servico);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateServicoDTO dto)
        {
            try
            {
                var sucesso = await _servicoService.UpdateAsync(id, dto);
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
            var sucesso = await _servicoService.InativarAsync(id);
            if (!sucesso) return NotFound();
            return NoContent();
        }

        [HttpPatch("{id}/ativar")]
        public async Task<IActionResult> Ativar(int id)
        {
            var sucesso = await _servicoService.AtivarAsync(id);
            if (!sucesso) return NotFound();
            return NoContent();
        }
    }
}