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
    public class Trabalhador_DAO
    {
        private readonly string _conexao;

        public Trabalhador_DAO(string conexao)
        {
            _conexao = conexao;
        }

        public void IncluirTrabalhador(Trabalhador trabalhador)
        {

            const string query = @"INSERT INTO trabalhador(Cpf, Email, Nome, Telefone)
                                 VALUES (@Cpf, @Email, @Nome, @Telefone )";


            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))

                {
                    comando.Parameters.Add("@Cpf", SqlDbType.NVarChar).Value = trabalhador.Cpf ?? (object)DBNull.Value;
                    comando.Parameters.Add("@Email", SqlDbType.NChar).Value = trabalhador.Email ?? (object)DBNull.Value;
                    comando.Parameters.Add("@Nome", SqlDbType.NChar).Value = trabalhador.Nome ?? (object)DBNull.Value;
                    comando.Parameters.Add("@Telefone", SqlDbType.NVarChar).Value = trabalhador.Telefone ?? (object)DBNull.Value;



                    conexaoBd.Open();


                    comando.ExecuteNonQuery();

                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao incluir Trabalhador: {ex.Message}", ex);
            }


        }
    }
}
