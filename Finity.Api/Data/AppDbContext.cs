using Microsoft.EntityFrameworkCore;
using Finity.Api.Models;


namespace Finity.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            // Constructor logic here
            
        }

        public DbSet<Finity.Api.Models.Empresa> Empresas { get; set; }
    }
}
