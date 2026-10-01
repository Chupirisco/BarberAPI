using Barbearia.Domain.Entities;
using Barbearia.Domain.Interfaces;
using Barbearia.Infra.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace Barbearia.Infra.Data.Repositories
{
    public class ServicoRepository : IServicoRepository
    {
        private readonly AppDbContext _context;
        public ServicoRepository(AppDbContext context) => _context = context;

        public async Task<Servico?> GetByIdAsync(int id) =>
            await _context.Servicos.FindAsync(id);

        public async Task<IEnumerable<Servico>> GetAllAsync() =>
            await _context.Servicos.ToListAsync();

        public async Task AddAsync(Servico servico)
        {
            await _context.Servicos.AddAsync(servico);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Servico servico)
        {
            _context.Servicos.Update(servico);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var servico = await _context.Servicos.FindAsync(id);
            if (servico != null)
            {
                _context.Servicos.Remove(servico);
                await _context.SaveChangesAsync();
            }
        }
    }
}