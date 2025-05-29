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

            const string query = @"INSERT INTO Lugar(tipo, nome, cep, capacidade, logradouro, numLogradouro, preco )
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
                    comando.Parameters.Add("@logradouro", SqlDbType.NVarChar).Value = lugar.logradouro ?? (object)DBNull.Value;
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


        public DataSet BuscarLugar(string pesquisa = "")
        {
            const string query = "SELECT * FROM Lugar WHERE nome LIKE @pesquisa";

            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))
                using (var adaptador = new SqlDataAdapter(comando))

                {
                    string parametroPesquisa = $"%{pesquisa}%";

                    comando.Parameters.Add("@pesquisa", SqlDbType.NVarChar).Value = parametroPesquisa;

                    conexaoBd.Open();

                    var dsLugar = new DataSet();

                    adaptador.Fill(dsLugar, "Lugar");

                    return dsLugar;

                }
            }

            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar lugar: {ex.Message}", ex);
            }

        }


        public void ExcluirLugar(int CodigoLugar) // OBS: Renomear para 'CodigoMedico' para manter a consistência
        {
            const string query = "DELETE FROM Lugar WHERE id_lugar = @CodigoLugar";

            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))
                {
                    comando.Parameters.Add("@CodigoLugar", SqlDbType.Int).Value = CodigoLugar;

                    conexaoBd.Open();
                    comando.ExecuteNonQuery(); // Executa a exclusão
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Erro de banco de dados ao excluir o lugar:{ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir lugar: {ex.Message}", ex);
            }
        }

        public Lugar ObtemLugar(int codigoLugar)
        {

            const string query = " SELECT * FROM Lugar WHERE id_lugar = @id_lugar";

            Lugar lugar = null;

            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))
                {

                    comando.Parameters.AddWithValue("@id_Lugar", codigoLugar);
                    conexaoBd.Open();

                    using (var reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lugar = new Lugar()
                            {
                                id_lugar = Convert.ToInt32(reader["id_Lugar"]),
                                tipo = reader.IsDBNull(reader.GetOrdinal("tipo")) ? null : reader["tipo"].ToString(),
                                nome = reader.IsDBNull(reader.GetOrdinal("nome")) ? null : reader["nome"].ToString(),
                                cep = reader.IsDBNull(reader.GetOrdinal("cep")) ? null : reader["cep"].ToString(),
                                capacidade = reader.IsDBNull(reader.GetOrdinal("capacidade")) ? null : reader["capacidade"].ToString(),
                                logradouro = reader.IsDBNull(reader.GetOrdinal("logradouro")) ? null : reader["logradouro"].ToString(),
                                numLogradouro = reader.IsDBNull(reader.GetOrdinal("numLogradouro")) ? null : reader["numLogradouro"].ToString(),
                                //preco = reader.IsDBNull(reader.GetOrdinal("preco")) ? null : reader["preco"].ToString(),
                                

                            };
                        }

                    }

                }
            }

            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter o lugar: {ex.Message}", ex); 
            }

            return lugar;

        }


        public void AlterarLugar(Lugar lugar)
        {

            const string query = @"
                  UPDATE Lugar 
                    SET 


                        tipo = @tipo,
                        nome = @nome,
                        cep = @cep,
                        capacidade = @capacidade,
                        logradouro = @logradouro,
                        numLogradouro = @numLogradouro,
                        preco = @preco

                        WHERE id_Lugar = @id_Lugar

                        ";




            try
            {

                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))
                {

                    comando.Parameters.Add("@tipo", SqlDbType.NVarChar).Value = lugar.tipo ?? (object)DBNull.Value;
                    comando.Parameters.Add("@nome", SqlDbType.NVarChar).Value = lugar.nome ?? (object)DBNull.Value;
                    comando.Parameters.Add("@cep", SqlDbType.NVarChar).Value = lugar.cep ?? (object)DBNull.Value;
                    comando.Parameters.Add("@capacidade", SqlDbType.NVarChar).Value = lugar.capacidade ?? (object)DBNull.Value;
                    comando.Parameters.Add("@logradouro", SqlDbType.NVarChar).Value = lugar.logradouro ?? (object)DBNull.Value;
                    comando.Parameters.Add("@numLogradouro", SqlDbType.NVarChar).Value = lugar.numLogradouro ?? (object)DBNull.Value;
                    comando.Parameters.Add("@preco", SqlDbType.NVarChar).Value = lugar.preco;
                    comando.Parameters.Add("@id_Lugar", SqlDbType.NVarChar).Value = lugar.id_lugar;

                    conexaoBd.Open();
                    comando.ExecuteNonQuery();


                }



            }

            catch (SqlException ex)
            {
                throw new Exception($"Erro de banco de dados ao alterar o Lugar : {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao alterar o Lugar: {ex.Message}", ex);
            }

        }



        public void AlterarServico(Servico servico)
        {

            const string query = @"
                    UPDATE  Servico
                    SET

                        preco = @preco,
                        descricao = @descricao,
                        tipo = @tipo

                        WHERE id_servico = @id_servico

                    ";


            try
            {

                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))
                {

                    comando.Parameters.Add("@preco", SqlDbType.NVarChar).Value = servico.preco;
                    comando.Parameters.Add("@descricao", SqlDbType.NVarChar).Value = servico.descricao ?? (object)DBNull.Value;
                    comando.Parameters.Add("@tipo", SqlDbType.NVarChar).Value = servico.tipo ?? (object)DBNull.Value;
                    comando.Parameters.Add("@id_servico", SqlDbType.NVarChar).Value = servico.id_servico;


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
