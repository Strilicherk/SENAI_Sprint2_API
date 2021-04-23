using senai_InLock_webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_InLock_webApi.Interfaces
{
    /// <summary>
    /// Interface responsável pelo repoistório UsuarioRepository
    /// </summary>
    interface IUsuariosRepository
    {
        /// <summary>
        /// Valida o usuário
        /// </summary>
        /// <param name="email">email do usuário</param>
        /// <param name="senha">senha do usuário</param>
        /// <returns>Um objeto do tipo UsuariosDomain que foi buscado</returns>
        UsuariosDomain BuscarPorEmailSenha(string email, string senha);
    }
}
