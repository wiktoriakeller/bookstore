using Bookstore.Core.Entities;
using Bookstore.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bookstore.DataAccessSQL.Repositories
{
    public class BooksRepository : BaseRepository<Book>, IBooksRepository
    {
        public BooksRepository(BookstoreDbContext dbContext) : base(dbContext)
        {
        }

        public override IEnumerable<Book> GetAll()
        {
            return _dbContext.Books.Include(b => b.Publisher);
        }

        public override IEnumerable<Book> GetWhere(Expression<Func<Book, bool>> predicate)
        {
            return _dbContext.Books
                .Include(b => b.Publisher)
                .Where(predicate);
        }
    }
}
