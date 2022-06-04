using SanTsgBootcampProject.Data.Repositories.Interfaces;
using SanTsgBootcampProject.Domain.SharedConstants;
using SanTsgBootcampProject.Domain.Users;
using System.Collections.Generic;
using System.Linq;

namespace SanTsgBootcampProject.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        //Repository depends on AppDbContext and we inherit this repository that's why we should initialize it here as well
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        /// <summary>
        /// Returns a list of users with active status, returns empty when there are no users
        /// </summary>
        /// <returns>list of users</returns>
        public List<User> GetActiveUsers()
        {
            return _context.Users.Where(status => status.Status == Status.Active).ToList();
        }
        /// <summary>
        /// Returns a list of users with deactivated status, returns empty when there are no users
        /// </summary>
        /// <returns>list of users</returns>
        public List<User> GetDeactiveUsers()
        {
            return _context.Users.Where(status => status.Status == Status.Deactive).ToList();
        }
        /// <summary>
        ///Returns a list of users with deleted status, returns empty when there are no users
        /// </summary>
        /// <returns>list of users</returns>
        public List<User> GetDeletedUsers()
        {
            return _context.Users.Where(status => status.Status == Status.Deleted).ToList();
        }

        //returns status depending on the current status
        public Status ChanceCurrentStatus(User user)
        {
            return user.Status == Status.Active ? Status.Deactive : Status.Active;
        }

    }
}
