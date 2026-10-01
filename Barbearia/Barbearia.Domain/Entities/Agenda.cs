using Barbearia.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Barbearia.Domain.Entities
{
    public class Agenda
    {
        public int ID_Age { get; set; }
        public DateTime DataHoraMarcada { get; set; }
        public StatusAgenda StatusAgendamento { get; set; }
        public StatusPagamento StatusPagamento { get; set; }

        public int ID_Bar_FK { get; set; }
        public Barbeiro Barbeiro { get; set; }

        public int ID_Cli_FK { get; set; }
        public Cliente Cliente { get; set; }

        public ICollection<AgendaServico> AgendaServicos { get; set; } = new List<AgendaServico>();
    }
}
