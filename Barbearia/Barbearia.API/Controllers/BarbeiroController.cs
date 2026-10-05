using Barbearia.Application.DTOs;
using Barbearia.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Barbearia.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BarbeiroController : ControllerBase
    {
        private readonly IBarbeiroService _barbeiroService;
        public BarbeiroController(IBarbeiroService barbeiroService) => _barbeiroService = barbeiroService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BarbeiroDTO>>> GetAll() =>
            Ok(await _barbeiroService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<BarbeiroDTO>> GetById(int id)
        {
            var barbeiro = await _barbeiroService.GetByIdAsync(id);
            if (barbeiro == null) return NotFound();
            return Ok(barbeiro);
        }

        [HttpPost]
        public async Task<ActionResult<BarbeiroDTO>> Create(CreateBarbeiroDTO dto)
        {
            try
            {
                var barbeiro = await _barbeiroService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = barbeiro.ID_Bar }, barbeiro);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateBarbeiroDTO dto)
        {
            try
            {
                var sucesso = await _barbeiroService.UpdateAsync(id, dto);
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
            var sucesso = await _barbeiroService.InativarAsync(id);
            if (!sucesso) return NotFound();
            return NoContent();
        }

        [HttpPatch("{id}/ativar")]
        public async Task<IActionResult> Ativar(int id)
        {
            var sucesso = await _barbeiroService.AtivarAsync(id);
            if (!sucesso) return NotFound();
            return NoContent();
        }
    }
}