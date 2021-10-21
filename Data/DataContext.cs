using Microsoft.EntityFrameworkCore;
using deCasa.Models;

namespace deCasa.Data
{
  public class DataContext : DbContext, IDataContext
  {
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<User>()
            .Property(f => f.Id)
            .ValueGeneratedOnAdd();

      modelBuilder.Entity<Transaction>()
            .Property(f => f.Id)
            .ValueGeneratedOnAdd();

      modelBuilder.Entity<User>()
          .HasIndex(u => u.Email)
          .IsUnique();

    }
    public DbSet<User> Users { get; set; }

    public DbSet<LegalPerson> LegalPersons { get; set; }

    public DbSet<NormalPerson> NomalPersons { get; set; }

    public DbSet<Transaction> Transactions { get; set; }
  }
}