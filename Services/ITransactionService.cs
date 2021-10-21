using System.Threading.Tasks;
using deCasa.Dtos;

namespace deCasa.Services
{
  public interface ITransactionService
  {
    Task<bool> Transfer(CreateTransactionDto createTransactionDto);
  }
}