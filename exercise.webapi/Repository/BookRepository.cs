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
            return await _db.Books.Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Book> UpdateBook(int bookID, int authorID)
        {
            var book = await _db.Books.Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == bookID);
            var author = await _db.Authors.FindAsync(authorID);

            book.AuthorId = authorID;
            await _db.SaveChangesAsync();

            return await _db.Books.Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == bookID);
        }
    }
}
