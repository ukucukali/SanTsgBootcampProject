using System;

namespace SanTsgBootcampProject.Data.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IUserRepository Users { get; }
        //save to database
        void Save();

    }
}
