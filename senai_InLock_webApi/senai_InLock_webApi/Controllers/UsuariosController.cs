using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using senai_InLock_webApi.Domains;
using senai_InLock_webApi.Interfaces;
using senai_InLock_webApi.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

/// <summary>
/// Controller responsável pelos endpoints referentes aos Usuários
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
    public class UsuariosController : ControllerBase
    {
        /// <summary>
        /// Objeto _usuariosRepository que irá receber todos os métodos definidos da interface IUsuariosRepository
        /// </summary>
        private IUsuariosRepository _usuariosRepository { get; set; }

        /// <summary>
        /// Instancia o objeto _usuariosRepository para que haja a referência aos métodos no repositório
        /// </summary>
        public UsuariosController()
        {
            _usuariosRepository = new UsuariosRepository();
        }

        /// <summary>
        /// Faz a autenticação do usuário
        /// </summary>
        /// <param name="login">objeto com os dados de e-mail e senha</param>
        /// <returns>Um status code e, em caso de sucesso, os dados do usuário buscado</returns>
        [HttpPost("Login")]
        public IActionResult Login(UsuariosDomain login)
        {
            // Busca o usuário pelo e-mail e senha
            UsuariosDomain usuarioBuscado = _usuariosRepository.BuscarPorEmailSenha(login.Email, login.Senha);

            // Caso não encontre nenhum usuário com o e-mail e senha informados
            if (usuarioBuscado == null)
            {
                // retorna NotFound com uma mensagem personalizada
                return NotFound("E-mail ou senha incorretos");
            }

            //Caso encontre, prossegue para a criação do token

            // Define os dados que serão fornecidos no token - Payload
            var claims = new[]
            {
                // Formato da Claim = TipoDaClaim, ValorDaClaim
                new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
                new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario.ToString()),
                new Claim(ClaimTypes.Role, usuarioBuscado.IdTipoUsuario.ToString()),
                new Claim("Claim personalizada", "valor teste")
            };

            // Define a chave de acesso ao token
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("InLock-key-autenticacao"));

            // Define as credenciais do token - Header
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Define a composição do token
            var token = new JwtSecurityToken(
                issuer : "InLock.webApi",               // emissor do token
                audience : "InLock.webApi",             // destinatário do token
                claims : claims,                        // dados definidos acima (linha 78)
                expires : DateTime.Now.AddMinutes(30),  // tempo de expiração
                signingCredentials: creds               // credenciais do token
                                                                        // 
            );

            // Retorna um status code 200 - Ok com o token criado
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    }
}
