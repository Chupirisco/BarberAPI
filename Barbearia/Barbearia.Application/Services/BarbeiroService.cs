using Barbearia.Application.DTOs;
using Barbearia.Application.Interfaces;
using Barbearia.Domain.Interfaces;

namespace Barbearia.Application.Services
{
    public class BarbeiroService : IBarbeiroService
    {
        private readonly IBarbeiroRepository _repository;
        public BarbeiroService(IBarbeiroRepository repository) => _repository = repository;

        public async Task<IEnumerable<BarbeiroDTO>> GetAllAsync()
        {
            var barbeiros = await _repository.GetAllAsync();
            return barbeiros.Select(b => new BarbeiroDTO
            {
                ID_Bar = b.ID_Bar,
                Nome = b.Nome,
                Status = b.Status.ToString(),
                NumeroTel = b.NumeroTel
            });
        }

        public async Task<BarbeiroDTO?> GetByIdAsync(int id)
        {
            var b = await _repository.GetByIdAsync(id);
            if (b == null) return null;

            return new BarbeiroDTO
            {
                ID_Bar = b.ID_Bar,
                Nome = b.Nome,
                Status = b.Status.ToString(),
                NumeroTel = b.NumeroTel
            };
        }
    }
}