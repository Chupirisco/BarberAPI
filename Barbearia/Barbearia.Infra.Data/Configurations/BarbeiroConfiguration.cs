using Barbearia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Barbearia.Domain.Enums;

namespace Barbearia.Infra.Data.Configurations
{
    public class BarbeiroConfiguration : IEntityTypeConfiguration<Barbeiro>
    {
        public void Configure(EntityTypeBuilder<Barbeiro> builder)
        {
            builder.ToTable("barbeiro");
            builder.HasKey(b => b.ID_Bar);

            builder.Property(b => b.ID_Bar).HasColumnName("id_bar");
            builder.Property(b => b.Nome).HasColumnName("nome");
            builder.Property(b => b.NumeroTel).HasColumnName("numero_tel");

            builder.Property(b => b.Status).HasColumnName("status");
        }
    }
}