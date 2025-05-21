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
            const string query = "DELETE FROM Cliente WHERE id_cliente = @id_cliente";

            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))
                {
                    comando.Parameters.Add("@id_cliente", SqlDbType.Int).Value = CodigoCliente;

                    conexaoBd.Open();
                    comando.ExecuteNonQuery(); // Executa a exclusão
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Erro de banco de dados ao excluir o Medico:{ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir Medico : {ex.Message}", ex);
            }
        }





    }
}
