using Barbearia.Application.DTOs;

namespace Barbearia.Application.Interfaces
{
    public interface IBarbeiroService
    {
        Task<IEnumerable<BarbeiroDTO>> GetAllAsync();
        Task<BarbeiroDTO?> GetByIdAsync(int id);
    }
}