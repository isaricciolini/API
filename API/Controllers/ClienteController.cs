using API.Infra;
using API.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("CORS")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class ClienteController : ControllerBase
    {
        private readonly ILogger<ClienteController> _logger;
        private Context _context;

        public ClienteController(ILogger<ClienteController> logger, Context context)
        {
            _context = context;
            _logger = logger;
        }


        [HttpGet("/clientes/consultar")]
        public ActionResult<List<Cliente>> Get()
        {
            var clientes = _context.Clientes.ToList();

            _logger.LogInformation($"Clientes encontrados: {clientes.Count()}");

            return Ok(clientes);
        }

        [HttpPut("/clientes/alterar")]
        public ActionResult<Cliente> Put([FromQuery]Cliente cliente)
        {
            try
            {
                var retorno = _context.Clientes.FirstOrDefault(c => c.IdCliente == cliente.IdCliente);
                if (retorno != null)
                {
                    retorno.Nome = cliente.Nome;
                    retorno.DataNascimento = cliente.DataNascimento;
                    retorno.Cpf = cliente.Cpf;
                    retorno.Telefone = cliente.Telefone;
                    retorno.Email = cliente.Email;
                    retorno.Bairro = cliente.Bairro;
                    retorno.Cep = cliente.Cep;
                    retorno.UF = cliente.UF;
                    retorno.Cidade = cliente.Cidade;
                    retorno.Pais = cliente.Pais;
                    retorno.Complemento = cliente.Complemento;
                    retorno.Endereco = cliente.Endereco;
                    retorno.Numero = cliente.Numero;

                    _context.Update(retorno);
                    _context.SaveChanges();

                    _logger.LogInformation($"ALTERAÇÃO - Cliente alterado: {cliente.IdCliente}");

                    return Ok(retorno);
                }
                else
                {
                    _logger.LogInformation($"ALTERAÇÃO - Cliente não encontrado: {cliente.IdCliente}");
                    return BadRequest();
                }
            }
            catch(Exception ex)
            {
                _logger.LogInformation($"ALTERAÇÃO - Cliente não alterado: {cliente.IdCliente} - Erro: {ex.Message}");
                return BadRequest();
            }
            
        }
        /*
        [HttpPost("/cadastrar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Cliente> Post([FromQuery]Cliente cliente)
        {
            try
            {
                _context.Add(cliente);
                _context.SaveChanges();

                _logger.LogInformation($"CADASTRO - Cliente cadastrado: {cliente.IdCliente}");

                return Ok(cliente);
            }catch(Exception ex)
            {
                _logger.LogInformation($"CADASTRO - Cliente não cadastrado: {cliente.IdCliente} - Erro: {ex.Message}");
                return BadRequest();
            } 
        }
        */
        [HttpDelete("/clientes/excluir")]
        public ActionResult Delete([FromQuery]Guid id)
        {
            try
            {
                var retorno = _context.Clientes.FirstOrDefault(c => c.IdCliente == id);

                if (retorno != null)
                {
                    var login = _context.Logins.FirstOrDefault(l => l.IdCliente == retorno.IdCliente);
                    if (login != null)
                    {
                        _context.Logins.Remove(login);
                    }
                    _context.Remove(retorno);
                    _context.SaveChanges();

                    _logger.LogInformation($"EXCLUSÃO - Cliente removido: {id}");

                    return Ok();
                }
                else
                {
                    _logger.LogInformation($"EXCLUSÃO - Cliente não encontrado: {id}");
                    return BadRequest();
                }
            }
            catch(Exception ex)
            {
                _logger.LogInformation($"EXCLUSÃO - Cliente não removido: {id} - Erro: {ex.Message}");
                return BadRequest();
            }
        }
    }
}
