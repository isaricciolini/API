using API.Domain.Models;
using API.Infra;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("CORS")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class LoginsController : ControllerBase
    {
        private readonly ILogger<LoginsController> _logger;
        private Context _context;

        public LoginsController(ILogger<LoginsController> logger, Context context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Consulta todos os logins cadastrados.
        /// </summary>
        /// <response code="200">Exibe todas as receitas cadastradas.</response>
        /// <response code="500">Erro interno do servidor.</response>
        [HttpGet("/logins/consultar")]
        public ActionResult<List<Login>> Get()
        {
            try
            {
                var logins = _context.Logins.ToList();
                return Ok(logins);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro na pesquisa: {ex.Message}");
                return BadRequest();
            }
        }

        /// <summary>
        /// Cadastra um login.
        /// </summary>
        /// <response code="200">Login cadastrado com sucesso.</response>
        /// <response code="400">Algum atributo informado inválido.</response>
        /// <response code="500">Erro interno de servidor.</response>
        [HttpPost("/logins/cadastrar")]
        public ActionResult<Login> Post([FromQuery]Login login)
        {
            try
            {
                if (String.IsNullOrEmpty(login.Usuario) || login.Usuario.Length < 3)
                    return BadRequest("Usuário deve conter no mínimo 3 caracteres");
                if (String.IsNullOrEmpty(login.Senha) || login.Senha.Length < 8 || login.Senha.Length > 14)
                    return BadRequest("Senha deve conter no mínimo 8 caracteres e no máximo 14 caracteres");

                //login.IdCliente = cliente.IdCliente;
                _context.Logins.Add(login);
                _context.SaveChanges();
                return Ok(login);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro no cadastro: {ex.Message}");
                return BadRequest();
            }
        }

        /// <summary>
        /// Altera um login.
        /// </summary>
        /// <response code="200">Login alterado com sucesso.</response>
        /// <response code="400">Algum parâmetro informado inválido ou os dados não foram encontrados.</response>
        /// <response code="500">Erro interno de servidor.</response>
        [HttpPut("/logins/alterar")]
        public ActionResult<Login> Put([FromQuery]Login login)
        {
            if (String.IsNullOrEmpty(login.Usuario))
                return BadRequest("Usuário não pode ser nulo.");
            if (String.IsNullOrEmpty(login.Senha) || login.Senha.Length < 8 || login.Senha.Length > 14)
                return BadRequest("Senha deve conter no mínimo 8 caracteres e no máximo 14 caracteres");

            try
            {
                _context.Logins.Update(login);
                _context.SaveChanges();
                return Ok(login);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro na alteração: {ex.Message}");
                return BadRequest();
            }
        }


        /// <summary>
        /// Exclui um login através do CodFuncionario informado.
        /// </summary>
        /// <response code="200">Login excluído com sucesso.</response>
        /// <response code="400">Parâmetro inválido informado ou o login a ser excluído não foi encontrado.</response>
        /// <response code="500">Erro interno do servidor.</response>
        [HttpDelete("/logins/excluir/{id}")]
        public ActionResult Delete(Guid id)
        {
            try
            {
                var retorno = _context.Logins.FirstOrDefault(l => l.IdLogin == id);

                if (retorno == null)
                    return BadRequest("Login não encontrado.");

                _context.Logins.Remove(retorno);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro na exclusão: {ex.Message}");
                return BadRequest();
            }
        }
    }
}
