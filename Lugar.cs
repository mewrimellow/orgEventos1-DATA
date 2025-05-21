using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orgEventos1_DATA
{
    public class Lugar
    {
        public int id_lugar { get; set; }
        public string tipo { get; set; }
        public string nome { get; set; }
        public string cep { get; set; }
        public string capacidade { get; set; }        
        public string logradouro { get; set; }
        public string numLogradouro { get; set; }
        public decimal preco { get; set; }
    }
}
