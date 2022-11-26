using Bookstore.Core.Common;
using Bookstore.Core.Entities;
using Bookstore.Interfaces.Repositories;
using System.Linq.Expressions;

namespace Bookstore.DataAccessMock.Repositories
{
    public class BooksRepositoryMock : IBooksRepository
    {
        private static List<Book> _books = new()
        {
            new()
            {
                Id = new Guid("6e4a578d-1f82-4f04-b10f-688014c62378"),
                Author = "David",
                Genre = "Sci-fi",
                Title = "Horror book",
                Reception = Reception.Good,
                PublishDate = new DateTime(2022, 10, 1),
                PublisherId = new Guid("6e4a578d-1f82-4f04-b10f-688014c62378"),
                Publisher = new Publisher
                {
                    Id = new Guid("6e4a578d-1f82-4f04-b10f-688014c62378"),
                    Name = "Pub1"
                }
            },
            new()
            {
                Id = new Guid("70ca73f2-64a7-4902-88c2-309d2d781fab"),
                Author = "Anne",
                Genre = "Adventure",
                Title = "Adventure book",
                Reception = Reception.Excellent,
                PublishDate = new DateTime(2022, 11, 2),
                PublisherId = new Guid("70ca73f2-64a7-4902-88c2-309d2d781fab"),
                Publisher = new Publisher
                {
                    Id = new Guid("70ca73f2-64a7-4902-88c2-309d2d781fab"),
                    Name = "Pub2"
                }
            }
        };

        public Task<Book> AddAsync(Book entity)
        {
            _books.Add(entity);
            return Task.FromResult(entity);
        }

        public Task<Book> UpdateAsync(Book entity)
        {
            for (int i = 0; i < _books.Count(); i++)
            {
                if (_books[i].Id == entity.Id)
                {
                    _books[i] = entity;
                    break;
                }
            }

            return Task.FromResult(entity);
        }

        public Task DeleteAsync(Book entity)
        {
            var bookToDelete = _books.First(b => b.Id == entity.Id);
            _books.Remove(bookToDelete);
            return Task.CompletedTask;
        }

        public Task<Book?> FirstOrDefaultAsync(Expression<Func<Book, bool>> predicate)
        {
            var func = predicate.Compile();
            return Task.FromResult(_books.FirstOrDefault(func));
        }

        public IEnumerable<Book> GetAll()
        {
            return _books;
        }

        public ValueTask<Book?> GetByIdAsync(Guid id)
        {
            return ValueTask.FromResult(_books.FirstOrDefault(b => b.Id == id));
        }

        public IEnumerable<Book> GetWhere(Expression<Func<Book, bool>> predicate)
        {
            var func = predicate.Compile();
            return _books.Where(func);
        }

        public Task<Book?> SignleOrDefaultAsync(Expression<Func<Book, bool>> predicate)
        {
            var func = predicate.Compile();
            return Task.FromResult(_books.SingleOrDefault(func));
        }
    }
}
