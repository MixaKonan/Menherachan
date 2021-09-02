using Menherachan.Domain.Database;

namespace Menherachan.Infrastructure.Persistence.Repositories
{
    public class BaseRepository
    {
        protected ApplicationDbContext Context;

        public BaseRepository(ApplicationDbContext context)
        {
            Context = context;
        }
    }
}