using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orgEventos1_DATA
{
    internal class Lugar
    {
        public int IdLugar { get; set; }
        public string Tipo { get; set; }
        public string Nome { get; set; }
        public string Cep { get; set; }
        public string Capacidade { get; set; }        
        public string Logradouro { get; set; }
        public string NumLogradouro { get; set; }
        public decimal Preco { get; set; }
    }
}
