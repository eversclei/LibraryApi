using LibraryApi.Application.DTOs;

namespace LibraryApi.Application.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreReadDto>> GetAllAsync();
        Task<GenreReadDto?> GetByIdAsync(int id);
        Task<GenreReadDto> CreateAsync(GenreCreateDto dto);
        Task<bool> UpdateAsync(int id, GenreCreateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
