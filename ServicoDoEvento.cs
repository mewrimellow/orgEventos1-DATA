﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orgEventos1_DATA
{
    public class ServicoDoEvento
    {
        public int id_servico { get; set; }  // Relación con Servico
        public int id_evento { get; set; }  // Relación con Evento
    }
}
