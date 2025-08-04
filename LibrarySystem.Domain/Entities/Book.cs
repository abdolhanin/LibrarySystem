namespace LibrarySystem.Domain.Entities
{
    public class Book
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Title { get; set; } = null!;
        public string? Author { get; set; } = null!;
        public string? ISBN { get; set; } = null!;
        public int AvailableCopies { get; set; }
        public bool IsAvailable => AvailableCopies > 0;
        public bool IsArchived { get; private set; } = false;

        public void Archive()
        {
            IsArchived = true;
        }

        public void Lend()
        {
            if (!IsAvailable)
                throw new InvalidOperationException("No copies available to lend.");

            AvailableCopies--;
        }
        public void UpdateDetails(string? title, string? author, string? isbn)
        {
            Title = title;
            Author = author;
            ISBN = isbn;
        }

        public void Return()
        {
            AvailableCopies++;
        }
    }
}
