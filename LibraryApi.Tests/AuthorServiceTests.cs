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
    public class AuthorServiceTests
    {
        private readonly Mock<IAuthorRepository> _repoMock;
        private readonly IMapper _mapper;
        private readonly AuthorService _service;

        public AuthorServiceTests()
        {
            _repoMock = new Mock<IAuthorRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Author, AuthorReadDto>().ReverseMap();
                cfg.CreateMap<Author, AuthorCreateDto>().ReverseMap();
            });
            _mapper = config.CreateMapper();

            _service = new AuthorService(_repoMock.Object, _mapper);
        }

        [Fact]
        public async Task Update_ShouldReturnTrue_WhenAuthorExists()
        {
            var author = new Author { Id = 1, Name = "Old Name" };
            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(author);

            var dto = new AuthorCreateDto { Name = "New Name" };
            var result = await _service.UpdateAsync(1, dto);

            _repoMock.Verify(r => r.UpdateAsync(It.Is<Author>(a => a.Name == "New Name")), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public async Task Update_ShouldReturnFalse_WhenAuthorNotFound()
        {
            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Author)null);

            var dto = new AuthorCreateDto { Name = "Doesn't Matter" };
            var result = await _service.UpdateAsync(1, dto);

            _repoMock.Verify(r => r.UpdateAsync(It.IsAny<Author>()), Times.Never);
            Assert.False(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnTrue_WhenAuthorExists()
        {
            var author = new Author { Id = 1, Name = "Someone" };
            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(author);

            var result = await _service.DeleteAsync(1);

            _repoMock.Verify(r => r.DeleteAsync(author), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnFalse_WhenAuthorNotFound()
        {
            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Author)null);

            var result = await _service.DeleteAsync(1);

            _repoMock.Verify(r => r.DeleteAsync(It.IsAny<Author>()), Times.Never);
            Assert.False(result);
        }
    }
}
