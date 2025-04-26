using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orgEventos1_DATA
{
    public class Cliente_DAO
    {
        private readonly string _conexao;

        public Cliente_DAO(string conexao)
        {
            _conexao = conexao;
        }

        public void IncluirCliente(Cliente cliente)
        {
            const string query = @"INSERT INTO Cliente";
        }

    }
}
