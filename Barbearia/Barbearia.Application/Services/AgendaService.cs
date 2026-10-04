using Barbearia.Application.DTOs;
using Barbearia.Application.Interfaces;
using Barbearia.Domain.Entities;
using Barbearia.Domain.Interfaces;

namespace Barbearia.Application.Services
{
    public class AgendaService : IAgendaService
    {
        private readonly IAgendaRepository _repository;
        public AgendaService(IAgendaRepository repository) => _repository = repository;

        private static AgendaDTO ToDTO(Agenda a) => new AgendaDTO
        {
            ID_Age = a.ID_Age,
            DataHoraMarcada = a.DataHoraMarcada,
            StatusAgendamento = a.StatusAgendamento,
            StatusPagamento = a.StatusPagamento,
            NomeCliente = a.Cliente.Nome,
            NomeBarbeiro = a.Barbeiro.Nome,
            Servicos = a.AgendaServicos.Select(x => x.Servico.Nome).ToList(),
            ValorTotal = a.AgendaServicos.Sum(x => x.Servico.Preco)
        };

        public async Task<IEnumerable<AgendaDTO>> GetAllAsync()
        {
            var agendas = await _repository.GetAllAsync();
            return agendas.Select(ToDTO);
        }

        public async Task<AgendaDTO?> GetByIdAsync(int id)
        {
            var a = await _repository.GetByIdAsync(id);
            return a == null ? null : ToDTO(a);
        }

        public async Task<IEnumerable<AgendaDTO>> GetContasReceberAsync()
        {
            var agendas = await _repository.GetContasReceberAsync();
            return agendas.Select(ToDTO);
        }
    }
}