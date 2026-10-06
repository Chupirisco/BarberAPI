using Barbearia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Barbearia.Domain.Interfaces
{
    public interface IServicoRepository
    {
        Task<Servico?> GetByIdAsync(int id);
        Task<IEnumerable<Servico>> GetAllAsync();
        Task AddAsync(Servico servico);
        Task UpdateAsync(Servico servico);
        Task DeleteAsync(int id);
    }
}
