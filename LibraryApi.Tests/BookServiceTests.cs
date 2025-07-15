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
    public class BookServiceTests
    {
        private readonly Mock<IBookRepository> _repoMock;
        private readonly IMapper _mapper;
        private readonly BookService _service;

        public BookServiceTests()
        {
            _repoMock = new Mock<IBookRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Book, BookReadDto>().ReverseMap();
                cfg.CreateMap<Book, BookCreateDto>().ReverseMap();
            });
            _mapper = config.CreateMapper();

            _service = new BookService(_repoMock.Object, _mapper);
        }

        [Fact]
        public async Task Update_ShouldReturnTrue_WhenBookExists()
        {
            var book = new Book { Id = 1, Title = "Old Title" };
            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(book);

            var dto = new BookCreateDto { Title = "New Title", AuthorId = 1, GenreId = 1 };
            var result = await _service.UpdateAsync(1, dto);

            _repoMock.Verify(r => r.UpdateAsync(It.Is<Book>(b => b.Title == "New Title")), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public async Task Update_ShouldReturnFalse_WhenBookNotFound()
        {
            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Book)null);

            var dto = new BookCreateDto { Title = "Doesn't Matter", AuthorId = 1, GenreId = 1 };
            var result = await _service.UpdateAsync(1, dto);

            _repoMock.Verify(r => r.UpdateAsync(It.IsAny<Book>()), Times.Never);
            Assert.False(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnTrue_WhenBookExists()
        {
            var book = new Book { Id = 1, Title = "Test Book" };
            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(book);

            var result = await _service.DeleteAsync(1);

            _repoMock.Verify(r => r.DeleteAsync(book), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnFalse_WhenBookNotFound()
        {
            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Book)null);

            var result = await _service.DeleteAsync(1);

            _repoMock.Verify(r => r.DeleteAsync(It.IsAny<Book>()), Times.Never);
            Assert.False(result);
        }
    }
}
