using LibraryApi.Application.DTOs;

namespace LibraryApi.Application.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorReadDto>> GetAllAsync();
        Task<AuthorReadDto?> GetByIdAsync(int id);
        Task<AuthorReadDto> CreateAsync(AuthorCreateDto dto);
        Task<bool> UpdateAsync(int id, AuthorCreateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
