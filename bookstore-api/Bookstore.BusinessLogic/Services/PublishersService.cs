using AutoMapper;
using Bookstore.BusinessLogic.Exceptions;
using Bookstore.Core.Dtos.Publishers;
using Bookstore.Core.Entities;
using Bookstore.Interfaces.Repositories;
using Bookstore.Interfaces.Services;

namespace Bookstore.BusinessLogic.Services
{
    public class PublishersService : IPublishersService
    {
        private readonly IPublishersRepository _publishersRepository;
        private readonly IBooksRepository _booksRepository;
        private readonly IMapper _mapper;

        public PublishersService(
            IPublishersRepository publishersRepository,
            IBooksRepository booksRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _publishersRepository = publishersRepository;
            _booksRepository = booksRepository;
        }

        public IEnumerable<PublisherDto> GetAllPublishers()
        {
            var allPublishers = _publishersRepository.GetAll();
            return _mapper.Map<IEnumerable<PublisherDto>>(allPublishers);
        }

        public IEnumerable<PublisherDto> FilterPublishers(PublishersFiltersDto publishersFiltersDto)
        {
            var search = string.Empty;
            if (!string.IsNullOrWhiteSpace(publishersFiltersDto.NameFilter))
            {
                search = publishersFiltersDto.NameFilter.ToLower();
            }

            var filteredPublishers = _publishersRepository.GetWhere(p => p.Name.ToLower().StartsWith(search));
            return _mapper.Map<IEnumerable<PublisherDto>>(filteredPublishers);
        }

        public async Task<Guid> AddPublisher(AddPublisherDto addPublisherDto)
        {
            var allPublishers = _publishersRepository.GetAll();
            var publisherName = addPublisherDto.Name.ToLower();

            if (allPublishers.Any(p => p.Name.ToLower() == publisherName))
            {
                throw new NotUniquePublisherException($"Name {addPublisherDto.Name} is already taken by another publisher");
            }

            var publisher = _mapper.Map<Publisher>(addPublisherDto);
            await _publishersRepository.AddAsync(publisher);
            return publisher.Id;
        }

        public async Task DeletePublisher(DeletePublisherDto deletePublisherDto)
        {
            var publisher = await _publishersRepository.GetByIdAsync(deletePublisherDto.Id);
            if (publisher is null)
            {
                throw new PublisherNotFoundException("Specified publisher does not exist");
            }

            if (_booksRepository.GetAll().FirstOrDefault(b => b.Publisher.Id == deletePublisherDto.Id) is not null)
            {
                throw new PublisherHasBooksException("You can't delete publisher that has books");
            }

            await _publishersRepository.DeleteAsync(publisher);
        }

        public async Task UpdatePublisher(UpdatePublisherDto updatePublisherDto)
        {
            var publisher = await _publishersRepository.GetByIdAsync(updatePublisherDto.Id);

            if (publisher is null)
            {
                throw new PublisherNotFoundException("Specified publisher does not exist");
            }

            var allPublishers = _publishersRepository.GetAll();
            if (allPublishers.Any(p => p.Name == updatePublisherDto.Name && p.Id != updatePublisherDto.Id))
            {
                throw new NotUniquePublisherException($"Name {updatePublisherDto.Name} is already taken by another publisher");
            }

            publisher.Name = updatePublisherDto.Name;
            await _publishersRepository.UpdateAsync(publisher);
        }
    }
}
