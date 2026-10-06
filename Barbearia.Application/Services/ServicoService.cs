using Barbearia.Application.DTOs;
using Barbearia.Application.Interfaces;
using Barbearia.Domain.Entities;
using Barbearia.Domain.Enums;
using Barbearia.Domain.Interfaces;

namespace Barbearia.Application.Services
{
    public class ServicoService : IServicoService
    {
        private readonly IServicoRepository _repository;
        public ServicoService(IServicoRepository repository) => _repository = repository;

        public async Task<IEnumerable<ServicoDTO>> GetAllAsync()
        {
            var servicos = await _repository.GetAllAsync();
            return servicos.Select(s => new ServicoDTO
            {
                ID_Ser = s.ID_Ser,
                Nome = s.Nome,
                Preco = s.Preco,
                Observacao = s.Observacao,
                Tempo = s.Tempo,
                Status = s.Status
            });
        }

        public async Task<ServicoDTO?> GetByIdAsync(int id)
        {
            var s = await _repository.GetByIdAsync(id);
            if (s == null) return null;

            return new ServicoDTO
            {
                ID_Ser = s.ID_Ser,
                Nome = s.Nome,
                Preco = s.Preco,
                Observacao = s.Observacao,
                Tempo = s.Tempo,
                Status = s.Status
            };
        }

        public async Task<ServicoDTO> CreateAsync(CreateServicoDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nome))
                throw new ArgumentException("Nome é obrigatório.");
            if (dto.Preco <= 0)
                throw new ArgumentException("Preço deve ser maior que zero.");

            var servico = new Servico
            {
                Nome = dto.Nome,
                Preco = dto.Preco,
                Observacao = dto.Observacao,
                Tempo = dto.Tempo,
                Status = StatusAtivo.Ativo.ToString().ToLower()
            };

            await _repository.AddAsync(servico);

            return new ServicoDTO
            {
                ID_Ser = servico.ID_Ser,
                Nome = servico.Nome,
                Preco = servico.Preco,
                Observacao = servico.Observacao,
                Tempo = servico.Tempo,
                Status = servico.Status
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateServicoDTO dto)
        {
            var servico = await _repository.GetByIdAsync(id);
            if (servico == null) return false;

            if (string.IsNullOrWhiteSpace(dto.Nome))
                throw new ArgumentException("Nome é obrigatório.");
            if (dto.Preco <= 0)
                throw new ArgumentException("Preço deve ser maior que zero.");

            servico.Nome = dto.Nome;
            servico.Preco = dto.Preco;
            servico.Observacao = dto.Observacao;
            servico.Tempo = dto.Tempo;

            await _repository.UpdateAsync(servico);
            return true;
        }

        public async Task<bool> InativarAsync(int id)
        {
            var servico = await _repository.GetByIdAsync(id);
            if (servico == null) return false;

            servico.Status = StatusAtivo.Inativo.ToString().ToLower();
            await _repository.UpdateAsync(servico);
            return true;
        }

        public async Task<bool> AtivarAsync(int id)
        {
            var servico = await _repository.GetByIdAsync(id);
            if (servico == null) return false;

            servico.Status = StatusAtivo.Ativo.ToString().ToLower();
            await _repository.UpdateAsync(servico);
            return true;
        }
    }
}