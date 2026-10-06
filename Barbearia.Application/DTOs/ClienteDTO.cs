using System;
using System.Collections.Generic;
using System.Text;

namespace Barbearia.Application.DTOs
{
    public class ClienteDTO
    {
        public int ID_Cli { get; set; }
        public string Nome { get; set; }
        public string Status { get; set; }
        public string Observacao { get; set; }
        public List<string> Telefones { get; set; }
    }
}
