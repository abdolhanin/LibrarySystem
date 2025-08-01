namespace LibrarySystem.Domain.Entities
{
    public class Book
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string ISBN { get; set; } = null!;
        public int AvailableCopies { get; set; }

        public bool IsAvailable => AvailableCopies > 0;

        public void Lend()
        {
            if (!IsAvailable)
                throw new InvalidOperationException("No copies available to lend.");

            AvailableCopies--;
        }

        public void Return()
        {
            AvailableCopies++;
        }
    }
}
