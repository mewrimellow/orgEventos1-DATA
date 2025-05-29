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
    public class Servico_DAO
    {

        private readonly string _conexao;

        public Servico_DAO(string conexao)
        {
            _conexao = conexao;
        }

        public void InserirServico(Servico servico)
        {

            const string query = @"INSERT INTO servico (preco, descricao, tipo)
                                 VALUES (@preco, @descricao, @tipo )";


            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))

                {
                    comando.Parameters.Add("@preco", SqlDbType.Decimal).Value = servico.preco;
                    comando.Parameters.Add("@descricao", SqlDbType.NVarChar).Value = servico.descricao ?? (object)DBNull.Value;
                    comando.Parameters.Add("@tipo", SqlDbType.NChar).Value = servico.tipo ?? (object)DBNull.Value;



                    conexaoBd.Open();


                    comando.ExecuteNonQuery();

                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao incluir Serviço: {ex.Message}", ex);
            }

        }


        public DataSet BuscarServico(string pesquisa = "")
        {
            const string query = "SELECT * FROM Servico WHERE tipo LIKE @pesquisa";

            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))
                using (var adaptador = new SqlDataAdapter(comando))

                {
                    string parametroPesquisa = $"%{pesquisa}%";

                    comando.Parameters.Add("@pesquisa", SqlDbType.NVarChar).Value = parametroPesquisa;

                    conexaoBd.Open();

                    var dsServico = new DataSet();

                    adaptador.Fill(dsServico, "Servico");

                    return dsServico;

                }
            }

            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar serviço: {ex.Message}", ex);
            }

        }


        public void ExcluirServico(int CodigoServico) // OBS: Renomear para 'CodigoMedico' para manter a consistência
        {
            const string query = "DELETE FROM Servico WHERE id_servico = @CodigoServico";

            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))
                {
                    comando.Parameters.Add("@CodigoServico", SqlDbType.Int).Value = CodigoServico;

                    conexaoBd.Open();
                    comando.ExecuteNonQuery(); // Executa a exclusão
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Erro de banco de dados ao excluir o servico:{ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir servico : {ex.Message}", ex);
            }
        }


        public List<Servico> ListarServicos()
        {
            List<Servico> lista = new List<Servico>();

            using (SqlConnection conn = new SqlConnection(_conexao))
            {
                string sql = "SELECT id_servico, tipo, descricao, preco FROM Servico";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Servico servico = new Servico
                            {
                                id_servico = reader.GetInt32(0),
                                tipo = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                                descricao = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                preco = reader.IsDBNull(3) ? 0m : reader.GetDecimal(3)
                            };
                            lista.Add(servico);
                        }
                    }
                }
            }

            return lista;
        }


        public Servico ObtemServico(int CodigoServico)
        {

            const string query = "SELECT * FROM Servico WHERE id_servico = @id_servico";

            Servico servico = null;

            try
            {

                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))
                {

                    comando.Parameters.AddWithValue("@id_servico", CodigoServico);

                    conexaoBd.Open();

                    using (var reader = comando.ExecuteReader())
                    {

                        if (reader.Read())
                        {

                            servico = new Servico()
                            {
                                id_servico = Convert.ToInt32(reader["id_servico"]),
                                //preco = reader.IsDBNull(reader.GetOrdinal("preco")) ? null : reader["preco"].ToString(),
                                descricao = reader.IsDBNull(reader.GetOrdinal("descricao")) ? null : reader["descricao"].ToString(),
                                tipo = reader.IsDBNull(reader.GetOrdinal("tipo")) ? null : reader["tipo"].ToString(),



                            };
                        }


                    }


                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter o cliente: {ex.Message}", ex); // OBS: Aqui o termo correto seria "médico", não "cliente"
            }

            return servico;

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
