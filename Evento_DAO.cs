using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace orgEventos1_DATA
{
    public class Evento_DAO
    {
        private readonly string _conexao;

        public Evento_DAO(string conexao)
        {
            _conexao = conexao;
        }

        public void IncluirEvento(Evento evento)
        {

            const string query = @"INSERT INTO evento(tipo, preco, lotacao, hora_comeco, hora_fin, dia_evento )
                                            VALUES (@tipo, @preco, @lotacao, @hora_comeco, @hora_fin, @dia_evento )";


            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))

                {
                    comando.Parameters.Add("@tipo", SqlDbType.NVarChar).Value = evento.tipo ?? (object)DBNull.Value;
                    comando.Parameters.Add("@preco", SqlDbType.Decimal).Value = evento.preco;
                    comando.Parameters.Add("@lotacao", SqlDbType.NChar).Value = evento.lotacao ?? (object)DBNull.Value;
                    comando.Parameters.Add("@hora_comeco", SqlDbType.DateTime).Value = evento.hora_comeco;
                    comando.Parameters.Add("@hora_fin", SqlDbType.DateTime).Value = evento.hora_fin;
                    comando.Parameters.Add("@dia_evento", SqlDbType.DateTime).Value = evento.dia_evento;


                    conexaoBd.Open();


                    comando.ExecuteNonQuery();

                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao incluir Evento: {ex.Message}", ex);
            }

        }
    }
}
