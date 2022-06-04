using SanTsgBootcampProject.Domain.Users;
using System.Threading.Tasks;

namespace SanTsgBootcampProject.Application.Interfaces
{
    public interface IUserService
    {
        Task CreateUser(User user);
    }
}
