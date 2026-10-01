using Barbearia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Barbearia.Domain.Interfaces
{
    public interface IClienteRepository
    {
        Task<Cliente?> GetByIdAsync(int id);
        Task<IEnumerable<Cliente>> GetAllAsync();
        Task AddAsync(Cliente cliente);
        Task UpdateAsync(Cliente cliente);
        Task DeleteAsync(int id);
    }
}