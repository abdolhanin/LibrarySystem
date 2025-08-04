namespace LibrarySystem.Application.DTOs
{
    public class AvailableBookDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; } = string.Empty;
        public string? Author { get; set; } = string.Empty;
        public string? ISBN { get; set; } = string.Empty;
        public int AvailableCopies { get; set; }
    }
}
