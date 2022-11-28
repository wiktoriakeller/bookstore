using AutoFixture.Xunit2;
using AutoMapper;
using Bookstore.BusinessLogic.Exceptions;
using Bookstore.BusinessLogic.Services;
using Bookstore.Core.Common;
using Bookstore.Core.Dtos.Books;
using Bookstore.Core.Entities;
using Bookstore.Interfaces.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace Bookstore.UnitTests.BusinessLogic.Services
{
    [ExcludeFromCodeCoverage]
    public class BooksServiceTests
    {
        [Theory]
        [AutoData]
        public async Task AddBook_GivenAddBookDto_WhenBookTitleIsUniqueAndPublisherIsFound_ShouldSuccessfulyReturnNewBookId(AddBookDto addBookDto)
        {
            //Arrange
            var fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var publisher = fixture.Create<Publisher>();
            var book = fixture.Create<Book>();

            var booksRepositoryMock = new Mock<IBooksRepository>();
            var publishersRepositoryMock = new Mock<IPublishersRepository>();
            var mapper = new Mock<IMapper>();

            booksRepositoryMock.Setup(b => b.GetAll())
                .Returns(Books);

            publishersRepositoryMock.Setup(x => x.FirstOrDefaultAsync(p => p.Id == addBookDto.PublisherId))
                .Returns(Task.FromResult(publisher));

            mapper.Setup(x => x.Map<Book>(addBookDto))
                .Returns(book);

            var sut = new BooksService(booksRepositoryMock.Object, publishersRepositoryMock.Object, mapper.Object);

            //Act
            var id = await sut.AddBook(addBookDto);

            //Assert
            id.Should().NotBeEmpty();
        }

        [Theory]
        [InlineAutoData("Adventure book")]
        [InlineAutoData("   Horror book")]
        [InlineAutoData("Adventure book  ")]
        public async Task AddBook_GivenAddBookDto_WhenBookTitleIsNotUnique_ShouldThrowNotUniqueBookException(string title, AddBookDto addBookDto)
        {
            //Arrange
            addBookDto.Title = title;
            var booksRepositoryMock = new Mock<IBooksRepository>();
            var publishersRepositoryMock = new Mock<IPublishersRepository>();
            var mapper = new Mock<IMapper>();

            booksRepositoryMock.Setup(x => x.GetAll())
                .Returns(Books);

            var sut = new BooksService(booksRepositoryMock.Object, publishersRepositoryMock.Object, mapper.Object);

            //Act
            var act = async () => await sut.AddBook(addBookDto);

            //Assert
            await act.Should().ThrowAsync<NotUniqueBookException>();
        }

        [Theory]
        [AutoData]
        public async Task DeleteBook_GivenDeleteBookDto_WhenBookIdIsNotFound_ShouldThrowBookNotFoundException(DeleteBookDto deleteBookDto)
        {
            //Arrange
            var booksRepositoryMock = new Mock<IBooksRepository>();
            var publishersRepositoryMock = new Mock<IPublishersRepository>();
            var mapper = new Mock<IMapper>();

            booksRepositoryMock.Setup(x => x.GetByIdAsync(deleteBookDto.Id))
                .Returns<DeleteBookDto>(null);

            var sut = new BooksService(booksRepositoryMock.Object, publishersRepositoryMock.Object, mapper.Object);

            //Act
            var act = async () => await sut.DeleteBook(deleteBookDto);

            //Assert
            await act.Should().ThrowAsync<BookNotFoundException>();
        }

        private static List<Book> Books = new()
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
    }
}
