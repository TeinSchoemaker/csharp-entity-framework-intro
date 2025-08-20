using exercise.webapi.Data;
using exercise.webapi.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.webapi.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        DataContext _db;

        public AuthorRepository(DataContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            return await _db.Authors.Include(a => a.Books).ToListAsync();
        }

        public async Task<Book> GetAuthorById(int id)
        {

            var result = _db.Books.Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id);
            return await result;
        }
    }
}
