namespace LibraryApi.Application.DTOs
{
    public class BookCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
    }
}
