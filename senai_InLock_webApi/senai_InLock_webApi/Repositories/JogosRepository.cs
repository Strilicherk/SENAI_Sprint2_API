using senai_InLock_webApi.Domains;
using senai_InLock_webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai_InLock_webApi.Repositories
{
    /// <summary>
    /// Classe responsável pelo repositorio dos jogos
    /// </summary>
    public class JogosRepository : IJogosRepository
    {
        /// <summary>
        /// String de conexão com o banco de dados
        /// Data Source = nome do servidor
        /// initial catalog = nome do banco de dados
        /// user Id= id do usuário; pwd = senha; = faz a autenticação com o usuário
        /// </summary>
        public string stringConexao = "Data Source = DESKTOP-Q0JOIHU\\SQLEXPRESS; initial catalog = InLock_Games; user Id = sa; pwd=strilicherk2012n";
        
        /// <summary>
        /// Cadastra um novo jogo
        /// </summary>
        /// <param name="novoJogo">Objeto novoJogo com as informações que serão cadastradas</param>
        public void Cadastrar(JogosDomain novoJogo)
        {
                // Declara a SqlConnection con passando a string de conexão como parâmetro
                using (SqlConnection con = new SqlConnection(stringConexao))
                {
                    // Declara a instrução a ser executada
                    // string queryInsert = "INSERT INTO Generos(Nome) VALUES ('" + novoGenero.nome + "')";
                    // Não usar dessa forma pois permite SQL Injection e causa o efeito Joana D'Arc
                    // Por exemplo:
                    // "nome" : "Perdeu mané') DROP TABLE Filmes--"

                    //Declara a instrução a ser executada
                    string queryInsert = "INSERT INTO Jogos(NomeJogo, Descricao, DataLancamento, Valor, IdEstudio) VALUES (@NomeJogo, @Descricao, @DataLancamento, @Valor, @IdEstudio)";

                    // Declara o SqlCommand cmd passando a query que será executada e a conexão como parâmetros
                    using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                    {
                        // Passa o valor dos parâmetros @
                        cmd.Parameters.AddWithValue("@NomeJogo", novoJogo.NomeJogo);
                        cmd.Parameters.AddWithValue("@Descricao", novoJogo.Descricao);
                        cmd.Parameters.AddWithValue("@DataLancamento", novoJogo.DataLancamento);
                        cmd.Parameters.AddWithValue("@Valor", novoJogo.Valor);
                        cmd.Parameters.AddWithValue("@IdEstudio", novoJogo.IdEstudio);

                        // Abre a conexão com o banco de dados
                        con.Open();

                        //Executa a query
                        cmd.ExecuteNonQuery();
                    }
                }
        }

        /// <summary>
        /// Lista todos os jogos
        /// </summary>
        /// <returns>Uma lista de jogos</returns>
        public List<JogosDomain> ListarTodos()
        {
            //Cria uma lista listaJogos onde serão armazenados os dados
            List<JogosDomain> listaJogos = new List<JogosDomain>();

            // Declara a SqlConnection con passando a string de conexão como parâmetro
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                //Declara a instrução a ser executada
                string querySelectAll = "SELECT J.IdJogo, J.NomeJogo, J.Descricao, J.DataLancamento, J.Valor, J.IdEstudio, E.NomeEstudio FROM Jogos J INNER JOIN Estudios E ON J.IdEstudio = E.IdEstudio;";
                
                // Abre a conexão com o banco de dados
                con.Open();
                
                // Declara o SqlDataReader rdr para percorrer a tabela do banco de dados
                SqlDataReader rdr;
                
                // Declara o SqlCommand cmd passando a query que será executada e a conexão como parâmetros
                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    // Executa a query e armazena os dados no rdr
                    rdr = cmd.ExecuteReader();

                    //Enquanto houver registros para serem lidos no rdr, o laço se repete
                    while (rdr.Read())
                    {
                        //Instancia um objeto genero do tipo GeneroDomain
                        JogosDomain jogos = new JogosDomain()
                        {
                            //Atribui à propriedade IdJogo o valor da primeira coluna da tabela do banco de dados
                            IdJogo = Convert.ToInt32(rdr[0]),

                            //Atribui à propriedade NomeJogo o valor da segunda coluna da tabela do banco de dados
                            NomeJogo = rdr[1].ToString(),

                            // //Atribui à propriedade Descricao o valor da terceiraa coluna da tabela do banco de dados
                            Descricao = rdr[2].ToString(),

                            //Atribui à propriedade DataLancamento o valor da quarta coluna da tabela do banco de dados
                            DataLancamento = Convert.ToDateTime(rdr[3]),

                            //Atribui à propriedade Valor o valor da quinta coluna da tabela do banco de dados
                            Valor = Convert.ToDecimal(rdr[4]),

                            //Atribui à propriedade IdEstudio o valor da sexta coluna da tabela do banco de dados
                            IdEstudio = Convert.ToInt32(rdr[5])

                        };

                        // Adiciona o objeto jogos criado à lista
                        listaJogos.Add(jogos);
                    }
                }
            }

            //retorna a lista de jogos
            return listaJogos;
        }
    }
}
