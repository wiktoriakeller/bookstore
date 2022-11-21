﻿using AutoMapper;
using Bookstore.BusinessLogic.Exceptions;
using Bookstore.Core.Dtos.Books;
using Bookstore.Core.Entities;
using Bookstore.Interfaces.Repositories;
using Bookstore.Interfaces.Services;

namespace Bookstore.BusinessLogic.Services
{
    public class BooksService : IBooksService
    {
        private readonly IBooksRepository _booksRepository;
        private readonly IPublishersRepository _publishersRepository;
        private readonly IMapper _mapper;

        public BooksService(
            IBooksRepository booksRepository,
            IPublishersRepository publishersRepository,
            IMapper mapper)
        {
            _booksRepository = booksRepository;
            _mapper = mapper;
            _publishersRepository = publishersRepository;
        }

        public IEnumerable<BookDto> GetAllBooks()
        {
            var allBooks = _booksRepository.GetAll();
            return _mapper.Map<IEnumerable<BookDto>>(allBooks);
        }

        public IEnumerable<BookDto> FilterBooks(BooksFiltersDto booksFiltersDto)
        {
            var titleSearch = string.Empty;
            if (!string.IsNullOrWhiteSpace(booksFiltersDto.TitleFilter))
            {
                titleSearch = booksFiltersDto.TitleFilter.ToLower();
            }

            var filteredBooks = _booksRepository
                .GetWhere(b => b.Title.Contains(titleSearch) && booksFiltersDto.ReceptionFilters.Contains(b.Reception));
            return _mapper.Map<IEnumerable<BookDto>>(filteredBooks);
        }

        public async Task<Guid> AddBook(AddBookDto addBookDto)
        {
            var allBooks = _booksRepository.GetAll();
            var bookTitle = addBookDto.Title.ToLower();

            if (allBooks.Any(b => b.Title.ToLower() == bookTitle))
            {
                throw new NotUniqueBookException($"Title {addBookDto.Title} is already taken by another book");
            }

            var allPublishers = _publishersRepository.GetAll();
            if (!allPublishers.Any(p => p.Id == addBookDto.PublisherId))
            {
                throw new PublisherNotFoundException($"Specified publisher does not exist");
            }

            var book = _mapper.Map<Book>(addBookDto);
            await _booksRepository.AddAsync(book);
            return book.Id;
        }

        public async Task DeleteBook(DeleteBookDto deleteBookDto)
        {
            var book = await _booksRepository.GetByIdAsync(deleteBookDto.Id);
            if (book is null)
            {
                throw new BookNotFoundException("Specified book does not exist");
            }

            await _booksRepository.DeleteAsync(book);
        }

        public async Task UpdateBook(UpdateBookDto updateBookDto)
        {
            var allBooks = _booksRepository.GetAll();
            var bookTitle = updateBookDto.Title.ToLower();

            if (allBooks.Any(b => b.Title.ToLower() == bookTitle && b.Id != updateBookDto.Id))
            {
                throw new NotUniqueBookException($"Title {updateBookDto.Title} is already taken by another book");
            }

            var allPublishers = _publishersRepository.GetAll();
            if (!allPublishers.Any(p => p.Id == updateBookDto.PublisherId))
            {
                throw new PublisherNotFoundException($"Specified publisher does not exist");
            }

            var book = _mapper.Map<Book>(updateBookDto);
            await _booksRepository.UpdateAsync(book);
        }
    }
}