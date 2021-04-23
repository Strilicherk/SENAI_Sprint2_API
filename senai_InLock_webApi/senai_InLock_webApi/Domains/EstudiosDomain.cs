using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Classe que representa a entidade (tabela) Estudios
/// </summary>
namespace senai_InLock_webApi.Domains
{
    public class EstudiosDomain
    {
        public int IdEstudio { get; set; }
        public string NomeEstudio { get; set; }
    }
}
