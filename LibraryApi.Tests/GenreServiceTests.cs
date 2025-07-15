using AutoMapper;
using LibraryApi.Application.DTOs;
using LibraryApi.Application.Services;
using LibraryApi.Domain.Entities;
using LibraryApi.Domain.Interfaces;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace LibraryApi.Tests
{
    public class GenreServiceTests
    {
        private readonly Mock<IGenreRepository> _repoMock;
        private readonly IMapper _mapper;
        private readonly GenreService _service;

        public GenreServiceTests()
        {
            _repoMock = new Mock<IGenreRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Genre, GenreReadDto>().ReverseMap();
                cfg.CreateMap<Genre, GenreCreateDto>().ReverseMap();
            });
            _mapper = config.CreateMapper();

            _service = new GenreService(_repoMock.Object, _mapper);
        }

        [Fact]
        public async Task Update_ShouldReturnTrue_WhenGenreExists()
        {
            var genre = new Genre { Id = 1, Name = "Old Name" };
            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(genre);

            var dto = new GenreCreateDto { Name = "New Name" };
            var result = await _service.UpdateAsync(1, dto);

            _repoMock.Verify(r => r.UpdateAsync(It.Is<Genre>(g => g.Name == "New Name")), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public async Task Update_ShouldReturnFalse_WhenGenreNotFound()
        {
            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Genre)null);

            var dto = new GenreCreateDto { Name = "Doesn't Matter" };
            var result = await _service.UpdateAsync(1, dto);

            _repoMock.Verify(r => r.UpdateAsync(It.IsAny<Genre>()), Times.Never);
            Assert.False(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnTrue_WhenGenreExists()
        {
            var genre = new Genre { Id = 1, Name = "Fiction" };
            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(genre);

            var result = await _service.DeleteAsync(1);

            _repoMock.Verify(r => r.DeleteAsync(genre), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnFalse_WhenGenreNotFound()
        {
            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Genre)null);

            var result = await _service.DeleteAsync(1);

            _repoMock.Verify(r => r.DeleteAsync(It.IsAny<Genre>()), Times.Never);
            Assert.False(result);
        }
    }
}
