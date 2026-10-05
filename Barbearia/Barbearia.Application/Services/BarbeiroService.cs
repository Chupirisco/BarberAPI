using Barbearia.Application.DTOs;
using Barbearia.Application.Interfaces;
using Barbearia.Domain.Entities;
using Barbearia.Domain.Enums;
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
        public async Task<BarbeiroDTO> CreateAsync(CreateBarbeiroDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nome))
                throw new ArgumentException("Nome é obrigatório.");

            var barbeiro = new Barbeiro
            {
                Nome = dto.Nome,
                NumeroTel = dto.NumeroTel,
                Status = StatusAtivo.Ativo.ToString().ToLower()
            };

            await _repository.AddAsync(barbeiro);

            return new BarbeiroDTO
            {
                ID_Bar = barbeiro.ID_Bar,
                Nome = barbeiro.Nome,
                Status = barbeiro.Status,
                NumeroTel = barbeiro.NumeroTel
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateBarbeiroDTO dto)
        {
            var barbeiro = await _repository.GetByIdAsync(id);
            if (barbeiro == null) return false;

            if (string.IsNullOrWhiteSpace(dto.Nome))
                throw new ArgumentException("Nome é obrigatório.");

            barbeiro.Nome = dto.Nome;
            barbeiro.NumeroTel = dto.NumeroTel;

            await _repository.UpdateAsync(barbeiro);
            return true;
        }

        public async Task<bool> InativarAsync(int id)
        {
            var barbeiro = await _repository.GetByIdAsync(id);
            if (barbeiro == null) return false;

            barbeiro.Status = StatusAtivo.Inativo.ToString().ToLower();
            await _repository.UpdateAsync(barbeiro);
            return true;
        }
        public async Task<bool> AtivarAsync(int id)
        {
            var barbeiro = await _repository.GetByIdAsync(id);
            if (barbeiro == null) return false;

            barbeiro.Status = StatusAtivo.Ativo.ToString().ToLower();
            await _repository.UpdateAsync(barbeiro);
            return true;
        }
    }
}