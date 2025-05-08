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

            const string query = @"INSERT INTO lugar(tipo, nome, cep, capacidade, logradouro, numLogradouro, preco )
                                 VALUES (@tipo, @nome, @cep, @capacidade, @logradouro, @numLogradouro, @preco )";


            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))

                {
                    comando.Parameters.Add("@tipo", SqlDbType.NVarChar).Value = lugar.tipo ?? (object)DBNull.Value;
                    comando.Parameters.Add("@nome", SqlDbType.NChar).Value = lugar.nome ?? (object)DBNull.Value;
                    comando.Parameters.Add("@cep", SqlDbType.NChar).Value = lugar.cep ?? (object)DBNull.Value;
                    comando.Parameters.Add("@capacidade", SqlDbType.NVarChar).Value = lugar.capacidade ?? (object)DBNull.Value;
                    comando.Parameters.Add("@logradouro", SqlDbType.DateTime).Value = lugar.logradouro;
                    comando.Parameters.Add("@numLogradouro", SqlDbType.NVarChar).Value = lugar.numLogradouro ?? (object)DBNull.Value;
                    comando.Parameters.Add("@preco", SqlDbType.Decimal).Value = lugar.preco;


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
