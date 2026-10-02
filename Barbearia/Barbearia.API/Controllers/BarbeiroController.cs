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
    }
}