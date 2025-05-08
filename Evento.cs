using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orgEventos1_DATA
{
    public class Evento
    {
        public int IdEvento { get; set; }
        public string Tipo { get; set; }
        public decimal Preco { get; set; }
        public string Lotacao { get; set; }        
        public DateTime HoraComeco { get; set; }
        public DateTime HoraFin { get; set; }
        public DateTime DiaEvento { get; set; }
        public int FkLugarIdLugar { get; set; }  // Relación con Lugar
        public int FkClienteIdCliente { get; set; }  // Relación con Cliente
    }
}
