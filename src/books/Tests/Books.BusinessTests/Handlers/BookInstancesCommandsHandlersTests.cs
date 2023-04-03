using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Books.Infrastructure.Contracts;
using Books.Infrastructure.Models;
using Books.Business.Commands;
using Books.Business.Handlers;

namespace Books.BusinessTests.Handlers
{
    public class BookInstancesCommandsHandlersTests
    {
        private readonly Mock<IBookRepository<BookRead>> _bookReadRepositoryMock;
        private readonly Mock<IBookInstancesRepository<BookInstance>> _bookInstanceRepositoryMock;

        public BookInstancesCommandsHandlersTests()
        {
            _bookReadRepositoryMock = new Mock<IBookRepository<BookRead>>();
            _bookInstanceRepositoryMock = new Mock<IBookInstancesRepository<BookInstance>>();
        }

        [Fact]
        public async void CreateBookInstanceHandlerTest()
        {
            // Arrange
            CreateBookInstanceCommand request = new CreateBookInstanceCommand("11111111111", true);
            _bookInstanceRepositoryMock.Setup(x => x.Create(It.IsAny<BookInstance>()));
            CreateBookInstanceHandler handler = new CreateBookInstanceHandler(_bookInstanceRepositoryMock.Object);

            // Act
            await handler.Handle(request, default);

            // Assert
            _bookInstanceRepositoryMock.VerifyAll();
        }

        [Fact()]
        public async void DeleteBookHandlerTest()
        {
            // Arrange
            DeleteBookInstanceCommand request = new DeleteBookInstanceCommand(1);
            Book book = new Book(1, "11111111111", "author", "title", "description", "coverImg");
            BookInstance bookInstance = new BookInstance();
            _bookInstanceRepositoryMock.Setup(x => x.Delete(It.IsAny<int>()));
            _bookInstanceRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(bookInstance);
            DeleteBookInstanceHandler handler = new DeleteBookInstanceHandler(_bookInstanceRepositoryMock.Object);

            // Act
            await handler.Handle(request, default);

            // Assert
            _bookInstanceRepositoryMock.VerifyAll();
        }
    }
}
