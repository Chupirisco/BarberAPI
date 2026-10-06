using Barbearia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barbearia.Infra.Data.Configurations
{
    public class TelefoneConfiguration : IEntityTypeConfiguration<Telefone>
    {
        public void Configure(EntityTypeBuilder<Telefone> builder)
        {
            builder.ToTable("telefone");
            builder.HasKey(t => t.ID_Tel);

            builder.Property(t => t.ID_Tel).HasColumnName("id_tel");
            builder.Property(t => t.NumeroTel).HasColumnName("numero_tel");
            builder.Property(t => t.ID_Cli_FK).HasColumnName("id_cli_fk");

            builder.HasOne(t => t.Cliente)
                .WithMany(c => c.Telefones)
                .HasForeignKey(t => t.ID_Cli_FK);
        }
    }
}