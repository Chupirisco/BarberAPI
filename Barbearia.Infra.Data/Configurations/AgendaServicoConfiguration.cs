using Barbearia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barbearia.Infra.Data.Configurations
{
    public class AgendaServicoConfiguration : IEntityTypeConfiguration<AgendaServico>
    {
        public void Configure(EntityTypeBuilder<AgendaServico> builder)
        {
            builder.ToTable("agenda_servico");
            builder.HasKey(x => new { x.ID_Age_FK, x.ID_Ser_FK });

            builder.Property(x => x.ID_Age_FK).HasColumnName("id_age_fk");
            builder.Property(x => x.ID_Ser_FK).HasColumnName("id_ser_fk");

            builder.HasOne(x => x.Agenda)
                .WithMany(a => a.AgendaServicos)
                .HasForeignKey(x => x.ID_Age_FK)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Servico)
                .WithMany(s => s.AgendaServicos)
                .HasForeignKey(x => x.ID_Ser_FK)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}