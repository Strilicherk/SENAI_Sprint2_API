using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai_InLock_webApi.Domains;
using senai_InLock_webApi.Interfaces;
using senai_InLock_webApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Controller responsável pelos endpoints referentes aos jogos
/// </summary>
namespace senai_InLock_webApi.Controllers
{
    // Define que o tipo de resposta da API será no formato json
    [Produces("application/json")]

    // Define que a rota de uma requisição será no formato dominio/api/nomeController
    // ex: https://localhost:5000/api/generos
    [Route("api/[controller]")]

    // Define que é um controlador de API
    [ApiController]
    public class JogosController : ControllerBase
    {
        /// <summary>
        /// Objeto _jogosRepository que irá receber todos os métodos definidos da interface IJogosRepository
        /// </summary>
        private IJogosRepository _jogosRepository { get; set; }

        /// <summary>
        /// Instancia o objeto _jogosRepository para que haja a referência aos métodos no repositório
        /// </summary>
        public JogosController()
        {
            _jogosRepository = new JogosRepository();
        }

        /// <summary>
        /// Lista todos os jogos e suas informações
        /// </summary>
        /// <returns>Uma lista de jogos e um status code</returns>
        /// http://localhost:5000/api/jogos
        /// Qualquer usuário logado pode listar
        /// [Authorize] => verifica se o usuário está logado
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            // Cria uma lista nomeada listaJogos para receber os dados
            List<JogosDomain> listaJogos = _jogosRepository.ListarTodos();

            // Retorna o status code 200 (Ok) com a lista de jogos no formato JSON
            return Ok(listaJogos);
        }

        /// <summary>
        /// Cadastra um novo jogo
        /// </summary>
        /// <param name="novoJogo">Objeto com as informações do jogo cadastrado</param>
        /// <returns>Um status code 201 - Created</returns>
        /// http://localhost:5000/api/jogos
        /// Somente o administrador pode cadastrar um novo jogo
        /// [Authorize(Roles = "1")] => verifica se o usuário está logado e se ele possui a permissão "1"(administrador)
        [Authorize(Roles = "1")] 
        [HttpPost]
        public IActionResult Post(JogosDomain novoJogo)
        {
            // Faz a chamada para o método .Cadastrar()
            _jogosRepository.Cadastrar(novoJogo);

            // Retorna um status code 201 - Created
            return StatusCode(201);
        }
    }
}
