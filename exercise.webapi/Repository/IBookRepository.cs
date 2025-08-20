using exercise.webapi.Models;

namespace exercise.webapi.Repository
{
    public interface IBookRepository
    {
        public Task<IEnumerable<Book>> GetAllBooks();
        public Task<Book> GetBook(int id);
        public Task<Book> UpdateBook(Book book, Author author);
        public Task<Book> DeleteBook(int id);
        public Task<Book> CreateBook(Book book);
    }
}
