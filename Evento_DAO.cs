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

            const string query = @"INSERT INTO evento(Tipo, Preco, Lotacao, HoraComeco, HoraFin, DiaEvento, )
                                            VALUES (@Tipo, @Preco, @Lotacao, @HoraComeco, @HoraFin, @DiaEvento, )";


            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))

                {
                    comando.Parameters.Add("@Tipo", SqlDbType.NVarChar).Value = evento.Tipo ?? (object)DBNull.Value;
                    comando.Parameters.Add("@Preco", SqlDbType.Decimal).Value = evento.Preco;
                    comando.Parameters.Add("@Lotacao", SqlDbType.NChar).Value = evento.Lotacao ?? (object)DBNull.Value;
                    comando.Parameters.Add("@HoraComeco", SqlDbType.DateTime).Value = evento.HoraComeco;
                    comando.Parameters.Add("@HoraFin", SqlDbType.DateTime).Value = evento.HoraFin;
                    comando.Parameters.Add("@DiaEvento", SqlDbType.DateTime).Value = evento.DiaEvento;


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
