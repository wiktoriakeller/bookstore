﻿using Bookstore.Core.Common;
using Bookstore.Core.Dtos.Publishers;

namespace Bookstore.Core.Dtos.Books
{
    public class BookDto
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public string Author { get; init; }
        public string Genre { get; init; }
        public Reception Reception { get; init; }
        public DateTime PublishDate { get; init; }
        public Guid PublisherId { get; init; }
        public PublisherDto Publisher { get; init; }
    }
}
