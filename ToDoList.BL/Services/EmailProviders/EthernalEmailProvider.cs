using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using ToDoList.BL.Helpers;
using ToDoList.BL.ServiceInterfaces;

namespace ToDoList.BL.Services.EmailProviders
{
    public class EthernalEmailProvider : IEmailProvider
    {
        private readonly SmtpSettings _smtpSettings;

        public EthernalEmailProvider(IOptions<SmtpSettings> smtpSettingsAccessor)
        {
            _smtpSettings = smtpSettingsAccessor.Value;
        }

        public void Send(string to, string subject, string html)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_smtpSettings.Login));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) {Text = html};

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect(_smtpSettings.Host, _smtpSettings.Port,
                SecureSocketOptions.StartTls);
            smtp.Authenticate(_smtpSettings.Login, _smtpSettings.Password);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}