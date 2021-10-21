using deCasa.Data;
using deCasa.Models;
using System.Threading.Tasks;
using System;


namespace deCasa.Repositories
{
  public class TransactionRepository : ITransactionRepository
  {
    private readonly IDataContext _context;
    public TransactionRepository(IDataContext context)
    {
      _context = context;
    }
    public async Task InsertTransaction(string from, string to, decimal value)
    {
      var transaction = new Transaction() { PayerId = from, PayeeId = to, Value = value };
      _context.Transactions.Add(transaction);
      await _context.SaveChangesAsync();
    }
  }
}
