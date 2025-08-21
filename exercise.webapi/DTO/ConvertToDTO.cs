using exercise.webapi.Models;

namespace exercise.webapi.DTO
{
    public static class ConvertToDTO
    {
        public static BookDTO ConvertBookDTO(this Book book) =>
            new BookDTO
            {
                Id = book.Id,
                Title = book.Title,
                Author = new AuthorDTO
                {
                    Id = book.Author.Id,
                    FirstName = book.Author.FirstName,
                    LastName = book.Author.LastName,
                    Email = book.Author.Email
                }
            };

        public static AuthorDTO ConvertAuthorDTO(this Author author) =>
            new AuthorDTO
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Email = author.Email,
                Books = author.Books.Select(b => new BookDTO
                {
                    Id = b.Id,
                    Title = b.Title
                }).ToList()
            };
    }
}