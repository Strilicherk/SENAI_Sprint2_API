using senai_InLock_webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_InLock_webApi.Interfaces
{
    interface IJogosRepository
    {
        // TipoRetorno NomeMetodo(TipoParametro NomeParametro);

        /// <summary>
        /// Cadastra um novo jogo
        /// </summary>
        /// <param name="novoJogo">Objeto novoJogo com as informações que serão cadastradas</param>
        void Cadastrar(JogosDomain novoJogo);
         
        /// <summary>
        /// Lista todos jogos e seus respectivos estudios
        /// </summary>
        /// <returns>Uma lista de jogoss com seus estudios</returns>
        List<JogosDomain> ListarTodos();

    }
}
