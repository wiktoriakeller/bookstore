using AutoMapper;
using Bookstore.Core.Dtos.Publishers;
using Bookstore.Core.Entities;

namespace Bookstore.BusinessLogic.MappingProfiles
{
    public class PublishersMappingProfile : Profile
    {
        public PublishersMappingProfile()
        {
            CreateMap<AddPublisherDto, Publisher>()
                .ReverseMap();
            CreateMap<UpdatePublisherDto, Publisher>()
                .ReverseMap();
            CreateMap<PublisherDto, Publisher>()
                .ReverseMap();
        }
    }
}
