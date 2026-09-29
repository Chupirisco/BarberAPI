using System;
using System.Collections.Generic;
using System.Text;

namespace Barbearia.Domain.Entities
{
    public class AgendaServico
    {
        public ICollection<Agenda> Agendas { get; set; }

        public ICollection<Servico> Servicos { get; set; }
    }
}
