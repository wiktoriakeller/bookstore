using AutoFixture.Xunit2;
using AutoMapper;
using Bookstore.BusinessLogic.Exceptions;
using Bookstore.BusinessLogic.Services;
using Bookstore.Core.Dtos.Books;
using Bookstore.Core.Dtos.Publishers;
using Bookstore.Core.Entities;
using Bookstore.Interfaces.Repositories;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Bookstore.UnitTests.BusinessLogic.Services
{
    [ExcludeFromCodeCoverage]
    public class PublishersServiceTests
    {
        [Theory]
        [AutoData]
        public async Task AddPublisher_GivenAddPublisherDto_WhenPublisherNameIsNotUnique_ShouldThrowNotUniquePublisherException(AddPublisherDto addPublisherDto)
        {
            //Arrange
            var fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var publisher = fixture.Create<Publisher>();

            var booksRepositoryMock = new Mock<IBooksRepository>();
            var publishersRepositoryMock = new Mock<IPublishersRepository>();
            var mapper = new Mock<IMapper>();

            publishersRepositoryMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<Publisher, bool>>>()))
                .Returns(Task.FromResult(publisher));

            var sut = new PublishersService(publishersRepositoryMock.Object, booksRepositoryMock.Object, mapper.Object);

            //Act
            var act = async () => await sut.AddPublisher(addPublisherDto);

            //Assert
            await act.Should().ThrowAsync<NotUniquePublisherException>();
        }

        [Theory]
        [AutoData]
        public async Task DeletePublisher_GivenDeletePublisherDto_WhenPublisherIdIsNotFound_ShouldThrowPublisherNotFoundException(DeletePublisherDto deletePublisherDto)
        {
            //Arrange
            var booksRepositoryMock = new Mock<IBooksRepository>();
            var publishersRepositoryMock = new Mock<IPublishersRepository>();
            var mapper = new Mock<IMapper>();

            publishersRepositoryMock.Setup(x => x.GetByIdAsync(deletePublisherDto.Id))
                .Returns<DeleteBookDto>(null);

            var sut = new PublishersService(publishersRepositoryMock.Object, booksRepositoryMock.Object, mapper.Object);

            //Act
            var act = async () => await sut.DeletePublisher(deletePublisherDto);

            //Assert
            await act.Should().ThrowAsync<PublisherNotFoundException>();
        }
    }
}
