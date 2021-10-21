using System.Threading.Tasks;
using deCasa.Models;

namespace deCasa.Repositories
{
  public interface ITransactionRepository
  {
    Task InsertTransaction(string from, string to, decimal value);
  }
}