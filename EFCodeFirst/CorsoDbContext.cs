using Microsoft.EntityFrameworkCore;

namespace EFCodeFirst
{
    public class UniversityDbContext : DbContext
    {

        public UniversityDbContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["BlogDbContextDatabase"].ConnectionString);
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=UniversityDb; User id = sa ; password = Pass@word01;Trusted_Connection=False");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Corso>()
                     .HasMany(e => e.Students)
                     .WithOne(e => e.Corso)
                     .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Corso> Corso { get; set; }
        public DbSet<Studente> Studente { get; set; }

    }
}


