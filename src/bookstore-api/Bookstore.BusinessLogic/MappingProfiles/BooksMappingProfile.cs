using AutoMapper;
using Bookstore.Core.Dtos.Books;
using Bookstore.Core.Entities;

namespace Bookstore.BusinessLogic.MappingProfiles
{
    public class BooksMappingProfile : Profile
    {
        public BooksMappingProfile()
        {
            CreateMap<AddBookDto, Book>()
                .ReverseMap();
            CreateMap<UpdateBookDto, Book>()
                .ReverseMap();
            CreateMap<BookDto, Book>()
                .ReverseMap();
        }
    }
}
