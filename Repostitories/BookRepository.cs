using Microsoft.EntityFrameworkCore;
using SkylerO_AmplifundProj.Models;

namespace SkylerO_AmplifundProj.Repostitories
{
    public class BookRepository : IBookRepository
    {
        Random idGenerator = new Random();
        private readonly BookContext _context;

        public BookRepository(BookContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAll() {
            return _context.Books.ToList();
        }

        public async Task<Book> GetOne(int id) {
            Book? book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
            if(book != null)
            {
                return book;
            } else
            {
                throw new Exception($"Book with id {id} does not exist");
            }
        }

        public async Task<Book> Add(Book book) {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task Update(int id, Book book) {
            Book? oldBook = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
            if(oldBook != null)
            {
                oldBook = book;
                await _context.SaveChangesAsync();
            } else
            {
                throw new Exception($"Book with id {book.Id} does not exist");
            }
        }

        public async Task Delete(Book book) {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
