using FluentAssertions;
using LibrarySystem.Application.Books.Commands;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Interfaces;
using Moq;

namespace LibrarySystem.UnitTests
{
    public class ArchiveBookCommandHandlerTests
    {
        private readonly Mock<IBookRepository> _mockRepository;
        private readonly ArchiveBookCommandHandler _handler;

        public ArchiveBookCommandHandlerTests()
        {
            _mockRepository = new Mock<IBookRepository>();
            _handler = new ArchiveBookCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_BookExists_ShouldArchiveBookAndSaveChanges()
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
            var command = new ArchiveBookCommand { BookId = bookId };
            var cancellationToken = CancellationToken.None;

            _mockRepository.Setup(r => r.GetByIdAsync(bookId))
                .ReturnsAsync(book);

            // Act
            await _handler.Handle(command, cancellationToken);

            // Assert
            book.IsArchived.Should().BeTrue();
            _mockRepository.Verify(r => r.SaveChangesAsync(cancellationToken), Times.Once);
        }

        [Fact]
        public async Task Handle_BookNotFound_ShouldThrowException()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var command = new ArchiveBookCommand { BookId = bookId };
            var cancellationToken = CancellationToken.None;

            _mockRepository.Setup(r => r.GetByIdAsync(bookId))
                .ReturnsAsync((Book)null);

            // Act 
            var exception = await Assert.ThrowsAsync<Exception>(
                () => _handler.Handle(command, cancellationToken));

            //Assert
            exception.Message.Should().Be("Book not found.");
            _mockRepository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
