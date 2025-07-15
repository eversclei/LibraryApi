using AutoMapper;
using LibraryApi.Application.DTOs;
using LibraryApi.Application.Interfaces;
using LibraryApi.Domain.Entities;
using LibraryApi.Domain.Interfaces;

namespace LibraryApi.Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository repository, IMapper mapper)
        {
            _authorRepository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AuthorReadDto>> GetAllAsync()
        {
            var authors = await _authorRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AuthorReadDto>>(authors);
        }

        public async Task<AuthorReadDto?> GetByIdAsync(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            return author == null ? null : _mapper.Map<AuthorReadDto>(author);
        }

        public async Task<AuthorReadDto> CreateAsync(AuthorCreateDto dto)
        {
            var author = _mapper.Map<Author>(dto);
            var created = await _authorRepository.AddAsync(author);
            return _mapper.Map<AuthorReadDto>(created);
        }
        public async Task<bool> UpdateAsync(int id, AuthorCreateDto dto)
        {
            var existing = await _authorRepository.GetByIdAsync(id);
            if (existing == null) return false;

            _mapper.Map(dto, existing);
            await _authorRepository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _authorRepository.GetByIdAsync(id);
            if (existing == null) return false;

            await _authorRepository.DeleteAsync(existing);
            return true;
        }
    }
}
