using Microsoft.EntityFrameworkCore;
using MacToDatabaseModel;
namespace InmemoryDb
{
    public class MacContext : DbContext
    {
        public MacContext(DbContextOptions<MacContext> options) : base ( options)
        {
        }
        public DbSet<MacToDatabase> MacstoDbs {get; set;} 
    }
    
}