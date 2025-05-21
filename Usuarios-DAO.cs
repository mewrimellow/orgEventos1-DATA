using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orgEventos1_DATA
{
    public class Usuarios_DAO
    {
        private readonly string _conexao;

        public Usuarios_DAO(string conexao)
        {
            _conexao = conexao;
        }

        public void IncluirUsuarios(Usuarios usuarios)
        {
            const string query = @"INSERT INTO Usuarios (nome, NomeUsuario, Senha)
                          VALUES(@nome, @NomeUsuario, @Senha)";

            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))

                {
                    comando.Parameters.Add("@nome", SqlDbType.NVarChar).Value = usuarios.nome ?? (object)DBNull.Value;
                    comando.Parameters.Add("@NomeUsuario", SqlDbType.NVarChar).Value = usuarios.NomeUsuario ?? (object)DBNull.Value;
                    comando.Parameters.Add("@Senha", SqlDbType.NChar).Value = usuarios.Senha ?? (object)DBNull.Value;


                    conexaoBd.Open();

                    comando.ExecuteNonQuery();



                }

            }

            catch (Exception ex)
            {
                throw new Exception($"Erro ao incluir Usuário: {ex.Message}", ex);
            }
        }
        public DataSet BuscaUsuario(string nomeusuario = "", string senha = "", string nome = "")
        {

            const string query = "SELECT * FROM Usuarios WHERE nome LIKE @nome AND nomeUsuario LIKE @nomeusuario AND Senha LIKE @senha";


            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))

                using (var comando = new SqlCommand(query, conexaoBd))

                using (var adaptador = new SqlDataAdapter(comando))

                {
                    string parametronome = $"{nome}";
                    string parametroNome = $"{nomeusuario}";
                    string parametroSenha = $"{senha}";

                    comando.Parameters.Add("@nome", SqlDbType.NVarChar).Value = parametronome;
                    comando.Parameters.Add("@NomeUsuario", SqlDbType.NVarChar).Value = parametroNome;
                    comando.Parameters.Add("@senha", SqlDbType.NVarChar).Value = parametroSenha;

                    conexaoBd.Open();

                    var dsUsuarios = new DataSet();

                    adaptador.Fill(dsUsuarios, "Usuarios");

                    return dsUsuarios;
                }

            }
            catch (Exception ex)
            {

                throw new Exception($"Nome de usuários ou senha incorretos");

            }



        }

        public DataSet LoginUsuario(string nomeusuario = "", string senha = "")
        {

            const string query = "SELECT * FROM Usuarios WHERE nomeUsuario LIKE @nomeusuario AND Senha LIKE @senha";


            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))

                using (var comando = new SqlCommand(query, conexaoBd))

                using (var adaptador = new SqlDataAdapter(comando))

                {
                    
                    string parametroNome = $"{nomeusuario}";
                    string parametroSenha = $"{senha}";

                    
                    comando.Parameters.Add("@NomeUsuario", SqlDbType.NVarChar).Value = parametroNome;
                    comando.Parameters.Add("@senha", SqlDbType.NVarChar).Value = parametroSenha;

                    conexaoBd.Open();

                    var dsUsuarios = new DataSet();

                    adaptador.Fill(dsUsuarios, "Usuarios");

                    return dsUsuarios;
                }

            }
            catch (Exception ex)
            {

                throw new Exception($"Nome de usuários ou senha incorretos");

            }



        }

        public DataSet dtvUsuario(string nomeusuario = "")
        {

            const string query = "SELECT * FROM Usuarios WHERE NomeUsuario LIKE @nomeusuario";


            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))

                using (var comando = new SqlCommand(query, conexaoBd))

                using (var adaptador = new SqlDataAdapter(comando))

                {
                    string parametroNome = $"%{nomeusuario}%";


                    comando.Parameters.Add("@NomeUsuario", SqlDbType.NVarChar).Value = parametroNome;


                    conexaoBd.Open();

                    var dsUsuarios = new DataSet();

                    adaptador.Fill(dsUsuarios, "Usuarios");

                    return dsUsuarios;
                }

            }
            catch (Exception ex)
            {

                throw new Exception($"Nome de usuários ou senha incorretos");

            }



        }

        public void ExcluiUsuario(int codigo)
        {
            const string query = "DELETE FROM Usuarios WHERE cod_usuario = @cod_usuario";

            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))

                using (var comando = new SqlCommand(query, conexaoBd))
                {
                    comando.Parameters.Add("@cod_usuario", SqlDbType.Int).Value = codigo;

                    conexaoBd.Open();

                    comando.ExecuteNonQuery();
                }
            }

            catch (SqlException ex)
            {

                throw new Exception($"Erro de banco de dados ao excluir o usuário: {ex.Message}", ex);

            }
            catch (Exception ex)
            {
                throw new Exception($"Erro  ao excluir o usuário : {ex.Message}", ex);
            }

        }

        public void AlterarUsuarios(Usuarios usuarios)
        {
            const string query = @"UPDATE Usuarios SET
                                   NomeUsuario = @NomeUsuario,
                                   Senha = @Senha
                                  
                                  
                            WHERE cod_usuario = @cod_usuario";

            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))

                {
                    comando.Parameters.Add("@NomeUsuario", SqlDbType.NVarChar).Value = usuarios.NomeUsuario ?? (object)DBNull.Value;
                    comando.Parameters.Add("@Senha", SqlDbType.NChar).Value = usuarios.Senha ?? (object)DBNull.Value;

                    comando.Parameters.Add("@cod_usuario", SqlDbType.Int).Value = usuarios.cod_usuario;
                    conexaoBd.Open();

                    comando.ExecuteNonQuery();

                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Erro: {ex.Message}", ex);
            }

            catch (Exception ex)
            {
                throw new Exception($"Erro ao alterar o Usuário: {ex.Message}", ex);
            }
        }
    }
}