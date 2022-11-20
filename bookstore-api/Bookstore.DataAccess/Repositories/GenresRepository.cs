using Bookstore.Core.Entities;
using Bookstore.Interfaces.Repositories;

namespace Bookstore.DataAccess.Repositories
{
    public class GenresRepository : BaseRepository<Genre>, IGenresRepository
    {
        public GenresRepository(BookstoreDbContext dbContext) : base(dbContext)
        {
        }
    }
}
