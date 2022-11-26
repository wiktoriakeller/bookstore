using Bookstore.Core.Common;
using Bookstore.Core.Entities;
using Bookstore.Interfaces.Repositories;
using System.Linq.Expressions;

namespace Bookstore.DataAccessMock.Repositories
{
    public class PublishersRepositoryMock : IPublishersRepository
    {
        private static List<Publisher> _publishers = new()
        {
            new()
            {
                Id = new Guid("6e4a578d-1f82-4f04-b10f-688014c62378"),
                Name = "Pub1",
                Books = new List<Book>
                {
                    new()
                    {
                        Id = new Guid("6e4a578d-1f82-4f04-b10f-688014c62378"),
                        Author = "David",
                        Genre = "Sci-fi",
                        Title = "Horror book",
                        Reception = Reception.Good,
                        PublishDate = new DateTime(2022, 10, 1),
                        PublisherId = new Guid("6e4a578d-1f82-4f04-b10f-688014c62378")
                    }
                }
            },
            new()
            {
                Id = new Guid("70ca73f2-64a7-4902-88c2-309d2d781fab"),
                Name = "Pub2",
                Books = new List<Book>
                {
                    new()
                    {
                        Id = new Guid("70ca73f2-64a7-4902-88c2-309d2d781fab"),
                        Author = "Anne",
                        Genre = "Adventure",
                        Title = "Adventure book",
                        Reception = Reception.Excellent,
                        PublishDate = new DateTime(2022, 11, 2),
                        PublisherId = new Guid("70ca73f2-64a7-4902-88c2-309d2d781fab")
                    }
                }
            }
        };

        private readonly IBooksRepository _booksRepository;

        public PublishersRepositoryMock(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public Task<Publisher> AddAsync(Publisher entity)
        {
            _publishers.Add(entity);
            return Task.FromResult(entity);
        }

        public async Task<Publisher> UpdateAsync(Publisher entity)
        {
            var books = _booksRepository.GetAll().ToList();
            for (int i = 0; i > _publishers.Count(); i++)
            {
                if (_publishers[i].Id == entity.Id)
                {
                    _publishers[i] = entity;
                    break;
                }
            }

            for (int i = 0; i < books.Count(); i++)
            {
                if (books[i].PublisherId == entity.Id)
                {
                    var newBook = books[i];
                    newBook.Publisher = entity;
                    await _booksRepository.UpdateAsync(newBook);
                }
            }

            return entity;
        }

        public Task DeleteAsync(Publisher entity)
        {
            var publisherToDelete = _publishers.First(b => b.Id == entity.Id);
            _publishers.Remove(publisherToDelete);
            return Task.CompletedTask;
        }

        public Task<Publisher?> FirstOrDefaultAsync(Expression<Func<Publisher, bool>> predicate)
        {
            var func = predicate.Compile();
            return Task.FromResult(_publishers.FirstOrDefault(func));
        }

        public IEnumerable<Publisher> GetAll()
        {
            return _publishers;
        }

        public ValueTask<Publisher?> GetByIdAsync(Guid id)
        {
            return ValueTask.FromResult(_publishers.FirstOrDefault(p => p.Id == id));
        }

        public IEnumerable<Publisher> GetWhere(Expression<Func<Publisher, bool>> predicate)
        {
            var func = predicate.Compile();
            return _publishers.Where(func);
        }

        public Task<Publisher?> SignleOrDefaultAsync(Expression<Func<Publisher, bool>> predicate)
        {
            var func = predicate.Compile();
            return Task.FromResult(_publishers.SingleOrDefault(func));
        }
    }
}
