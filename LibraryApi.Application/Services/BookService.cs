using AutoMapper;
using LibraryApi.Application.DTOs;
using LibraryApi.Application.Interfaces;
using LibraryApi.Domain.Entities;
using LibraryApi.Domain.Interfaces;

namespace LibraryApi.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository repository, IMapper mapper)
        {
            _bookRepository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookReadDto>> GetAllAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookReadDto>>(books);
        }

        public async Task<BookReadDto?> GetByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            return book == null ? null : _mapper.Map<BookReadDto>(book);
        }

        public async Task<BookReadDto> CreateAsync(BookCreateDto dto)
        {
            var book = _mapper.Map<Book>(dto);
            var created = await _bookRepository.AddAsync(book);
            return _mapper.Map<BookReadDto>(created);
        }
        public async Task<bool> UpdateAsync(int id, BookCreateDto dto)
        {
            var existing = await _bookRepository.GetByIdAsync(id);
            if (existing == null) return false;

            _mapper.Map(dto, existing);
            await _bookRepository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _bookRepository.GetByIdAsync(id);
            if (existing == null) return false;

            await _bookRepository.DeleteAsync(existing);
            return true;
        }

    }
}
