using System;
using System.Collections.Generic;
using System.Text;

namespace Barbearia.Domain.Entities
{
    public class Cliente
    {
        public int ID_Cli {  get; set; }
        
        public string Name { get; set; }
        
        public string Status { get; set; }

        public string Observacao { get; set; }

    }
}
