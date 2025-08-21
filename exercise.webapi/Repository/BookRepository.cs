using exercise.webapi.Data;
using exercise.webapi.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.webapi.Repository
{
    public class BookRepository : IBookRepository
    {
        DataContext _db;

        public BookRepository(DataContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _db.Books.Include(b => b.Author).ToListAsync();
        }

        public async Task<Book> GetBookById(int id)
        {
            var book = _db.Books.Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id);

            return await book;
        }

        public async Task<Book> UpdateBook(int bookId, int authorId)
        {
            var book = await _db.Books.Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == bookId);
            var author = await _db.Authors.FindAsync(authorId);

            book.AuthorId = authorId;
            await _db.SaveChangesAsync();

            var updated = _db.Books.Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == bookId);

            return await updated;
        }

        public async Task<Book> DeleteBook(int id)
        {
            var target = await _db.Books.Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id);

            _db.Books.Remove(target);
            await _db.SaveChangesAsync();

            return target;
        }

        public async Task<Book> CreateBook(Book book)
        {
            _db.Books.Add(book);
            await _db.SaveChangesAsync();
            return await GetBookById(book.Id);
        }
    }
}
