using Bookstore.Core.Entities;
using Bookstore.Interfaces.Repositories;

namespace Bookstore.DataAccess.Repositories
{
    public class PublishersRepository : BaseRepository<Publisher>, IPublishersRepository
    {
        public PublishersRepository(BookstoreDbContext dbContext) : base(dbContext)
        {
        }
    }
}
