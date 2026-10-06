using Barbearia.Application.DTOs;
using Barbearia.Application.Interfaces;
using Barbearia.Domain.Entities;
using Barbearia.Domain.Enums;
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
        public async Task<AgendaDTO> CreateAsync(CreateAgendaDTO dto)
        {
            if (dto.ServicoIds == null || !dto.ServicoIds.Any())
                throw new ArgumentException("Selecione ao menos um serviço.");

            var agenda = new Agenda
            {
                ID_Cli_FK = dto.ID_Cli_FK,
                ID_Bar_FK = dto.ID_Bar_FK,
                DataHoraMarcada = dto.DataHoraMarcada,
                StatusAgendamento = StatusAgenda.Agendado.ToString().ToLower(),
                StatusPagamento = StatusPagamento.Pendente.ToString().ToLower(),
                AgendaServicos = dto.ServicoIds.Select(sId => new AgendaServico { ID_Ser_FK = sId }).ToList()
            };

            await _repository.AddAsync(agenda);

            var completa = await _repository.GetByIdAsync(agenda.ID_Age);
            return ToDTO(completa!);
        }

        public async Task<bool> UpdateAsync(int id, UpdateAgendaDTO dto)
        {
            var agenda = await _repository.GetByIdAsync(id);
            if (agenda == null) return false;

            if (dto.ServicoIds == null || !dto.ServicoIds.Any())
                throw new ArgumentException("Selecione ao menos um serviço.");

            agenda.DataHoraMarcada = dto.DataHoraMarcada;

            agenda.AgendaServicos.Clear();
            foreach (var sId in dto.ServicoIds)
                agenda.AgendaServicos.Add(new AgendaServico { ID_Age_FK = id, ID_Ser_FK = sId });

            await _repository.UpdateAsync(agenda);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var agenda = await _repository.GetByIdAsync(id);
            if (agenda == null) return false;

            await _repository.DeleteAsync(id);
            return true;
        }

        public async Task<bool> MudarStatusAgendamentoAsync(int id, string novoStatus)
        {
            if (!Enum.TryParse<StatusAgenda>(novoStatus, true, out var status))
                throw new ArgumentException("Status de agendamento inválido.");

            var agenda = await _repository.GetByIdAsync(id);
            if (agenda == null) return false;

            agenda.StatusAgendamento = status.ToString().ToLower();
            await _repository.UpdateAsync(agenda);
            return true;
        }

        public async Task<bool> MudarStatusPagamentoAsync(int id, string novoStatus)
        {
            if (!Enum.TryParse<StatusPagamento>(novoStatus, true, out var status))
                throw new ArgumentException("Status de pagamento inválido.");

            var agenda = await _repository.GetByIdAsync(id);
            if (agenda == null) return false;

            agenda.StatusPagamento = status.ToString().ToLower();
            await _repository.UpdateAsync(agenda);
            return true;
        }
    }
}