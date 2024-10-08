using Microsoft.AspNetCore.Mvc;
using SkylerO_AmplifundProj.Models;
using SkylerO_AmplifundProj.Repostitories;

namespace SkylerO_AmplifundProj.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {

        private readonly ILogger<BookController> _logger;
        private readonly IBookRepository _bookRepository;

        public BookController(ILogger<BookController> logger, IBookRepository bookRepository)
        {
            _logger = logger;
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public List<Book> GetAll()
        {
            return _bookRepository.GetAll();
        }

        [HttpDelete]
        public void Delete(int id)
        {
            Book book = GetOne(id);
            _bookRepository.Delete(book);
        }

        [HttpPost]
        public void Post(Book book)
        {
            _bookRepository.Add(book);
        }

        [HttpPut("/{id}")]
        public void Put(int id, Book book)
        {
            _bookRepository.Update(id, book);
        }

        [HttpGet("/{id}")]
        public Book GetOne(int id) {
            return _bookRepository.GetOne(id);
        }
    }
}
