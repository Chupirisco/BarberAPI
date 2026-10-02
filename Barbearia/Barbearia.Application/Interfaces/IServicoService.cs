using Barbearia.Application.DTOs;

namespace Barbearia.Application.Interfaces
{
    public interface IServicoService
    {
        Task<IEnumerable<ServicoDTO>> GetAllAsync();
        Task<ServicoDTO?> GetByIdAsync(int id);
    }
}