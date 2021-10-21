using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using deCasa.Models;

namespace deCasa.Data
{
  public interface IDataContext
  {
    DbSet<User> Users { get; set; }
    DbSet<LegalPerson> LegalPersons { get; set; }

    DbSet<NormalPerson> NomalPersons { get; set; }

    DbSet<Transaction> Transactions { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
  }
}