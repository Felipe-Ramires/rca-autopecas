using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RcaAutopecas.WebApp.Data
{
    public class Context : IdentityDbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }
    }
}
