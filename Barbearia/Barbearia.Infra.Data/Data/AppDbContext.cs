using Barbearia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Barbearia.Infra.Data.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Barbeiro> Barbeiros { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<Telefone> Telefones { get; set; }
        public DbSet<Agenda> Agendas { get; set; }
        public DbSet<AgendaServico> AgendaServicos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}