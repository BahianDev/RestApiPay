using System;
using deCasa.Data;
using deCasa.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace deCasa.Repositories
{
  public class UserRepository : IUserRepository
  {
    private readonly IDataContext _context;
    public UserRepository(IDataContext context)
    {
      _context = context;
    }

    public async Task<bool> TransferAsync(string payerId, string payeeId, decimal value)
    {
      try
      {
        if (IsWithdrawAllowed(payerId, value))
        {
          var payer = await _context.Users.FindAsync(payerId);
          var payee = await _context.Users.FindAsync(payeeId);
          payer.Wallet -= value;
          payee.Wallet += value;
        }
        else
        {
          throw new Exception();
        }
      }
      catch
      {
        return false;
      }

      await _context.SaveChangesAsync();

      return true;
    }
    public async Task<User> Get(int id)
    {
      return await _context.Users.FindAsync(id);
    }
    public async Task<IEnumerable<User>> GetAll()
    {
      return await _context.Users.ToListAsync();
    }
    public async Task Add(User user)
    {
      _context.Users.Add(user);
      await _context.SaveChangesAsync();
    }
    public async Task Delete(int id)
    {
      var userToDelete = await _context.Users.FindAsync(id);
      if (userToDelete == null)
      {
        throw new NullReferenceException();
      }
      _context.Users.Remove(userToDelete);
      await _context.SaveChangesAsync();
    }
    public string GetUserEmail(string id)
    {
      return _context.Users.Where(user => user.Id == id).FirstOrDefault().Email;
    }
    public async Task Update(User user)
    {
      var userToUpdate = await _context.Users.FindAsync(user.Id);
      if (userToUpdate == null)
      {
        throw new NullReferenceException();
      }
      userToUpdate.Name = user.Name;
      userToUpdate.Email = user.Email;

    }
    public NormalPerson GetNormalPerson(string id)
    {
      var query = _context.Users.AsQueryable<User>().OfType<NormalPerson>().Where(user => user.Id == id);

      return query.FirstOrDefault();

    }
    private bool IsWithdrawAllowed(string id, decimal value)
    {
      var balance = _context.Users.Where(u => u.Id == id).FirstOrDefault().Wallet;
      return balance >= value;
    }

    public async Task MockUsers()
    {
      var userList = new List<User>
      {
        new NormalPerson()
        {
          Name = "Red Guy",
          Cpf = "12345678911",
          Password = "$2y$12$1x//ASzyeZ0Tegxk3eObe.zsEjE4pfpVF/pzyYn9SEeEjz5UrBhBa",
          Email = "redguy@gmail.com",
          Wallet = new decimal(5000.00)
        },
        new NormalPerson()
        {
          Name = "Yellow Guy",
          Cpf = "12345678912",
          Password = "$2y$12$/1HVewZglsliW9FLoP0.jeLQNkF1K0CWU1HdV7OqLDfotr6ELkrBO",
          Email = "yellowguy@gmail.com",
          Wallet = new decimal(5000.00)
        },
        new NormalPerson()
        {
          Name = "Duck",
          Cpf = "12345678913",
          Password = "$2y$12$tjQiNdcnQvj3k2RWM5VNY.dLe4sEBNcBwUOox/q76jjkDy.jm2ZBC",
          Email = "duck@gmail.com",
          Wallet = new decimal(5000.00)
        },
        new LegalPerson()
        {
          Name = "Tony the Clock",
          Cnpj = "12345678912345",
          Password = "$2y$12$BrUGsxFcQepDqFctxVEx6eR5NRyNM28p8LaX8jLLg/kDlnergnqrS",
          Email = "tonytheclock@gmail.com",
          Wallet = new decimal(20000.00)
        },
        new LegalPerson()
        {
          Name = "Colin the Computer",
          Cnpj = "12345678912346",
          Password = "$2y$12$WU/MTfVJ2AM8/.zhG/NJq.QK7j5UryAtPgzvQZs9fTLN0pdeiMFCC",
          Email = "colinthecomputer@gmail.com",
          Wallet = new decimal(20000.00)
        },
        new LegalPerson()
        {
          Name = "Sketchbook",
          Cnpj = "123456789123457",
          Password = "$2y$12$rv87/eQpjh.mJsqSVrW7he3cTPRl0MAFQ/XN3zqRx5N.uOT/Jzz0y",
          Email = "sketchbook@gmail.com",
          Wallet = new decimal(20000.00)
        }
      };
      userList.ForEach(n => _context.Users.Add(n));
      await _context.SaveChangesAsync();
    }

  }
}