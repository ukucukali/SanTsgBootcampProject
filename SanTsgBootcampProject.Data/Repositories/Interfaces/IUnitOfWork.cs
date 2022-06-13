using System;

namespace SanTsgBootcampProject.Data.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IUserRepository Users { get; }

        public IReservatioConfirmationDetailsRepository ReservatioConfirmationDetails { get; }
        //save to database
        void Save();

    }
}
