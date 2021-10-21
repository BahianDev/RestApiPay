using MailKit.Net.Smtp;
using MailKit.Security;
using System.Threading.Tasks;
using deCasa.Models;
using deCasa.Settings;
using Microsoft.Extensions.Options;
using System.IO;
using MimeKit;
using deCasa.Repositories;

namespace deCasa.Services
{
  public class MailService : IMailService
  {
    private readonly MailSettings _mailSettings;
    private readonly IUserRepository _userRepository;

    public MailService(IOptions<MailSettings> options, IUserRepository userRepository)
    {
      _mailSettings = options.Value;
      _userRepository = userRepository;
    }
    public async Task SendEmailAsync(MailRequest mailRequest)
    {
      var toEmail = _userRepository.GetUserEmail(mailRequest.ToEmail);
      var email = new MimeMessage();
      email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
      email.To.Add(MailboxAddress.Parse(toEmail));
      email.Subject = mailRequest.Subject;
      var builder = new BodyBuilder();
      if (mailRequest.Attachments != null)
      {
        byte[] fileBytes;
        foreach (var file in mailRequest.Attachments)
        {
          if (file.Length > 0)
          {
            using (var ms = new MemoryStream())
            {
              file.CopyTo(ms);
              fileBytes = ms.ToArray();
            }
            builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
          }
        }
      }
      builder.HtmlBody = mailRequest.Body;
      email.Body = builder.ToMessageBody();
      using var smtp = new SmtpClient();
      smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
      smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
      await smtp.SendAsync(email);
      smtp.Disconnect(true);
    }
  }
}