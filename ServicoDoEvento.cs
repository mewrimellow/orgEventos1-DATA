using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orgEventos1_DATA
{
    public class ServicoDoEvento
    {
        public int FkServicoIdServico { get; set; }  // Relación con Servico
        public int FkEventoIdEvento { get; set; }  // Relación con Evento
    }
}
