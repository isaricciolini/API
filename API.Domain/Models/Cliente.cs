using API.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Cliente
    {
        public Cliente()
        {
            //Logins = new HashSet<Login>();
        }

        public Guid IdCliente { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Endereco { get; set; }
        public string? Complemento { get; set; }
        public int Numero { get; set; }
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string Pais { get; set; }

        [InverseProperty(nameof(Login.CodClienteNavigation))]
        public virtual List<Login> Logins { get; set; }
    }
}
