using Barbearia.Application.DTOs;

namespace Barbearia.Application.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteDTO>> GetAllAsync();
        Task<ClienteDTO?> GetByIdAsync(int id);
        Task<ClienteDTO> CreateAsync(CreateClienteDTO dto);
        Task<bool> UpdateAsync(int id, UpdateClienteDTO dto);
        Task<bool> InativarAsync(int id);
        Task<bool> AtivarAsync(int id);

        // Telefone
        Task<bool> AddTelefoneAsync(int clienteId, string numero);
        Task<IEnumerable<TelefoneDTO>> GetTelefonesAsync(int clienteId);
        Task<bool> UpdateTelefoneAsync(int telefoneId, string numero);
        Task<bool> RemoveTelefoneAsync(int telefoneId);

    }
}