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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .HasOne(a => a.Endereco)
                .WithOne(e => e.ApplicationUser)
                .HasForeignKey<Endereco>(e => e.ApplicationUserId);
        }
    }
}
