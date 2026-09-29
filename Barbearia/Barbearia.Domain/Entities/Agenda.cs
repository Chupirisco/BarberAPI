using System;
using System.Collections.Generic;
using System.Text;

namespace Barbearia.Domain.Entities
{
    public class Agenda
    {
        public int ID_Age { get; set; }

        public DateTime DataHoraMarcada { get; set; }

        public string StatusAgendamento { get; set; }

        
        public string StatusPagamento { get; set; }

        public ICollection<Cliente> Cliente { get; set; }
    
        public ICollection<Barbeiro> Barbeiros { get; set; }


        public ICollection<Servico> Servicos { get; set; }

    }
}
