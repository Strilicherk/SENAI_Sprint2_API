using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace senai_InLock_webApi.Domains
{
    /// <summary>
    /// Classe que representa a entidade (tabela) Usuarios
    /// </summary>
    public class UsuariosDomain
    {
        public int IdUsuario { get; set; }

        // Define que o campo é obrigatório
        [Required(ErrorMessage = "O e-mail é obrigatório")]
        public string Email { get; set; }

        // Define que o campo é obrigatório, com no mínimo 3 e no máximo 20 caractéres
        [Required(ErrorMessage = "A senha é obrigatória")]
        [StringLength(20, MinimumLength =3, ErrorMessage ="O campo senha precisa ter no minimo 3 caracteres")]
        public string Senha { get; set; }
        public int IdTipoUsuario { get; set; }
    }
}
