using FluentAssertions;
using LibrarySystem.Application.Books.Queries;
using LibrarySystem.Application.DTOs;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Interfaces;
using Moq;

namespace LibrarySystem.UnitTests
{
    public class GetAvailableBooksQueryHandlerTests
    {
        private readonly Mock<IBookRepository> _mockRepository;
        private readonly GetAvailableBooksQueryHandler _handler;

        public GetAvailableBooksQueryHandlerTests()
        {
            _mockRepository = new Mock<IBookRepository>();
            _handler = new GetAvailableBooksQueryHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_BooksExist_ShouldReturnAvailableBookDtos()
        {
            // Arrange
            var books = new List<Book>
            {
                CreateBook(Guid.NewGuid(), "Book 1", "Author 1", "ISBN1", 3),
                CreateBook(Guid.NewGuid(), "Book 2", "Author 2", "ISBN2", 1),
                CreateBook(Guid.NewGuid(), "Book 3", "Author 3", "ISBN3", 5)
            };

            var query = new GetAvailableBooksQuery();
            var cancellationToken = CancellationToken.None;

            _mockRepository.Setup(r => r.GetAvailableBooksAsync())
                          .ReturnsAsync(books);

            // Act
            var result = await _handler.Handle(query, cancellationToken);

            // Assert
            result.Should().HaveCount(3);
            result.Should().BeEquivalentTo(books.Select(b => new AvailableBookDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                ISBN = b.ISBN,
                AvailableCopies = b.AvailableCopies
            }));
        }

        [Fact]
        public async Task Handle_NoBooksAvailable_ShouldReturnEmptyList()
        {
            // Arrange
            var query = new GetAvailableBooksQuery();
            var cancellationToken = CancellationToken.None;

            _mockRepository.Setup(r => r.GetAvailableBooksAsync())
                          .ReturnsAsync(new List<Book>());

            // Act
            var result = await _handler.Handle(query, cancellationToken);

            // Assert
            result.Should().BeEmpty();
        }

        private static Book CreateBook(Guid id, string title, string author, string isbn, int availableCopies)
        {
            var book = new Book
            {
                Title = title,
                Author = author,
                ISBN = isbn,
                AvailableCopies = availableCopies
            };

            var idProperty = typeof(Book).GetProperty("Id");
            idProperty?.SetValue(book, id);

            return book;
        }
    }
}
