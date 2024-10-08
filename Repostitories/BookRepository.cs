using SkylerO_AmplifundProj.Models;

namespace SkylerO_AmplifundProj.Repostitories
{
    public class BookRepository:IBookRepository
    {
        private List<Book> books; //temporary db replacement
        Random idGenerator = new Random();

        public BookRepository() {
            books = new List<Book>();
        }

        public List<Book> GetAll() {
            return books;
        }

        public Book GetOne(int id) {
            Book? book = books.FirstOrDefault(x => x.Id == id);
            if(book != null)
            {
                return book;
            } else
            {
                throw new Exception($"Book with id {id} does not exist");
            }
        }

        public void Add(Book book) {
            books.Add(book);
        }

        public void Update(int id, Book book) {
            Book? oldBook = books.FirstOrDefault(x => x.Id == id);
            if(oldBook != null)
            {
                oldBook = book;
            } else
            {
                throw new Exception($"Book with id {book.Id} does not exist");
            }
        }

        public void Delete(Book book) { 
            books.Remove(book);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
