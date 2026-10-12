using Microsoft.EntityFrameworkCore;
using Finity.Api.Models.Cadastro;


namespace Finity.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            // Constructor logic here
            
        }

        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Perfil> Perfis { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
