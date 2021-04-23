using senai_InLock_webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_InLock_webApi.Interfaces
{
    interface IEstudiosRepository
    {
        // TipoRetorno NomeMetodo(TipoParametro NomeParametro);

        /// <summary>
        /// Lista todos os estudios e seus jogos
        /// </summary>
        /// <returns>Uma lista de estudios com seus jogos</returns>
        List<EstudiosDomain> ListarTodos();
    }
}
