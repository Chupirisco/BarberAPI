using Barbearia.Domain.Entities;
using Barbearia.Domain.Interfaces;
using Barbearia.Infra.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace Barbearia.Infra.Data.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;
        public ClienteRepository(AppDbContext context) => _context = context;

        public async Task<Cliente?> GetByIdAsync(int id) =>
            await _context.Clientes.Include(c => c.Telefones).FirstOrDefaultAsync(c => c.ID_Cli == id);

        public async Task<IEnumerable<Cliente>> GetAllAsync() =>
            await _context.Clientes.Include(c => c.Telefones).ToListAsync();

        public async Task AddAsync(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }
        }
    }
}