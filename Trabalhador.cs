using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orgEventos1_DATA
{
    public class Trabalhador
    {
        public int id_trabalhador { get; set; }
        public string cpf { get; set; }
        public string email { get; set; }
        public string nome { get; set; }
        public string telefone { get; set; }
        public int fk_servico_id_servico { get; set; }


    }
}
