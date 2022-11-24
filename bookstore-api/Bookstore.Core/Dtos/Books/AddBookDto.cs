﻿using Bookstore.Core.Common;

namespace Bookstore.Core.Dtos.Books
{
    public class AddBookDto
    {
        public string Title { get; init; }
        public string Author { get; init; }
        public string Genre { get; init; }
        public Reception Reception { get; init; }
        public DateTime PublishDate { get; init; }
        public Guid PublisherId { get; init; }
    }
}
