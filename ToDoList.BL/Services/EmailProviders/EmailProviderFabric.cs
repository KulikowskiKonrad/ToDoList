using System;
using Microsoft.Extensions.Options;
using ToDoList.BL.Helpers;
using ToDoList.BL.RepositoryInterfaces;
using ToDoList.BL.ServiceInterfaces;
using ToDoList.Models.Entity;

namespace ToDoList.BL.Services.EmailProviders
{
    public class EmailProviderFabric : IEmailProviderFabric
    {
        private readonly AppSettings _appSettings;
        private readonly IOptions<SmtpSettings> _smtpSettingsAccessor;
        private readonly IRepository<Email> _emailRepository;

        public EmailProviderFabric(IOptions<AppSettings> appSettingsAccessor,
            IOptions<SmtpSettings> smtpSettingsAccessor, IRepository<Email> emailRepository)
        {
            _appSettings = appSettingsAccessor.Value;
            _smtpSettingsAccessor = smtpSettingsAccessor;
            _emailRepository = emailRepository;
        }

        public IEmailProvider GetEmailProvider()
        {
            switch (_appSettings.EmailProviderType)
            {
                case EmailProviderType.Smtp:
                    return new SmtpEmailProvider(_smtpSettingsAccessor);
                case EmailProviderType.Ethernal:
                    return new EthernalEmailProvider(_smtpSettingsAccessor);
                case EmailProviderType.Db:
                    return new DbEmailProvider(_emailRepository, _smtpSettingsAccessor);
                case EmailProviderType.Fake:
                    return new FakeEmailProvider();
                default:
                    throw new InvalidOperationException("No provider");
            }
        }
    }
}