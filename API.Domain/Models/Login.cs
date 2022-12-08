using API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Domain.Models
{
    public partial class Login
    {
        [Key]
        public Guid IdLogin { get; set; }
        [Required]
        [StringLength(40)]
        public string Usuario { get; set; }
        [Required]
        [StringLength(14)]
        public string Senha { get; set; }
        public Guid IdCliente { get; set; }

        [ForeignKey(nameof(IdCliente))]
        [InverseProperty(nameof(Cliente.Logins))]
        public virtual Cliente CodClienteNavigation { get; set; }
    }
}
