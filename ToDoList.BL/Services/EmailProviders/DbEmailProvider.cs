using System;
using Microsoft.Extensions.Options;
using ToDoList.BL.Helpers;
using ToDoList.BL.RepositoryInterfaces;
using ToDoList.BL.ServiceInterfaces;
using ToDoList.Models.Entity;

namespace ToDoList.BL.Services.EmailProviders
{
    public class DbEmailProvider : IEmailProvider
    {
        private readonly IRepository<Email> _emailRepository;
        private readonly SmtpSettings _smtpSettings;

        public DbEmailProvider(IRepository<Email> emailRepository, IOptions<SmtpSettings> smtpSettingsAccessor)
        {
            _emailRepository = emailRepository;
            _smtpSettings = smtpSettingsAccessor.Value;
        }

        public void Send(string receiveUserMail, string subject, string html)
        {
            var email = new Email()
            {
                ReceiveUserEmail = receiveUserMail,
                Content = html,
                Subject = subject,
                SendDate = DateTime.UtcNow,
                SendUserEmail = _smtpSettings.Login
            };

            _emailRepository.Add(email);
        }
    }
}