using SanTsgBootcampProject.Application.Models;
using System.Threading.Tasks;

namespace SanTsgBootcampProject.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
