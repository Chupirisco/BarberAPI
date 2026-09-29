using System;
using System.Collections.Generic;
using System.Text;

namespace Barbearia.Domain.Entities
{
    public class Servico
    {
        public int ID_Ser {  get; set; }

        public double Preco { get; set; }

        public string Nome { get; set; }

        public string Observação { get; set; }

        public DateTime Tempo { get; set; }
    }
}
