using Barbearia.Application.DTOs;

namespace Barbearia.Application.Interfaces
{
    public interface IAgendaService
    {
        Task<IEnumerable<AgendaDTO>> GetAllAsync();
        Task<AgendaDTO?> GetByIdAsync(int id);
        Task<IEnumerable<AgendaDTO>> GetContasReceberAsync();
    }
}