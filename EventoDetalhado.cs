using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orgEventos1_DATA
{
    public class EventoDetalhado
    {
        public int IdEvento {  get; set; }
        public string nomeCliente { get; set; }

        public string nomeLugar { get; set; }

        public DateTime DataEvento { get; set; }

        public TimeSpan HoraInicio { get; set; }

        public TimeSpan HoraFim { get; set; }


    }
}
