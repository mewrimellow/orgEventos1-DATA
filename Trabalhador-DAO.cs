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

            const string query = @"INSERT INTO trabalhador(cpf, email, nome, telefone)
                                 VALUES (@cpf, @email, @nome, @telefone )";


            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))

                {
                    comando.Parameters.Add("@cpf", SqlDbType.NVarChar).Value = trabalhador.cpf ?? (object)DBNull.Value;
                    comando.Parameters.Add("@email", SqlDbType.NChar).Value = trabalhador.email ?? (object)DBNull.Value;
                    comando.Parameters.Add("@nome", SqlDbType.NChar).Value = trabalhador.nome ?? (object)DBNull.Value;
                    comando.Parameters.Add("@telefone", SqlDbType.NVarChar).Value = trabalhador.telefone ?? (object)DBNull.Value;



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
