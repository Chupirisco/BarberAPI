using Barbearia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Barbearia.Domain.Interfaces
{
    public interface IAgendaServicoRepository
    {
        Task<IEnumerable<AgendaServico>> GetByAgendaIdAsync(int agendaId);
        Task AddAsync(AgendaServico agendaServico);
        Task RemoveAsync(int agendaId, int servicoId);
    }
}