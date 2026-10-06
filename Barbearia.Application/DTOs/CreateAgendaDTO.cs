using System;
using System.Collections.Generic;
using System.Text;

namespace Barbearia.Application.DTOs
{
    public class CreateAgendaDTO
    {
        public int ID_Cli_FK { get; set; }
        public int ID_Bar_FK { get; set; }
        public DateTime DataHoraMarcada { get; set; }
        public List<int> ServicoIds { get; set; }
    }
}
