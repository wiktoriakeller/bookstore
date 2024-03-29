﻿namespace Bookstore.Core.Dtos.Books
{
    public class AddBookDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int Reception { get; set; }
        public DateTime PublishDate { get; set; }
        public Guid PublisherId { get; set; }
    }
}
