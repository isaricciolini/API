using API.Domain.Models;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Infra
{
    public class Context : DbContext
    {
        public Context() { }
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Login> Logins { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=ISABELLE;Database=Indique;User Id=sa;Password=a2m8x7h5; Trusted_Connection=True;", builder =>
            {
                builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });
            
            optionsBuilder.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(c => c.IdCliente);

                entity.Property(c => c.Nome).HasMaxLength(256).IsRequired();

                entity.Property(c => c.Cpf).HasMaxLength(15).IsRequired();

                entity.Property(c => c.Telefone).HasMaxLength(15).IsRequired();

                entity.Property(c => c.Email).HasMaxLength(128).IsRequired();

                entity.Property(c => c.Endereco).HasMaxLength(256).IsRequired();

                entity.Property(c => c.Complemento).HasMaxLength(128);

                entity.Property(c => c.Cep).HasMaxLength(9).IsRequired();

                entity.Property(c => c.Bairro).HasMaxLength(64).IsRequired();

                entity.Property(c => c.Cidade).HasMaxLength(64).IsRequired();

                entity.Property(c => c.UF).HasMaxLength(2).IsRequired();

                entity.Property(c => c.Pais).HasMaxLength(64).IsRequired();
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.HasKey(l => l.IdLogin);

                entity.Property(l => l.Usuario).HasMaxLength(40).IsRequired();

                entity.Property(l => l.Senha).HasMaxLength(14).IsRequired();

                entity.HasOne(d => d.CodClienteNavigation)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Logins_Clientes");
            });
        }
    }
}
