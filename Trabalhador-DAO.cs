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

            const string query = @"INSERT INTO Trabalhador(cpf, email, nome, telefone)
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




        public DataSet BuscarTrabalhador(string pesquisa = "")
        {
            const string query = "SELECT * FROM Trabalhador WHERE nome LIKE @pesquisa";

            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))
                using (var adaptador = new SqlDataAdapter(comando))

                {
                    string parametroPesquisa = $"%{pesquisa}%";

                    comando.Parameters.Add("@pesquisa", SqlDbType.NVarChar).Value = parametroPesquisa;

                    conexaoBd.Open();

                    var dsTrabalhador = new DataSet();

                    adaptador.Fill(dsTrabalhador, "Trabalhador");

                    return dsTrabalhador;

                }
            }

            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar cliente: {ex.Message}", ex);
            }

        }


        public void ExcluirTrabalhador(int CodigoTrabalhador) // OBS: Renomear para 'CodigoMedico' para manter a consistência
        {
            const string query = "DELETE FROM Trabalhador WHERE id_trabalhador = @CodigoTrabalhador";

            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))
                {
                    comando.Parameters.Add("@CodigoTrabalhador", SqlDbType.Int).Value = CodigoTrabalhador;

                    conexaoBd.Open();
                    comando.ExecuteNonQuery(); // Executa a exclusão
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Erro de banco de dados ao excluir o trabalhador:{ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir trabalhador : {ex.Message}", ex);
            }
        }


        public Trabalhador ObtemTrabalhador(int codigoTrabalhador)
        {

            const string query = "SELECT * FROM Trabalhador WHERE id_trabalhador = @codigoTrabalhador";

            Trabalhador trabalhador = null;
            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))
                {

                    comando.Parameters.AddWithValue("@codigoTrabalhador", codigoTrabalhador);

                    conexaoBd.Open();

                    using (var reader = comando.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            trabalhador = new Trabalhador()
                            {
                                id_trabalhador = Convert.ToInt32(reader["id_trabalhador"]),
                                cpf = reader.IsDBNull(reader.GetOrdinal("cpf")) ? null : reader["cpf"].ToString(),
                                email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader["email"].ToString(),
                                nome = reader.IsDBNull(reader.GetOrdinal("nome")) ? null : reader["nome"].ToString(),
                                telefone = reader.IsDBNull(reader.GetOrdinal("telefone")) ? null : reader["telefone"].ToString(),


                            };
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter o trabalhador: {ex.Message}", ex); 
            }

            return trabalhador;

        }


        public void AlterarTrabalhador(Trabalhador trabalhador)
        {

            const string query = @"

                 UPDATE Trabalhador 
                 SET
    
                   cpf = @cpf,
                   email = @email,
                   nome = @nome,
                   telefone = @telefone

    
                   WHERE id_trabalhador = @id_trabalhador ";


            try
            {

                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))
                {

                    comando.Parameters.Add("@cpf", SqlDbType.NVarChar).Value = trabalhador.cpf ?? (object)DBNull.Value;
                    comando.Parameters.Add("@email", SqlDbType.NVarChar).Value = trabalhador.email ?? (object)DBNull.Value;
                    comando.Parameters.Add("@nome", SqlDbType.NVarChar).Value = trabalhador.nome ?? (object)DBNull.Value;
                    comando.Parameters.Add("@telefone", SqlDbType.NVarChar).Value = trabalhador.telefone ?? (object)DBNull.Value;

                    comando.Parameters.Add("@id_trabalhador", SqlDbType.NVarChar).Value = trabalhador.id_trabalhador;

                    conexaoBd.Open();
                    comando.ExecuteNonQuery();

                }

            }
            catch (SqlException ex)
            {
                throw new Exception($"Erro de banco de dados ao alterar o paciente : {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao alterar o Paciente: {ex.Message}", ex);
            }
        }

    }
}
