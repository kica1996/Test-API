using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;




namespace Test_API
{
    public class Korisnici:DbContext
    {
        public Korisnici() : base()
        {
            
        }
        public Korisnici(DbContextOptions<Korisnici> options)
    : base(options)
        { }

        protected override void OnConfiguring(
DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.;database=Testapi;trusted_connection=true;");
        }

        public DbSet<Korisnik> korisnik { get; set; }

        public DbSet<KorisnikUredjaj> korisnikUredj { get; set; }


    }
    
}

