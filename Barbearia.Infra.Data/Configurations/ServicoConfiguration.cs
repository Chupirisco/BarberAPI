using Barbearia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barbearia.Infra.Data.Configurations
{
    public class ServicoConfiguration : IEntityTypeConfiguration<Servico>
    {
        public void Configure(EntityTypeBuilder<Servico> builder)
        {
            builder.ToTable("servico");
            builder.HasKey(s => s.ID_Ser);

            builder.Property(s => s.ID_Ser).HasColumnName("id_ser");
            builder.Property(s => s.Nome).HasColumnName("nome");
            builder.Property(s => s.Preco).HasColumnName("preco").HasColumnType("numeric");
            builder.Property(s => s.Observacao).HasColumnName("observacao");
            builder.Property(s => s.Tempo).HasColumnName("tempo");
            builder.Property(s => s.Status).HasColumnName("status");
        }
    }
}