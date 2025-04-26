using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orgEventos1_DATA
{
    public class Cliente
    {
        public int id_cliente { get; set; }
        public string nome { get; set; }

        public string cpf { get; set; }

        public string telefone { get; set; }

        public string email { get; set; }

        public string logradouro { get; set; }

        public DateTime dataNasc { get; set; }

        public string numLogradouro { get; set; }

        public string complemento { get; set; }
    }
}
