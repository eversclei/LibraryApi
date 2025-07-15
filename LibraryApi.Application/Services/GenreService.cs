using AutoMapper;
using LibraryApi.Application.DTOs;
using LibraryApi.Application.Interfaces;
using LibraryApi.Domain.Entities;
using LibraryApi.Domain.Interfaces;

namespace LibraryApi.Application.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreService(IGenreRepository repository, IMapper mapper)
        {
            _genreRepository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GenreReadDto>> GetAllAsync()
        {
            var genres = await _genreRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GenreReadDto>>(genres);
        }

        public async Task<GenreReadDto?> GetByIdAsync(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            return genre == null ? null : _mapper.Map<GenreReadDto>(genre);
        }

        public async Task<GenreReadDto> CreateAsync(GenreCreateDto dto)
        {
            var genre = _mapper.Map<Genre>(dto);
            var created = await _genreRepository.AddAsync(genre);
            return _mapper.Map<GenreReadDto>(created);
        }

        public async Task<bool> UpdateAsync(int id, GenreCreateDto dto)
        {
            var existing = await _genreRepository.GetByIdAsync(id);
            if (existing == null) return false;

            _mapper.Map(dto, existing);
            await _genreRepository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _genreRepository.GetByIdAsync(id);
            if (existing == null) return false;

            await _genreRepository.DeleteAsync(existing);
            return true;
        }

    }
}
