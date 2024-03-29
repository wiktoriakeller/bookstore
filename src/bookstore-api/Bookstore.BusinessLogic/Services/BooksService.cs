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
            var allBooks = _booksRepository.GetAll().OrderBy(b => b.PublishDate);
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
                .GetWhere(b =>
                    b.Title.ToLower().Contains(titleSearch)
                    && b.PublishDate.Date >= booksFiltersDto.PublishDateStart.Date
                    && b.PublishDate.Date <= booksFiltersDto.PublishDateEnd.Date)
                .OrderBy(b => b.PublishDate);

            return _mapper.Map<IEnumerable<BookDto>>(filteredBooks);
        }

        public async Task<Guid> AddBook(AddBookDto addBookDto)
        {
            var allBooks = _booksRepository.GetAll();
            var bookTitle = addBookDto.Title.ToLower().Trim();

            if (allBooks.Any(b => b.Title.ToLower().Trim() == bookTitle))
            {
                throw new NotUniqueBookException($"Title {addBookDto.Title} is already taken by another book");
            }

            var selectedPublisher = await _publishersRepository.FirstOrDefaultAsync(p => p.Id == addBookDto.PublisherId);
            if (selectedPublisher is null)
            {
                throw new PublisherNotFoundException($"Specified publisher does not exist");
            }

            var book = _mapper.Map<Book>(addBookDto);
            book.Title = addBookDto.Title.Trim();
            book.Publisher = selectedPublisher;

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
            var bookTitle = updateBookDto.Title.ToLower().Trim();

            if (allBooks.Any(b => b.Title.ToLower().Trim() == bookTitle && b.Id != updateBookDto.Id))
            {
                throw new NotUniqueBookException($"Title {updateBookDto.Title} is already taken by another book");
            }

            var bookPublisher = await _publishersRepository.FirstOrDefaultAsync(p => p.Id == updateBookDto.PublisherId);
            if (bookPublisher is null)
            {
                throw new PublisherNotFoundException($"Specified publisher does not exist");
            }

            var book = allBooks.First(b => b.Id == updateBookDto.Id);
            book.Title = updateBookDto.Title.Trim();
            book.Reception = updateBookDto.Reception;
            book.Genre = updateBookDto.Genre;
            book.Author = updateBookDto.Author;
            book.PublishDate = updateBookDto.PublishDate;
            book.PublisherId = updateBookDto.PublisherId;
            book.Publisher = bookPublisher;

            await _booksRepository.UpdateAsync(book);
        }
    }
}
