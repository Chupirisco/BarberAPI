using Barbearia.Application.DTOs;

namespace Barbearia.Application.Interfaces
{
    public interface IAgendaService
    {
        Task<IEnumerable<AgendaDTO>> GetAllAsync();
        Task<AgendaDTO?> GetByIdAsync(int id);
        Task<IEnumerable<AgendaDTO>> GetContasReceberAsync();
        Task<AgendaDTO> CreateAsync(CreateAgendaDTO dto);
        Task<bool> UpdateAsync(int id, UpdateAgendaDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> MudarStatusAgendamentoAsync(int id, string novoStatus);
        Task<bool> MudarStatusPagamentoAsync(int id, string novoStatus);
    }
}