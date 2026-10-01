using Barbearia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Barbearia.Domain.Enums;

namespace Barbearia.Infra.Data.Configurations
{
    public class AgendaConfiguration : IEntityTypeConfiguration<Agenda>
    {
        public void Configure(EntityTypeBuilder<Agenda> builder)
        {
            builder.ToTable("agenda");
            builder.HasKey(a => a.ID_Age);

            builder.Property(a => a.ID_Age).HasColumnName("id_age");
            builder.Property(a => a.DataHoraMarcada).HasColumnName("data_hora_marcada");

            builder.Property(a => a.StatusAgendamento)
                .HasColumnName("status_age")
                .HasConversion(
                    v => v.ToString().ToLower(),
                    v => (StatusAgenda)Enum.Parse(typeof(StatusAgenda), v, true))
                .HasColumnType("status_agenda");

            builder.Property(a => a.StatusPagamento)
                .HasColumnName("status_pag")
                .HasConversion(
                    v => v.ToString().ToLower(),
                    v => (StatusPagamento)Enum.Parse(typeof(StatusPagamento), v, true))
                .HasColumnType("status_pagamento");

            builder.Property(a => a.ID_Bar_FK).HasColumnName("id_bar_fk");
            builder.Property(a => a.ID_Cli_FK).HasColumnName("id_cli_fk");

            builder.HasOne(a => a.Cliente)
                .WithMany()
                .HasForeignKey(a => a.ID_Cli_FK);

            builder.HasOne(a => a.Barbeiro)
                .WithMany()
                .HasForeignKey(a => a.ID_Bar_FK);
        }
    }
}