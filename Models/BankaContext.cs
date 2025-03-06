namespace WebTemplate.Models;

public class BankaContext : DbContext
{
    public DbSet<Korisnik> Korisnici { get; set;}
    public DbSet<Racun> Racuni { get; set; }
    public DbSet<Transakcija> Transakcije { get; set; }
    public DbSet<Stednja> Stednje { get; set; }
    public BankaContext(DbContextOptions options) : base(options)
    {
        
    }
   
}