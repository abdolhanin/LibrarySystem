using FluentAssertions;
using LibrarySystem.Application.Books.Commands;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Interfaces;
using Moq;

namespace LibrarySystem.UnitTests
{
    public class CreateBookCommandHandlerTests
    {
        private readonly Mock<IBookRepository> _mockRepository;
        private readonly CreateBookCommandHandler _handler;

        public CreateBookCommandHandlerTests()
        {
            _mockRepository = new Mock<IBookRepository>();
            _handler = new CreateBookCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldCreateBookAndReturnId()
        {
            // Arrange
            var command = new CreateBookCommand(
                "Test Book",
                "Test Author",
                "1234567890",
                5
            );
            var cancellationToken = CancellationToken.None;

            _mockRepository.Setup(r => r.AddAsync(It.IsAny<Book>(), cancellationToken))
                          .Returns(Task.CompletedTask)
                          .Callback<Book, CancellationToken>((book, ct) =>
                          {
                              // Simulate setting the Id when added to repository
                              book.GetType().GetProperty("Id")?.SetValue(book, Guid.NewGuid());
                          });

            // Act
            var result = await _handler.Handle(command, cancellationToken);

            // Assert
            result.Should().NotBeEmpty();
            _mockRepository.Verify(r => r.AddAsync(It.Is<Book>(b =>
                b.Title == command.Title &&
                b.Author == command.Author &&
                b.ISBN == command.ISBN &&
                b.AvailableCopies == command.AvailableCopies), cancellationToken), Times.Once);
        }

        [Theory]
        [InlineData("", "Author", "ISBN", 1)]
        [InlineData("Title", "", "ISBN", 1)]
        [InlineData("Title", "Author", "", 1)]
        [InlineData("Title", "Author", "ISBN", 0)]
        public async Task Handle_InvalidCommand_ShouldCreateBookWithProvidedValues(
            string title, string author, string isbn, int availableCopies)
        {
            // Arrange
            var command = new CreateBookCommand(title, author, isbn, availableCopies);
            var cancellationToken = CancellationToken.None;

            // Act
            var result = await _handler.Handle(command, cancellationToken);

            // Assert
            result.Should().NotBeEmpty();
            _mockRepository.Verify(r => r.AddAsync(It.Is<Book>(b =>
                b.Title == title &&
                b.Author == author &&
                b.ISBN == isbn &&
                b.AvailableCopies == availableCopies), cancellationToken), Times.Once);
        }
    }
}
