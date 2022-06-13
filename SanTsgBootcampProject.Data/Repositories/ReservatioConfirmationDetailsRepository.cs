using SanTsgBootcampProject.Data.Repositories.Interfaces;
using SanTsgBootcampProject.Domain;

namespace SanTsgBootcampProject.Data.Repositories
{
    public class ReservatioConfirmationDetailsRepository : Repository<ConfirmationResult>, IReservatioConfirmationDetailsRepository
    {
        private readonly AppDbContext _context;

        public ReservatioConfirmationDetailsRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
