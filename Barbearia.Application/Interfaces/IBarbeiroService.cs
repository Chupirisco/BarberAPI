using Barbearia.Application.DTOs;

namespace Barbearia.Application.Interfaces
{
    public interface IBarbeiroService
    {
        Task<IEnumerable<BarbeiroDTO>> GetAllAsync();
        Task<BarbeiroDTO?> GetByIdAsync(int id);
        Task<BarbeiroDTO> CreateAsync(CreateBarbeiroDTO dto);
        Task<bool> UpdateAsync(int id, UpdateBarbeiroDTO dto);
        Task<bool> InativarAsync(int id);
        Task<bool> AtivarAsync(int id);
    }
}