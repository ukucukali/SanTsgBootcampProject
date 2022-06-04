using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using SanTsgBootcampProject.Application.Interfaces;
using SanTsgBootcampProject.Application.Models;
using SanTsgBootcampProject.Shared.SettingsModel;
using System.Threading.Tasks;

namespace SanTsgBootcampProject.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;


        //The IOptions service is used to bind strongly types options class to configuration section and registers it to the Asp.Net Core Dependency Injection Service Container as singleton lifetime. It exposes a Value property which contains your configured TOptions class.
        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var builder = new BodyBuilder
            {
                HtmlBody = mailRequest.Body
            };

            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_emailSettings.Mail),
                Subject = mailRequest.Subject,
                Body = builder.ToMessageBody()
            };
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.From.Add(MailboxAddress.Parse("SanTsg"));
            using var smtp = new SmtpClient();
            smtp.Connect(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailSettings.Mail, _emailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
