using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai_InLock_webApi.Domains;
using senai_InLock_webApi.Interfaces;
using senai_InLock_webApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_InLock_webApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class EstudiosController : ControllerBase
    {
        private IEstudioRepository _estudioRepository { get; set; }
        public EstudiosController()
        {
            _estudioRepository = new EstudioRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            // Retorna a resposta da requisição fazendo a chamada para o método
            return Ok(_estudioRepository.Listar());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_estudioRepository.BuscarPorId(id));
        }

        [HttpPost]
        public IActionResult Post(Estudio novoEstudio)
        {
            // Chama o método
            _estudioRepository.Cadastrar(novoEstudio);
            // Retorna status code
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Faz a chamada para o método
            _estudioRepository.Deletar(id);

            // Retorna um status code
            return StatusCode(204);

        }

        [HttpGet("jogos")]
        public IActionResult GetGames()
        {
            // Retorna a resposta da requisição fazendo a chamada para o método
            return Ok(_estudioRepository.ListarJogos());
        }

    }
}
