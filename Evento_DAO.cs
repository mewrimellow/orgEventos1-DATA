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
    public class Evento_DAO
    {
        private readonly string _conexao;

        public Evento_DAO(string conexao)
        {
            _conexao = conexao;
        }

        //public void IncluirEvento(Evento evento)
        //{

        //    const string query = @"INSERT INTO evento(tipo, preco, lotacao, hora_comeco, hora_fin, dia_evento )
        //                                    VALUES (@tipo, @preco, @lotacao, @hora_comeco, @hora_fin, @dia_evento )";


        //    try
        //    {
        //        using (var conexaoBd = new SqlConnection(_conexao))
        //        using (var comando = new SqlCommand(query, conexaoBd))

        //        {
        //            comando.Parameters.Add("@tipo", SqlDbType.NVarChar).Value = evento.tipo ?? (object)DBNull.Value;
        //            comando.Parameters.Add("@preco", SqlDbType.Decimal).Value = evento.preco;
        //            comando.Parameters.Add("@lotacao", SqlDbType.NChar).Value = evento.lotacao ?? (object)DBNull.Value;
        //            comando.Parameters.Add("@hora_comeco", SqlDbType.DateTime).Value = evento.hora_comeco;
        //            comando.Parameters.Add("@hora_fin", SqlDbType.DateTime).Value = evento.hora_fin;
        //            comando.Parameters.Add("@dia_evento", SqlDbType.DateTime).Value = evento.dia_evento;


        //            conexaoBd.Open();


        //            comando.ExecuteNonQuery();

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"Erro ao incluir Evento: {ex.Message}", ex);
        //    }

        //}

        public int InserirEvento(Evento evento)
        {
            using (SqlConnection conn = new SqlConnection(_conexao))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
                INSERT INTO Evento (id_cliente, id_lugar, dia_evento, hora_inicio, hora_fim, preco)
                VALUES (@cliente, @lugar, @data, @inicio, @fim, @preco);
                SELECT SCOPE_IDENTITY();", conn);

                cmd.Parameters.AddWithValue("@cliente", evento.id_cliente);
                cmd.Parameters.AddWithValue("@lugar", evento.id_lugar);
                cmd.Parameters.AddWithValue("@data", evento.dia_evento.Date); // Aquí sigue usando dia_evento en C#, pero insertando en data_evento
                cmd.Parameters.AddWithValue("@inicio", evento.hora_inicio);
                cmd.Parameters.AddWithValue("@fim", evento.hora_fim);
                cmd.Parameters.AddWithValue("@preco", evento.preco);

                Console.WriteLine("DATA A INSERIR: " + evento.dia_evento.ToString("yyyy-MM-dd"));
                return Convert.ToInt32(cmd.ExecuteScalar()); // retorna o ID do evento inserido
            }
        }

        public void InserirServicosDoEvento(List<ServicoDoEvento> lista, int idEvento)
        {
            using (SqlConnection conn = new SqlConnection(_conexao))
            {
                conn.Open();
                foreach (var item in lista)
                {
                    SqlCommand cmd = new SqlCommand(@"
                    INSERT INTO Evento_Servico (fk_evento_id_evento, fk_servico_id_servico)
                    VALUES (@id_evento, @id_servico)", conn);

                        cmd.Parameters.AddWithValue("@id_evento", idEvento);
                        cmd.Parameters.AddWithValue("@id_servico", item.id_servico);
                        cmd.ExecuteNonQuery();
                }
            }
        }

        public DataSet BuscarEvento(string pesquisa = "")
        {
            const string query = @"
            SELECT e.id_evento, e.dia_evento, e.hora_inicio, e.hora_fim, e.preco,
                   c.nome AS nome_cliente, l.nome AS nome_lugar
            FROM Evento e
            JOIN Cliente c ON e.id_cliente = c.id_cliente
            JOIN Lugar l ON e.id_lugar = l.id_lugar
            WHERE c.nome LIKE @pesquisa";

            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(query, conexaoBd))
                using (var adaptador = new SqlDataAdapter(comando))
                {
                    string parametroPesquisa = $"%{pesquisa}%";
                    comando.Parameters.Add("@pesquisa", SqlDbType.NVarChar).Value = parametroPesquisa;

                    conexaoBd.Open();

                    var dsEvento = new DataSet();
                    adaptador.Fill(dsEvento, "Evento");

                    return dsEvento;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar eventos: {ex.Message}", ex);
            }
        }



        public void ExcluirEvento(int codigoEvento)
        {
            using (var conexao = new SqlConnection(_conexao))
            {
                conexao.Open();

                using (var transaction = conexao.BeginTransaction())
                {
                    try
                    {
                        // Apagar registros filhos primeiro
                        string sqlDeleteFilhos = "DELETE FROM Evento_Servico WHERE fk_evento_id_evento = @IdEvento";
                        using (var cmdFilho = new SqlCommand(sqlDeleteFilhos, conexao, transaction))
                        {
                            cmdFilho.Parameters.AddWithValue("@IdEvento", codigoEvento);
                            cmdFilho.ExecuteNonQuery();
                        }

                        // Depois apagar o evento
                        string sqlDeleteEvento = "DELETE FROM Evento WHERE id_evento = @IdEvento";
                        using (var cmdEvento = new SqlCommand(sqlDeleteEvento, conexao, transaction))
                        {
                            cmdEvento.Parameters.AddWithValue("@IdEvento", codigoEvento);
                            cmdEvento.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Erro ao excluir evento: " + ex.Message, ex);
                    }
                }
            }
        }



        public List<EventoDetalhado> ListarEventosDetalhados()
        {
            const string sql = @"
            SELECT 
                e.id_evento,
                c.nome AS nome_cliente,
                l.nome AS nome_lugar,
                e.dia_evento,
                e.hora_inicio,
                e.hora_fim
            FROM evento e
            JOIN cliente c ON c.id_cliente = e.id_cliente
            JOIN lugar l ON l.id_lugar = e.id_lugar";

            var lista = new List<EventoDetalhado>();

            try
            {
                using (var conexaoBd = new SqlConnection(_conexao))
                using (var comando = new SqlCommand(sql, conexaoBd))
                {
                    conexaoBd.Open();

                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var evento = new EventoDetalhado
                            {
                                IdEvento = Convert.ToInt32(reader["id_evento"]),
                                nomeCliente = reader["nome_cliente"].ToString(),
                                nomeLugar = reader["nome_lugar"].ToString(),
                                DataEvento = Convert.ToDateTime(reader["dia_evento"]),
                                HoraInicio = reader["hora_inicio"] != DBNull.Value ? (TimeSpan)reader["hora_inicio"] : TimeSpan.Zero,
                                HoraFim = reader["hora_fim"] != DBNull.Value ? (TimeSpan)reader["hora_fim"] : TimeSpan.Zero
                            };

                            lista.Add(evento);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar eventos detalhados: {ex.Message}", ex);
            }

            return lista;
        }




    }
}
