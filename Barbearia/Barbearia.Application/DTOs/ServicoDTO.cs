using System;
using System.Collections.Generic;
using System.Text;

namespace Barbearia.Application.DTOs
{
    public class ServicoDTO
    {
        public int ID_Ser { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string Observacao { get; set; }
        public int Tempo { get; set; }
        public string Status { get; set; }
    }
}
