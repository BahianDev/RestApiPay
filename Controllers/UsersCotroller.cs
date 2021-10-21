using Microsoft.AspNetCore.Mvc;
using deCasa.Repositories;
using System.Threading.Tasks;
using deCasa.Models;
using System.Collections.Generic;
using deCasa.Dtos;

namespace deCasa.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class UsersController : ControllerBase
  {
    private readonly IUserRepository _userRepository;
    public UsersController(IUserRepository userRepository)
    {
      _userRepository = userRepository;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
      var user = await _userRepository.Get(id);
      if (user == null)
        return NotFound();

      return Ok(user);
    }
    [HttpGet()]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers(int id)
    {
      var users = await _userRepository.GetAll();
      return Ok(users);
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(CreateUserDto createUserDto)
    {
      var user = new NormalPerson
      {
        Name = createUserDto.Name,
        Password = createUserDto.Password,
        Email = createUserDto.Email,
        Wallet = createUserDto.Wallet,
        Cpf = createUserDto.Cpf
      };

      await _userRepository.Add(user);

      return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteUser(int id)
    {
      await _userRepository.Delete(id);
      return Ok();
    }

    [HttpPost]
    [Route("Mockusers")]
    public async Task<ActionResult> MockUsers()
    {
      await _userRepository.MockUsers();
      return Ok();
    }
  }
}