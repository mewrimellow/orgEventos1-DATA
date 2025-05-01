using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orgEventos1_DATA
{
    internal class Pagamento
    {
        public int IdPagamento { get; set; }
        public decimal Valor { get; set; }
        public DateTime DiaPagamento { get; set; }        
        public string StatusPagamento { get; set; }
        public int FkEventoIdEvento { get; set; }

    }
}
