using exercise.webapi.Models;

namespace exercise.webapi.Repository
{
    public interface IBookRepository
    {
        public Task<IEnumerable<Book>> GetAllBooks();
        public Task<Book> GetBookById(int id);
        public Task<Book> UpdateBook(int bookId, int authorId);
        public Task<Book> DeleteBook(int id);
        public Task<Book> CreateBook(Book book);
        public Task<Book> AssignAuthor(int bookId, int authorId);
        public Task<Book> RemoveAuthor(int bookId);
    }
}
