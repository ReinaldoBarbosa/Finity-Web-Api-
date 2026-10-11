using Microsoft.EntityFrameworkCore;

namespace Finity.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            // Constructor logic here
            
        }
    }
}
