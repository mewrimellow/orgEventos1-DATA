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
    public class Cliente_DAO
    {
        private readonly string _conexao;

        public Cliente_DAO(string conexao)
        {
            _conexao = conexao;
        }

        public void IncluirCliente(Cliente cliente)
        {
            const string query = @"INSERT INTO Cliente(nome, cpf, telefone, email, dataNasc, logradouro, numLogradouro, complemento)
                                 VALUES (@nome, @cpf, @telefone, @email, @dataNasc, @logradouro, @numLogradouro, @complemento)";

            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))

                {
                    comando.Parameters.Add("@nome", SqlDbType.NVarChar).Value = cliente.nome ?? (object)DBNull.Value;
                    comando.Parameters.Add("@cpf", SqlDbType.NChar).Value = cliente.cpf ?? (object)DBNull.Value;
                    comando.Parameters.Add("@telefone", SqlDbType.NChar).Value = cliente.telefone ?? (object)DBNull.Value;
                    comando.Parameters.Add("@email", SqlDbType.NVarChar).Value = cliente.email ?? (object)DBNull.Value;
                    comando.Parameters.Add("@dataNasc", SqlDbType.DateTime).Value = cliente.dataNasc;
                    comando.Parameters.Add("@logradouro", SqlDbType.NVarChar).Value = cliente.logradouro ?? (object)DBNull.Value;
                    comando.Parameters.Add("@numLogradouro", SqlDbType.NVarChar).Value = cliente.numLogradouro ?? (object)DBNull.Value;
                    comando.Parameters.Add("@complemento", SqlDbType.NChar).Value = cliente.complemento ?? (object)DBNull.Value;


                    conexaoBd.Open();


                    comando.ExecuteNonQuery();


                }


            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao incluir paciente: {ex.Message}", ex);
            }

        }

        public DataSet BuscarCliente(string pesquisa = "")
        {
            const string query = "SELECT * FROM Cliente WHERE nome LIKE @pesquisa";

            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))
                using (var adaptador = new SqlDataAdapter(comando))

                {
                    string parametroPesquisa = $"%{pesquisa}%";

                    comando.Parameters.Add("@pesquisa", SqlDbType.NVarChar).Value = parametroPesquisa;

                    conexaoBd.Open();

                    var dsCliente = new DataSet();

                    adaptador.Fill(dsCliente, "Cliente");

                    return dsCliente;

                }
            }

            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar cliente: {ex.Message}", ex);
            }

        }

        public void ExcluirCliente(int CodigoCliente) // OBS: Renomear para 'CodigoMedico' para manter a consistência
        {
            const string query = "DELETE FROM Cliente WHERE id_cliente = @CodigoCliente";

            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))
                {
                    comando.Parameters.Add("@CodigoCliente", SqlDbType.Int).Value = CodigoCliente;

                    conexaoBd.Open();
                    comando.ExecuteNonQuery(); // Executa a exclusão
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Erro de banco de dados ao excluir o cliente:{ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir cliente : {ex.Message}", ex);
            }
        }


        public Cliente ObtemCliente(int CodigoCliente)
        {
            const string query = "SELECT * FROM Cliente WHERE id_cliente = @codigoCliente";

            Cliente cliente = null;
            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))
                {
                    comando.Parameters.AddWithValue("@codigoCliente", CodigoCliente); // Define o parâmetro

                    conexaoBd.Open();

                    using (var reader = comando.ExecuteReader())
                    {
                        if (reader.Read()) // Se encontrou o médico
                        {
                            // Cria o objeto com os dados lidos do banco
                            cliente = new Cliente
                            {
                                id_cliente = Convert.ToInt32(reader["id_cliente"]),
                                nome = reader["nome"].ToString(),
                                cpf = reader.IsDBNull(reader.GetOrdinal("cpf")) ? null : reader["cpf"].ToString(),
                                dataNasc = (DateTime)reader["dataNasc"],
                                email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader["email"].ToString(),
                                telefone = reader.IsDBNull(reader.GetOrdinal("telefone")) ? null : reader["telefone"].ToString(),
                                logradouro = reader.IsDBNull(reader.GetOrdinal("logradouro")) ? null : reader["logradouro"].ToString(),
                                numLogradouro = reader.IsDBNull(reader.GetOrdinal("numLogradouro")) ? null : reader["numLogradouro"].ToString(),
                                complemento = reader.IsDBNull(reader.GetOrdinal("complemento")) ? null : reader["complemento"].ToString(),
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter o cliente: {ex.Message}", ex); // OBS: Aqui o termo correto seria "médico", não "cliente"
            }

            return cliente; // Retorna o objeto preenchido ou null se não encontrou
        }


        public void AlterarCliente(Cliente cliente)
        {

            const string query = @"
                    UPDATE  Cliente
                    SET

                        nome = @nome,
                        cpf = @cpf,
                        telefone = @telefone,
                        email = @email,
                        dataNasc = @dataNasc,
                        logradouro = @logradouro,
                        numLogradouro = @numLogradouro,
                        complemento = @complemento

                         WHERE id_cliente = @id_cliente
                                
                        ";


            try
            {


                // Cria e abre a conexão com o banco
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))
                {
                    comando.Parameters.Add("@nome", SqlDbType.NVarChar).Value = cliente.nome ?? (object)DBNull.Value;
                    comando.Parameters.Add("@cpf", SqlDbType.NVarChar).Value = cliente.cpf ?? (object)DBNull.Value;
                    comando.Parameters.Add("@telefone", SqlDbType.NVarChar).Value = cliente.telefone ?? (object)DBNull.Value;
                    comando.Parameters.Add("@email", SqlDbType.NVarChar).Value = cliente.email ?? (object)DBNull.Value;
                    comando.Parameters.Add("@dataNasc", SqlDbType.DateTime).Value = cliente.dataNasc;
                    comando.Parameters.Add("@logradouro", SqlDbType.NVarChar).Value = cliente.logradouro ?? (object)DBNull.Value;
                    comando.Parameters.Add("@numLogradouro", SqlDbType.NVarChar).Value = cliente.numLogradouro ?? (object)DBNull.Value;
                    comando.Parameters.Add("@complemento", SqlDbType.NVarChar).Value = cliente.complemento ?? (object)DBNull.Value;
                    comando.Parameters.Add("@id_cliente", SqlDbType.NVarChar).Value = cliente.id_cliente;


                    conexaoBd.Open();
                    comando.ExecuteNonQuery(); // Executa a atualização



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

