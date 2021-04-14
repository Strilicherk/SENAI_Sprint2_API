using System;
using senai_filmes_webApi.Domains;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_filmes_webApi.Interfaces
{
    /// <summary>
    /// Interface responsável pelo repositório FilmeRepository
    /// </summary>
    interface IFilmeRepository
    {
        // TipoRetorno NomeMetodo(TipoParametro NomeParametro);
        /// <summary>
        /// Lista todos os filmes
        /// </summary>
        /// <returns>Uma lista de filmes</returns>
        List<FilmeDomain> ListarTodos();
        /// <summary>
        /// Busca um filme através do seu id
        /// </summary>
        /// <param name="id">id do filme que será buscado</param>
        /// <returns>O filme que foi buscado</returns>
        FilmeDomain BuscarPorId(int id);
        /// <summary>
        /// Cadastra um novo filme
        /// </summary>
        /// <param name="cadastrarFilme">Objeto novoFilme com as informações que serão cadastradas</param>
        void Cadastrar(FilmeDomain novoFilme);
        /// <summary>
        /// Atualiza um filme existente passando o id pelo corpo da requisição
        /// </summary>
        /// <param name="filme">Objeto filme com as novas informações</param>
        void AtualizarIdCorpo(FilmeDomain filme);
        /// <summary>
        ///  Atualiza um gênero existente passando o id pela URL da requisição
        /// </summary>
        /// <param name="id">id do gênero que será atualizado</param>
        /// <param name="filme">Objeto filme com as novas informações</param>
        void AtualizarIdUrl(int id, FilmeDomain filme);
        /// <summary>
        /// Deleta um filme a partir do id
        /// </summary>
        /// <param name="id">id do filme que será deletado</param>
        void Deletar(int id);
    }
}
