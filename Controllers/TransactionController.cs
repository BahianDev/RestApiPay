using Microsoft.AspNetCore.Mvc;
using deCasa.Services;
using System.Threading.Tasks;
using deCasa.Dtos;
using deCasa.Models;
using System;

namespace deCasa.Controllers
{
  [ApiController]
  [Route("[controller]")]

  public class TransactionController : ControllerBase
  {
    private readonly ITransactionService _transactionService;
    private readonly IMailService _mailService;

    public TransactionController(ITransactionService transactionService, IMailService mailService)
    {
      _transactionService = transactionService;
      _mailService = mailService;
    }

    [HttpPost]
    public async Task<ActionResult<Transaction>> AddTransaction([FromBody] CreateTransactionDto createTransactionDto)
    {
      var success = await _transactionService.Transfer(createTransactionDto);

      if (success)
      {
        try
        {
          await _mailService.SendEmailAsync(new MailRequest
          {
            ToEmail = createTransactionDto.PayerId,
            Body = "Transaferencia realizaca com sucesso",
            Subject = "Transaferencia realizaca com sucesso"

          });
          return Ok();
        }
        catch (Exception ex)
        {

          throw ex;
        }
      }

      return Ok(success);
    }
  }

}