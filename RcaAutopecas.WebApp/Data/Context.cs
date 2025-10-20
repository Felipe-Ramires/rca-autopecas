using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RcaAutopecas.WebApp.Models;

namespace RcaAutopecas.WebApp.Data
{
    public class Context : IdentityDbContext<ApplicationUser>
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Vendedor> Vendedores { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // A user can have an associated seller profile (one-to-one)
            builder.Entity<ApplicationUser>()
                .HasOne(a => a.Vendedor)
                .WithOne(v => v.ApplicationUser)
                .HasForeignKey<Vendedor>(v => v.ApplicationUserId);

            // A user can have an associated client profile (one-to-one)
            builder.Entity<ApplicationUser>()
                .HasOne(a => a.Cliente)
                .WithOne(c => c.ApplicationUser)
                .HasForeignKey<Cliente>(c => c.ApplicationUserId);

            // A client has one address (one-to-one)
            builder.Entity<Cliente>()
                .HasOne(c => c.Endereco)
                .WithOne(e => e.Cliente)
                .HasForeignKey<Endereco>(e => e.ClienteId);
        }
    }
}
