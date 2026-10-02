using Barbearia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Barbearia.Domain.Enums;

namespace Barbearia.Infra.Data.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("cliente");
            builder.HasKey(c => c.ID_Cli);

            builder.Property(c => c.ID_Cli).HasColumnName("id_cli");
            builder.Property(c => c.Nome).HasColumnName("nome");
            builder.Property(c => c.Observacao).HasColumnName("observacao");

            builder.Property(c => c.Status)
             .HasColumnName("status")
             .HasConversion(
                 v => v.ToString().ToLower(),
                 v => (StatusAtivo)Enum.Parse(typeof(StatusAtivo), v, true))
             .HasColumnType("status_ativo");

            builder.HasMany(c => c.Telefones)
            .WithOne(t => t.Cliente)
            .HasForeignKey(t => t.ID_Cli_FK);
        }
    }
}