using Menherachan.Domain.Database;

namespace Menherachan.Infrastructure.Persistence.Repositories
{
    public class BaseRepository
    {
        private ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}