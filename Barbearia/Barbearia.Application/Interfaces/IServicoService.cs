using Barbearia.Application.DTOs;

namespace Barbearia.Application.Interfaces
{
    public interface IServicoService
    {
        Task<IEnumerable<ServicoDTO>> GetAllAsync();
        Task<ServicoDTO?> GetByIdAsync(int id);
        Task<ServicoDTO> CreateAsync(CreateServicoDTO dto);
        Task<bool> UpdateAsync(int id, UpdateServicoDTO dto);
        Task<bool> InativarAsync(int id);
        Task<bool> AtivarAsync(int id);
    }
}