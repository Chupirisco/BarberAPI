using System;
using System.Collections.Generic;
using System.Text;

namespace Barbearia.Domain.Entities
{
    public class Telefone
    {
        public int ID_Tel { get; set; }

        public string NumeroTel { get; set; }

        public ICollection<Cliente> Clientes { get; set; }

    }
}
