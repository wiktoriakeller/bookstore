﻿using Bookstore.UI.Common;

namespace Bookstore.Core.Dtos.Books
{
    public class BooksFiltersDto
    {
        public string? TitleFilter { get; init; }
        public Reception[] ReceptionFilters { get; init; } = Array.Empty<Reception>();
    }
}
