using FormulaABD.Models;
using Microsoft.EntityFrameworkCore;

namespace FormulaABD.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Pilota> Piloti { get; set; }
        public DbSet<Tracciato> Tracciati { get; set; }
        public DbSet<Risultato> Risultati { get; set; }
    }
}
