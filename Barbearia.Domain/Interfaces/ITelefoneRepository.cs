using Barbearia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Barbearia.Domain.Interfaces
{
    public interface ITelefoneRepository
    {
        Task<Telefone?> GetByIdAsync(int id);
        Task<IEnumerable<Telefone>> GetByClienteIdAsync(int clienteId);
        Task AddAsync(Telefone telefone);
        Task UpdateAsync(Telefone telefone);
        Task DeleteAsync(int id);
    }
}
