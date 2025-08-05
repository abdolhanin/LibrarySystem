using FluentAssertions;
using LibrarySystem.Application.Books.Commands;
using LibrarySystem.Application.Common;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Interfaces;
using Moq;

namespace LibrarySystem.UnitTests
{
    public class LendBookCommandHandlerTests
    {
        private readonly Mock<IBookRepository> _mockRepository;
        private readonly LendBookCommandHandler _handler;

        public LendBookCommandHandlerTests()
        {
            _mockRepository = new Mock<IBookRepository>();
            _handler = new LendBookCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_BookExists_ShouldLendBookAndSaveChanges()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var book = new Book
            {
                Id = bookId,
                Title = "Test Book",
                Author = "Test Author",
                ISBN = "1234567890",
                AvailableCopies = 3
            };
            var command = new LendBookCommand(bookId);
            var cancellationToken = CancellationToken.None;

            _mockRepository.Setup(r => r.GetByIdAsync(bookId))
                .ReturnsAsync(book);

            // Act
            await _handler.Handle(command, cancellationToken);

            // Assert
            book.AvailableCopies.Should().Be(2);
            _mockRepository.Verify(r => r.SaveChangesAsync(cancellationToken), Times.Once);
        }

        [Fact]
        public async Task Handle_BookNotFound_ShouldThrowNotFoundException()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var command = new LendBookCommand(bookId);
            var cancellationToken = CancellationToken.None;

            _mockRepository.Setup(r => r.GetByIdAsync(bookId))
                          .ReturnsAsync((Book)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(
                () => _handler.Handle(command, cancellationToken));

            exception.Message.Should().Be("Book not found.");
            _mockRepository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}


