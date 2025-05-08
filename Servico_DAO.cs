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

            const string query = @"INSERT INTO servico (preco, descricao, tipo, )
                                 VALUES (@preco, @descricao, @tipo )";


            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))

                {
                    comando.Parameters.Add("@preco", SqlDbType.Decimal).Value = servico.preco;
                    comando.Parameters.Add("@descricao", SqlDbType.NChar).Value = servico.descricao ?? (object)DBNull.Value;
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
    }
}
