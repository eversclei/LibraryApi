using AutoMapper;
using LibraryApi.Application.DTOs;
using LibraryApi.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LibraryApi.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorReadDto>().ReverseMap();
            CreateMap<Author, AuthorCreateDto>().ReverseMap();

            CreateMap<Genre, GenreReadDto>().ReverseMap();
            CreateMap<Genre, GenreCreateDto>().ReverseMap();

            CreateMap<Book, BookReadDto>().ReverseMap();
            CreateMap<Book, BookCreateDto>().ReverseMap();
        }
    }
}
