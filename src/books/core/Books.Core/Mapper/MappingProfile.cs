using AutoMapper;
using Books.Infrastructure.Models;
using Books.Core.Responses;
using Books.Core.Create;
using Books.Core.Update;
using Books.Core.Delete;

namespace Books.Core.Mapper
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookRead, BookResponse>().ReverseMap();
            CreateMap<BookRead, CreateBookCommand>().ReverseMap();
            CreateMap<BookRead, UpdateBookCommand>().ReverseMap();
            CreateMap<Book, CreateBookCommand>().ReverseMap();
            CreateMap<Book, UpdateBookCommand>().ReverseMap();
            CreateMap<BookInstance, BookInstanceResponse>().ReverseMap();
            CreateMap<BookInstance, CreateBookInstanceCommand>().ReverseMap();
            CreateMap<BookInstance, DeleteBookInstanceCommand>().ReverseMap();
        }
    }
}
