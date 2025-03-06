using Microsoft.EntityFrameworkCore;

namespace WebTemplate.Models
{
    public class BankaContext : DbContext
    {
        public BankaContext(DbContextOptions<BankaContext> options) : base(options) { }

        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Racun> Racuni { get; set; }
        public DbSet<Transakcija> Transakcije { get; set; }
        public DbSet<Stednja> Stednje { get; set; }
    }
}
