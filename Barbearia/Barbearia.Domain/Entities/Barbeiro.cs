using Barbearia.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Barbearia.Domain.Entities
{
    public class Barbeiro
    {
        public int ID_Bar {  get; set; }

        public string Nome { get; set; }

        public StatusAtivo Status { get; set; }

        public string NumeroTel { get; set; }
    }
}
