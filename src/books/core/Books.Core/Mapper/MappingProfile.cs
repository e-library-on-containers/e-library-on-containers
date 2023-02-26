using AutoMapper;
using Books.Infrastructure.Models;
using Books.Business.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Books.Business.Responses;

namespace Books.Business.Mapper
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
