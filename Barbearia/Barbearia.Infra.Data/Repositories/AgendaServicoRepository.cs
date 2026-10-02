using Barbearia.Domain.Entities;
using Barbearia.Domain.Interfaces;
using Barbearia.Infra.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace Barbearia.Infra.Data.Repositories
{
    public class AgendaServicoRepository : IAgendaServicoRepository
    {
        private readonly AppDbContext _context;
        public AgendaServicoRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<AgendaServico>> GetByAgendaIdAsync(int agendaId) =>
        await _context.AgendaServicos.Include(x => x.Servico)
        .Where(x => x.ID_Age_FK == agendaId).ToListAsync();

        public async Task AddAsync(AgendaServico agendaServico)
        {
            await _context.AgendaServicos.AddAsync(agendaServico);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int agendaId, int servicoId)
        {
            var item = await _context.AgendaServicos.FindAsync(agendaId, servicoId);
            if (item != null)
            {
                _context.AgendaServicos.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}