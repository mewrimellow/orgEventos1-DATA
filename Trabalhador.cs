using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orgEventos1_DATA
{
    public class Trabalhador
    {
        public int IdTrabalhador { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public int FkServicoIdServico { get; set; }


    }
}
