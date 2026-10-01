using Barbearia.Domain.Entities;
using Barbearia.Domain.Interfaces;
using Barbearia.Infra.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace Barbearia.Infra.Data.Repositories
{
    public class TelefoneRepository : ITelefoneRepository
    {
        private readonly AppDbContext _context;
        public TelefoneRepository(AppDbContext context) => _context = context;

        public async Task<Telefone?> GetByIdAsync(int id) =>
            await _context.Telefones.FindAsync(id);

        public async Task<IEnumerable<Telefone>> GetByClienteIdAsync(int clienteId) =>
            await _context.Telefones.Where(t => t.ID_Cli_FK == clienteId).ToListAsync();

        public async Task AddAsync(Telefone telefone)
        {
            await _context.Telefones.AddAsync(telefone);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Telefone telefone)
        {
            _context.Telefones.Update(telefone);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var telefone = await _context.Telefones.FindAsync(id);
            if (telefone != null)
            {
                _context.Telefones.Remove(telefone);
                await _context.SaveChangesAsync();
            }
        }
    }
}