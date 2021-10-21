using deCasa.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace deCasa.Repositories
{
  public interface IUserRepository
  {
    Task<User> Get(int id);
    Task<bool> TransferAsync(string payerId, string payeeId, decimal value);
    Task<IEnumerable<User>> GetAll();
    Task Add(User user);
    Task Delete(int id);
    Task Update(User user);
    NormalPerson GetNormalPerson(string id);
    Task MockUsers();

    string GetUserEmail(string id);
  }
}