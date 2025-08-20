using exercise.webapi.Models;
using exercise.webapi.Repository;
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
            app.MapPut("/{book}/author/{author}", UpdateBook);
            app.MapPut("/{id:int}", DeleteBook);
            app.MapPut("/{book}/author/{author}", CreateBook);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        private static async Task<IResult> GetBooks(IBookRepository bookRepository)
        {
            var books = await bookRepository.GetAllBooks();
            return TypedResults.Ok(books);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        private static async Task<IResult> GetBookById(int id, IBookRepository bookRepository)
        {
            var book = await bookRepository.GetBook(id);
            return TypedResults.Ok(book);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        private static async Task<IResult> UpdateBook(Book book, Author author, IBookRepository bookRepository)
        {
            var updated = await bookRepository.UpdateBook(book, author);

            return TypedResults.Ok(updated);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        private static async Task<IResult> DeleteBook(int id, IBookRepository bookRepository)
        {
            var book = await bookRepository.DeleteBook(id);
            return TypedResults.Ok(book);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> CreateBook(Book book, IBookRepository bookRepository)
        {

            if (book.AuthorId <= 0)
            {
                return TypedResults.NotFound("Author ID is not valid");
            }

            var created = await bookRepository.CreateBook(book);
            if (created == null)
            {
                return TypedResults.BadRequest("Book Object not valid");
            }

            return TypedResults.Ok(created);
        }
    }
}
