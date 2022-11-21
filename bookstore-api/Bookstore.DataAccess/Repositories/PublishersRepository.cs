using Bookstore.Core.Entities;
using Bookstore.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.DataAccessSQL.Repositories
{
    public class PublishersRepository : BaseRepository<Publisher>, IPublishersRepository
    {
        public PublishersRepository(BookstoreDbContext dbContext) : base(dbContext)
        {
        }

        public override async ValueTask<Publisher?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Publishers
                .Include(p => p.Books)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
