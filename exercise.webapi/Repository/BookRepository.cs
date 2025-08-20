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

        public async Task<Book> GetBook(int id)
        {
            var book = _db.Books.Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id);

            return await book;
        }

        public async Task<Book> UpdateBook(Book book, Author author)
        {
            var bookID = await _db.Books.Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == book.Id);
            var authorID = await _db.Authors.FindAsync(author.Id);

            book.AuthorId = author.Id;
            await _db.SaveChangesAsync();

            var updated = _db.Books.Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == book.Id);

            return await updated;
        }

        public async Task<Book> DeleteBook(int id)
        {
            var target = await _db.Books.Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id);

            _db.Books.Remove(target);

            return target;
        }

        public async Task<Book> CreateBook(Book book)
        {
            var author = _db.Authors.FindAsync(book.AuthorId);

            await _db.Books.AddAsync(book);
            await _db.SaveChangesAsync();

            var created = _db.Books.Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == book.Id);
            return await created;
        }
    }
}
