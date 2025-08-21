using exercise.webapi.Models;
using exercise.webapi.Repository;
using exercise.webapi.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using static System.Reflection.Metadata.BlobBuilder;

namespace exercise.webapi.Endpoints
{
    public static class BookApi
    {
        public static void ConfigureBooksApi(this WebApplication app)
        {
            var books = app.MapGroup("books");

            app.MapGet("/", GetBooks);
            app.MapGet("/{id:int}", GetBookById);
            app.MapPut("/{bookId:int}/author/{authorId:int}", UpdateBook);
            app.MapDelete("/{id:int}", DeleteBook);
            app.MapPost("/{book}/author/{author}", CreateBook);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        private static async Task<IResult> GetBooks(IBookRepository bookRepository)
        {
            var books = await bookRepository.GetAllBooks();

            var dto = books.Select(b => b.ConvertBookDTO());

            return TypedResults.Ok(dto);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        private static async Task<IResult> GetBookById(int id, IBookRepository bookRepository)
        {
            var book = await bookRepository.GetBookById(id);
            var dto = book.ConvertBookDTO();
            return TypedResults.Ok(dto);
        }
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        private static async Task<IResult> UpdateBook(int bookId, int authorId, IBookRepository bookRepository)
        {
            var updated = await bookRepository.UpdateBook(bookId, authorId);
            var dto = updated.ConvertBookDTO();
            return TypedResults.Ok(dto);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        private static async Task<IResult> DeleteBook(int id, IBookRepository bookRepository)
        {
            var book = await bookRepository.DeleteBook(id);
            var dto = book.ConvertBookDTO();
            return TypedResults.Ok(dto);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> CreateBook(string title, int id, IBookRepository bookRepository)
        {

            if (id <= 0)
            {
                return TypedResults.NotFound("Author ID is not valid");
            }

            var newBook = new Book
            {
                Title = title,
                AuthorId = id,
            };

            var created = await bookRepository.CreateBook(newBook);

            if (created == null)
            {
                return TypedResults.BadRequest("Book Object not valid");
            }

            var dto = created.ConvertBookDTO();

            return TypedResults.Created($"/books/{dto.Id}");
        }
    }
}
