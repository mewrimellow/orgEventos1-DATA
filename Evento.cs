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
        public string tipo { get; set; }
        public decimal preco { get; set; }
        public string lotacao { get; set; }        
        public DateTime hora_comeco { get; set; }
        public DateTime hora_fin { get; set; }
        public DateTime dia_evento { get; set; }
        public int fk_lugar_id_lugar { get; set; }  // Relación con Lugar
        public int fk_cliente_id_cliente { get; set; }  // Relación con Cliente
    }
}
