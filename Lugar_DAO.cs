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
    public class Lugar_DAO
    {
        private readonly string _conexao;

        public Lugar_DAO(string conexao)
        {
            _conexao = conexao;
        }


        public void IncluirLugar ( Lugar lugar)
        {

            const string query = @"INSERT INTO lugar(Tipo, Nome, Cep, Capacidade, Logradouro, NumLogradouro, Preco, )
                                 VALUES (@Tipo, @Nome, @Cep, @Capacidade, @Logradouro, @NumLogradouro, @Preco, )";


            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))

                {
                    comando.Parameters.Add("@Tipo", SqlDbType.NVarChar).Value = lugar.Tipo ?? (object)DBNull.Value;
                    comando.Parameters.Add("@Nome", SqlDbType.NChar).Value = lugar.Nome ?? (object)DBNull.Value;
                    comando.Parameters.Add("@Cep", SqlDbType.NChar).Value = lugar.Cep ?? (object)DBNull.Value;
                    comando.Parameters.Add("@Capacidade", SqlDbType.NVarChar).Value = lugar.Capacidade ?? (object)DBNull.Value;
                    comando.Parameters.Add("@Logradouro", SqlDbType.DateTime).Value = lugar.Logradouro;
                    comando.Parameters.Add("@NumLogradouro", SqlDbType.NVarChar).Value = lugar.NumLogradouro ?? (object)DBNull.Value;
                    comando.Parameters.Add("@Preco", SqlDbType.Decimal).Value = lugar.Preco;


                    conexaoBd.Open();


                    comando.ExecuteNonQuery();

                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao incluir Lugar: {ex.Message}", ex);
            }
        }       
    }
}
