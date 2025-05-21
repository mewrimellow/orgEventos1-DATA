using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orgEventos1_DATA
{
    public class Pagamento
    {
        public int id_pagamento { get; set; }
        public decimal valor { get; set; }
        public DateTime dia_pagamento { get; set; }        
        public string status_pagamento { get; set; }
        public int fk_evento_id_vento { get; set; }

    }
}
