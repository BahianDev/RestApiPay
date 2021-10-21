using System;
using deCasa.Repositories;
using deCasa.Dtos;
using System.Threading.Tasks;
using deCasa.Models;

namespace deCasa.Services
{
  public class TransactionService : ITransactionService
  {
    private readonly ITransactionRepository _transactionRepo;
    private readonly IUserRepository _userRepo;
    public TransactionService(ITransactionRepository transactionRepo, IUserRepository userRepository)
    {
      _transactionRepo = transactionRepo;
      _userRepo = userRepository;
    }

    public async Task<bool> Transfer(CreateTransactionDto createTransactionDto)
    {
      var isPayerValid = IsPayerValidAsync(createTransactionDto.PayerId);
      if (isPayerValid)
      {
        var transactionCommited = await _userRepo.TransferAsync(payerId: createTransactionDto.PayerId, payeeId: createTransactionDto.PayeeId, value: createTransactionDto.Value);
        if (transactionCommited)
        {
          try
          {
            await _transactionRepo.InsertTransaction(from: createTransactionDto.PayerId, to: createTransactionDto.PayeeId, value: createTransactionDto.Value);
          }
          catch
          {
            return false;
          }
        }
        return transactionCommited;
      }
      return false;
    }


    private bool IsPayerValidAsync(string id)
    {
      var user = _userRepo.GetNormalPerson(id);
      return !(user is null);
    }
  }
}