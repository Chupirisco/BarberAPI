using Barbearia.Domain.Entities;
using Barbearia.Domain.Enums;
using Barbearia.Domain.Interfaces;
using Barbearia.Infra.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace Barbearia.Infra.Data.Repositories
{
    public class AgendaRepository : IAgendaRepository
    {
        private readonly AppDbContext _context;
        public AgendaRepository(AppDbContext context) => _context = context;

        private IQueryable<Agenda> Completa() =>
            _context.Agendas
                .Include(a => a.Cliente)
                .Include(a => a.Barbeiro)
                .Include(a => a.AgendaServicos).ThenInclude(x => x.Servico);

        public async Task<Agenda?> GetByIdAsync(int id) =>
            await Completa().FirstOrDefaultAsync(a => a.ID_Age == id);

        public async Task<IEnumerable<Agenda>> GetAllAsync() =>
            await Completa().ToListAsync();

        public async Task<IEnumerable<Agenda>> GetByDataAsync(DateTime data) =>
            await Completa().Where(a => a.DataHoraMarcada.Date == data.Date).ToListAsync();

        public async Task<IEnumerable<Agenda>> GetContasReceberAsync() =>
        await Completa()
            .Where(a => a.StatusAgendamento == "concluido"
                     && a.StatusPagamento == "pendente")
            .ToListAsync();

        public async Task AddAsync(Agenda agenda)
        {
            await _context.Agendas.AddAsync(agenda);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Agenda agenda)
        {
            _context.Agendas.Update(agenda);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var agenda = await _context.Agendas.FindAsync(id);
            if (agenda != null)
            {
                _context.Agendas.Remove(agenda);
                await _context.SaveChangesAsync();
            }
        }


    }
}