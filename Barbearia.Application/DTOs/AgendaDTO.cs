using System;
using System.Collections.Generic;
using System.Text;

namespace Barbearia.Application.DTOs
{
    public class AgendaDTO
    {
        public int ID_Age { get; set; }
        public DateTime DataHoraMarcada { get; set; }
        public string StatusAgendamento { get; set; }
        public string StatusPagamento { get; set; }
        public string NomeCliente { get; set; }
        public string NomeBarbeiro { get; set; }
        public List<string> Servicos { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
