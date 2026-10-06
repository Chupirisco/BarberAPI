using System;
using System.Collections.Generic;
using System.Text;

namespace Barbearia.Application.DTOs
{
    public class CreateServicoDTO
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string Observacao { get; set; }
        public int Tempo { get; set; }
    }
}
