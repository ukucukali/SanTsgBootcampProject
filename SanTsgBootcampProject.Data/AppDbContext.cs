using Microsoft.EntityFrameworkCore;
using SanTsgBootcampProject.Domain;
using SanTsgBootcampProject.Domain.Users;

namespace SanTsgBootcampProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<ConfirmationResult> ReservatioConfirmationDetails { get; set; }
    }
}
