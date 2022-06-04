using SanTsgBootcampProject.Domain.SharedConstants;
using SanTsgBootcampProject.Domain.Users;
using System.Collections.Generic;

namespace SanTsgBootcampProject.Data.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        //all the custom methods for users are declared here
        public List<User> GetActiveUsers();
        public List<User> GetDeactiveUsers();
        public List<User> GetDeletedUsers();
        public Status ChanceCurrentStatus(User user);

    }
}
