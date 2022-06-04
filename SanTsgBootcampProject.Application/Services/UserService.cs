using SanTsgBootcampProject.Application.Interfaces;
using SanTsgBootcampProject.Application.Models;
using SanTsgBootcampProject.Data.Repositories.Interfaces;
using SanTsgBootcampProject.Domain.Users;
using System.Threading.Tasks;

namespace SanTsgBootcampProject.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public UserService(IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }
        public async Task CreateUser(User user)
        {

            _unitOfWork.Users.Add(user);

            MailRequest mail = new MailRequest()
            {
                Body = "Succesfully Registered",
                Subject = "Registration Confirmation",
                ToEmail = user.EmailAddress
            };

            await _emailService.SendEmailAsync(mail);
            _unitOfWork.Save();
        }
    }
}
