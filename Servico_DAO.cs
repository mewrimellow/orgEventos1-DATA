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

            const string query = @"INSERT INTO servico (Preco, Descricao, Tipo, )
                                 VALUES (@Preco, @Descricao, @Tipo )";


            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))

                {
                    comando.Parameters.Add("@Tipo", SqlDbType.Decimal).Value = servico.Preco;
                    comando.Parameters.Add("@Nome", SqlDbType.NChar).Value = servico.Descricao ?? (object)DBNull.Value;
                    comando.Parameters.Add("@Cep", SqlDbType.NChar).Value = servico.Tipo ?? (object)DBNull.Value;



                    conexaoBd.Open();


                    comando.ExecuteNonQuery();

                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao incluir Serviço: {ex.Message}", ex);
            }

        }
    }
}
