using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace senai_InLock_webApi.Domains
{
    /// <summary>
    /// Classe que representa a entidade (tabela) Jogos
    /// </summary>
    public class JogosDomain
    {
        public int IdJogo { get; set; }
        public string NomeJogo { get; set; }
        public string Descricao { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataLancamento { get; set; }
        public Decimal Valor { get; set; }

        public int IdEstudio { get; set; }
    }
}
