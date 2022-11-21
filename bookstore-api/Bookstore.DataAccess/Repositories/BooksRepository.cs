using Bookstore.Core.Entities;
using Bookstore.Interfaces.Repositories;

namespace Bookstore.DataAccessSQL.Repositories
{
    public class BooksRepository : BaseRepository<Book>, IBooksRepository
    {
        public BooksRepository(BookstoreDbContext dbContext) : base(dbContext)
        {
        }
    }
}
