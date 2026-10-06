using Barbearia.Domain.Entities;
using Barbearia.Domain.Interfaces;
using Barbearia.Infra.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace Barbearia.Infra.Data.Repositories
{
    public class BarbeiroRepository : IBarbeiroRepository
    {
        private readonly AppDbContext _context;
        public BarbeiroRepository(AppDbContext context) => _context = context;

        public async Task<Barbeiro?> GetByIdAsync(int id) =>
            await _context.Barbeiros.FindAsync(id);

        public async Task<IEnumerable<Barbeiro>> GetAllAsync() =>
            await _context.Barbeiros.ToListAsync();

        public async Task AddAsync(Barbeiro barbeiro)
        {
            await _context.Barbeiros.AddAsync(barbeiro);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Barbeiro barbeiro)
        {
            _context.Barbeiros.Update(barbeiro);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var barbeiro = await _context.Barbeiros.FindAsync(id);
            if (barbeiro != null)
            {
                _context.Barbeiros.Remove(barbeiro);
                await _context.SaveChangesAsync();
            }
        }
    }
}