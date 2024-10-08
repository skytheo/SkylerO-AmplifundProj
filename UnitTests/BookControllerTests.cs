using SkylerO_AmplifundProj.Repostitories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.EntityFrameworkCore;
using SkylerO_AmplifundProj.Models;
using System.Reflection.Metadata;

namespace SkylerO_AmplifundProj.UnitTests
{
    [TestClass]
    public class BookControllerTests
    {
        [TestMethod]
        public async Task CreateBook_Success()
        {
            var mockSet = new Mock<DbSet<Book>>();

            var mockContext = new Mock<BookContext>();
            mockContext.Setup(m => m.Books).Returns(mockSet.Object);

            var service = new BookRepository(mockContext.Object);
            await service.Add(new Book { Title = "test" });

            mockSet.Verify(m => m.Add(It.IsAny<Book>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public async Task GetAll_Success()
        {
            var data = new List<Book>
            {
                new Book { Title = "BBB" },
                new Book { Title = "ZZZ" },
                new Book { Title = "AAA" },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Book>>();
            mockSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<BookContext>();
            mockContext.Setup(c => c.Books).Returns(mockSet.Object);

            var service = new BookRepository(mockContext.Object);
            var books = service.GetAll().Result.ToArray();

            Assert.AreEqual(3, books.Count());
            Assert.AreEqual("AAA", books[0].Title);
            Assert.AreEqual("BBB", books[1].Title);
            Assert.AreEqual("ZZZ", books[2].Title);
        }

        [TestMethod]
        public async Task GetOne_Success()
        {
            var data = new List<Book>
            {
                new Book { Title = "BBB", Id=1 },
                new Book { Title = "ZZZ", Id = 2 },
                new Book { Title = "AAA", Id = 3 },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Book>>();
            mockSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<BookContext>();
            mockContext.Setup(c => c.Books).Returns(mockSet.Object);

            var service = new BookRepository(mockContext.Object);
            var book = service.GetOne(1).Result;

            Assert.AreEqual("AAA", book.Title);
        }

        [TestMethod]
        public async Task GetOne_Failure()
        {
            var data = new List<Book>
            {
                new Book { Title = "BBB", Id=1 },
                new Book { Title = "ZZZ", Id = 2 },
                new Book { Title = "AAA", Id = 3 },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Book>>();
            mockSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<BookContext>();
            mockContext.Setup(c => c.Books).Returns(mockSet.Object);

            var service = new BookRepository(mockContext.Object);
            Assert.ThrowsException<Exception>(() => service.GetOne(4));
        }

        [TestMethod]
        public async Task Update_Success()
        {
            var data = new List<Book>
            {
                new Book { Title = "BBB", Id=1 },
                new Book { Title = "ZZZ", Id = 2 },
                new Book { Title = "AAA", Id = 3 },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Book>>();
            mockSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<BookContext>();
            mockContext.Setup(c => c.Books).Returns(mockSet.Object);

            var service = new BookRepository(mockContext.Object);
            await service.Update(1, new Book { Title = "new"});
            mockSet.Verify(m => m.Add(It.IsAny<Book>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());

            var book = service.GetOne(1).Result;
            Assert.AreEqual("new", book.Title);
        }

        [TestMethod]
        public async Task Update_Failure()
        {
            var data = new List<Book>
            {
                new Book { Title = "BBB", Id=1 },
                new Book { Title = "ZZZ", Id = 2 },
                new Book { Title = "AAA", Id = 3 },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Book>>();
            mockSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<BookContext>();
            mockContext.Setup(c => c.Books).Returns(mockSet.Object);

            var service = new BookRepository(mockContext.Object);
            Assert.ThrowsException<Exception>(() => service.Update(4, new Book { Title="fake", Id = 4}));
        }

        [TestMethod]
        public async Task DeleteBook_SavesContextAsync()
        {
            var mockSet = new Mock<DbSet<Book>>();

            var mockContext = new Mock<BookContext>();
            mockContext.Setup(m => m.Books).Returns(mockSet.Object);

            var service = new BookRepository(mockContext.Object);
            await service.Delete(new Book { Title = "test" });

            mockSet.Verify(m => m.Add(It.IsAny<Book>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}
