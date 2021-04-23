using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Classe que representa a entidade (tabela) TiposUsuario
/// </summary>
namespace senai_InLock_webApi.Domains
{
    public class TiposUsuarioDomain
    {
        public int IdTipoUsuario { get; set; }
        public string Titulo { get; set; }
    }
}
