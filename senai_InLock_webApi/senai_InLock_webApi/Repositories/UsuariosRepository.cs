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
    /// Classe responsável pelo repositorio dos Usuarios
    /// </summary>
    public class UsuariosRepository : IUsuariosRepository
    {
        /// <summary>
        /// String de conexão com o banco de dados
        /// Data Source = nome do servidor
        /// initial catalog = nome do banco de dados
        /// user Id= id do usuário; pwd = senha; = faz a autenticação com o usuário
        /// </summary>
        public string stringConexao = "Data Source = DESKTOP-Q0JOIHU\\SQLEXPRESS; initial catalog = InLock_Games; user Id = sa; pwd=strilicherk2012n";

        /// <summary>
        /// Valida o usuário
        /// </summary>
        /// <param name="email">e-mail do usuário</param>
        /// <param name="senha">senha do usuário</param>
        /// <returns>Um objeto do tipo UsuariosDomain que foi buscado</returns>
        public UsuariosDomain BuscarPorEmailSenha(string email, string senha)
        {
            // Declara a SqlConnection con passando a string de conexão como parâmetro
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Define a query a ser executada no banco de dados
                string querySelect = "SELECT * FROM Usuarios WHERE Email = @Email AND Senha = @Senha";

                // Define o comando passando a query e a conexão como parâmetros
                using (SqlCommand cmd = new SqlCommand(querySelect, con))
                {
                    // Define os valores dos parâmetros
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Senha", senha);

                    // Abre a conexão com o banco de dados
                    con.Open();

                    // Executa o comando e armazena os dados no objeto rdr
                    SqlDataReader rdr = cmd.ExecuteReader();

                    // Caso dados tenham sido obtidos
                    if (rdr.Read())
                    {
                        // Cria um objeto usuarioBuscado
                        UsuariosDomain usuarioBuscado = new UsuariosDomain
                        {
                            // Atribui às propriedades os valores das colunas
                            IdUsuario = Convert.ToInt32(rdr["IdUsuario"]),
                            Email = rdr["Email"].ToString(),
                            Senha = rdr["Senha"].ToString(),
                            IdTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"])

                        };
                        // Retorna o objeto usuarioBuscado
                        return usuarioBuscado;
                    }

                    // Caso não encontre um e-mail e senha correspondentes, retorna null
                    return null;

                }
            }
        }
    }
}
