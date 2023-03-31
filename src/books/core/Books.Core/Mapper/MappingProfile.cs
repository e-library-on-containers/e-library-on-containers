using AutoMapper;
using Books.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Books.Core.Commands;
using Books.Core.Responses;

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
