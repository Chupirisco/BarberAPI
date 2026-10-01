using System;
using System.Collections.Generic;
using System.Text;

namespace Barbearia.Domain.Entities
{
    public class AgendaServico
    {
        public int ID_Age_FK { get; set; }
        public Agenda Agenda { get; set; }

        public int ID_Ser_FK { get; set; }
        public Servico Servico { get; set; }


    }
}
