using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orgEventos1_DATA
{
    public class Evento
    {
        public int id_evento { get; set; }
        public int id_cliente { get; set; }
        public int id_lugar { get; set; }
        public DateTime DataEvento { get; set; }
        public TimeSpan hora_inicio { get; set; }
        public TimeSpan hora_fim { get; set; }
    }
}
