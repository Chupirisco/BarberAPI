using Barbearia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Barbearia.Domain.Interfaces
{
    public interface IBarbeiroRepository
    {
        Task<Barbeiro?> GetByIdAsync(int id);
        Task<IEnumerable<Barbeiro>> GetAllAsync();
        Task AddAsync(Barbeiro barbeiro);
        Task UpdateAsync(Barbeiro barbeiro);
        Task DeleteAsync(int id);
    }
}
