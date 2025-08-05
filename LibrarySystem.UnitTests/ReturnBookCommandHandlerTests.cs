using FluentAssertions;
using LibrarySystem.Application.Books.Commands;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Interfaces;
using Moq;

namespace LibrarySystem.UnitTests
{
    public class ReturnBookCommandHandlerTests
    {
        private readonly Mock<IBookRepository> _mockRepository;
        private readonly ReturnBookCommandHandler _handler;

        public ReturnBookCommandHandlerTests()
        {
            _mockRepository = new Mock<IBookRepository>();
            _handler = new ReturnBookCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_BookExists_ShouldIncreaseAvailableCopiesAndSaveChanges()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var book = new Book
            {
                Id = bookId,
                Title = "Test Book",
                Author = "Test Author",
                ISBN = "1234567890",
                AvailableCopies = 2
            }; var command = new ReturnBookCommand { BookId = bookId };
            var cancellationToken = CancellationToken.None;

            _mockRepository.Setup(r => r.GetByIdAsync(bookId))
                .ReturnsAsync(book);

            // Act
            await _handler.Handle(command, cancellationToken);

            // Assert
            book.AvailableCopies.Should().Be(3);
            _mockRepository.Verify(r => r.SaveChangesAsync(cancellationToken), Times.Once);
        }

        [Fact]
        public async Task Handle_BookNotFound_ShouldThrowException()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var command = new ReturnBookCommand { BookId = bookId };
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
