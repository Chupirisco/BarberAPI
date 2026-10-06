using Barbearia.Application.DTOs;
using Barbearia.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Barbearia.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        public ClienteController(IClienteService clienteService) => _clienteService = clienteService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> GetAll() =>
            Ok(await _clienteService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDTO>> GetById(int id)
        {
            var cliente = await _clienteService.GetByIdAsync(id);
            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

        [HttpGet("{id}/telefones")]
        public async Task<ActionResult<IEnumerable<TelefoneDTO>>> GetTelefones(int id) =>
        Ok(await _clienteService.GetTelefonesAsync(id));

        [HttpPost]
        public async Task<ActionResult<ClienteDTO>> Create(CreateClienteDTO dto)
        {
            try
            {
                var cliente = await _clienteService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = cliente.ID_Cli }, cliente);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateClienteDTO dto)
        {
            try
            {
                var sucesso = await _clienteService.UpdateAsync(id, dto);
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
            var sucesso = await _clienteService.InativarAsync(id);
            if (!sucesso) return NotFound();
            return NoContent();
        }

        [HttpPost("{id}/telefones")]
        public async Task<IActionResult> AddTelefone(int id, [FromBody] string numero)
        {
            var sucesso = await _clienteService.AddTelefoneAsync(id, numero);
            if (!sucesso) return NotFound();
            return Ok();
        }

        [HttpPut("{id}/telefones/{telefoneId}")]
        public async Task<IActionResult> UpdateTelefone(int id, int telefoneId, [FromBody] string numero)
        {
            var sucesso = await _clienteService.UpdateTelefoneAsync(telefoneId, numero);
            if (!sucesso) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}/telefones/{telefoneId}")]
        public async Task<IActionResult> DeleteTelefone(int id, int telefoneId)
        {
            var sucesso = await _clienteService.RemoveTelefoneAsync(telefoneId);
            if (!sucesso) return NotFound();
            return NoContent();
        }

        [HttpPatch("{id}/ativar")]
        public async Task<IActionResult> Ativar(int id)
        {
            var sucesso = await _clienteService.AtivarAsync(id);
            if (!sucesso) return NotFound();
            return NoContent();
        }
    }
}