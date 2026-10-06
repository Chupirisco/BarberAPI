using System;
using System.Collections.Generic;
using System.Text;

namespace Barbearia.Application.DTOs
{
    public class UpdateAgendaDTO
    {
        public DateTime DataHoraMarcada { get; set; }
        public List<int> ServicoIds { get; set; }
    }
}
