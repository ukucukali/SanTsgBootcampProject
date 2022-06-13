using SanTsgBootcampProject.Data.Repositories;
using SanTsgBootcampProject.Data.Repositories.Interfaces;

namespace SanTsgBootcampProject.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IUserRepository Users { get; private set; }
        public IReservatioConfirmationDetailsRepository ReservatioConfirmationDetails { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Users = new UserRepository(context);
            ReservatioConfirmationDetails = new ReservatioConfirmationDetailsRepository(context);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
