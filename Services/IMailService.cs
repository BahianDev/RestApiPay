using System.Threading.Tasks;
using deCasa.Models;

namespace deCasa.Services
{
  public interface IMailService
  {
    Task SendEmailAsync(MailRequest mailRequest);
  }
}