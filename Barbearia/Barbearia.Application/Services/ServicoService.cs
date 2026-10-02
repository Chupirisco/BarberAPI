using Barbearia.Application.DTOs;
using Barbearia.Application.Interfaces;
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
                Tempo = s.Tempo
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
                Tempo = s.Tempo
            };
        }
    }
}