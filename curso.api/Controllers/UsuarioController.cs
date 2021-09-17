using curso.api.Business.Entities;
using curso.api.Business.Repositories;
using curso.api.Filters;
using curso.api.models;
using curso.api.models.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace curso.api.Controllers
{
    /// <summary>
    /// Classe de controle de acesso e criação de usuários
    /// </summary>
    [Route("api/v1/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuarioRepository _usuarioReporitory;        
        private readonly IAuthenticationService _authenticationService;        
        public UsuarioController(IUsuarioRepository usuarioReporitory, IAuthenticationService authenticationService)
        {
            _usuarioReporitory = usuarioReporitory;            
            _authenticationService = authenticationService;            
        }


        /// <summary>
        /// Este serviço permite Autenticar um usuário existente
        /// </summary>
        /// <param name= "loginViewModelInput"></param>
        /// <returns></returns>

        [SwaggerResponse(statusCode: 200, description: "Sucesso ao autenticar", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidaCampoViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErroGenericoViewModel))]
        [HttpPost]
        [Route("logar")]
        [ValidacaoModelStateCustomizado]
        public async Task<IActionResult> Logar(LoginViewModelInput loginViewModelInput) {

            var usuario = await _usuarioReporitory.ObterUsuarioAsync(loginViewModelInput.Login);

            if (usuario == null)
            {
                return BadRequest("Houve um erro ao tentar acessar.");
            }

            var usuarioViewModelOutput = new RegistroViewModelInput()
            {
                Codigo = usuario.Codigo,
                Login = usuario.Login,
                Email = usuario.Email
            };

            var token = _authenticationService.GerarToken(usuarioViewModelOutput);
            return Ok(new
            { 
                Token = token,
                Usuario = usuarioViewModelOutput
            });
        }

        /// <summary>
        /// Este serviço permite cadastrar um usuário não existente
        /// </summary>
        /// <param name= "loginViewModelInput">View model do registro login</param>
        /// <returns></returns>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao cadastrar", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidaCampoViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErroGenericoViewModel))]
        [HttpPost]
        [Route("registrar")] 
        [ValidacaoModelStateCustomizado]
        public async Task<IActionResult> Registrar(RegistroViewModelInput loginViewModelInput)
        {
            var usuario = await _usuarioReporitory.ObterUsuarioAsync(loginViewModelInput.Login);

            if (usuario != null)
            {
                return BadRequest("Usuário já cadastrado");
            }

            usuario = new Usuario
            {
                Login = loginViewModelInput.Login,
                Senha = loginViewModelInput.Senha,
                Email = loginViewModelInput.Email
            };




            _usuarioReporitory.Adicionar(usuario);
            _usuarioReporitory.Commit();

            return Created("", loginViewModelInput);
        }
    }
}
