using exercise.webapi.Models;
using exercise.webapi.Repository;
using System.Net.NetworkInformation;
using static System.Reflection.Metadata.BlobBuilder;

namespace exercise.webapi.Endpoints
{
    public static class BookApi
    {
        public static void ConfigureBooksApi(this WebApplication app)
        {
            app.MapGet("/books", GetBooks);
            app.MapGet("/books/{id:int}", GetBookById);
            app.MapPut("/books/{id:int}/author/{authorId:int}", UpdateBook);
        }

        private static async Task<IResult> GetBooks(IBookRepository bookRepository)
        {
            var books = await bookRepository.GetAllBooks();
            return TypedResults.Ok(books);
        }

        private static async Task<IResult> GetBookById(int id, IBookRepository bookRepository)
        {
            var book = await bookRepository.GetBook(id);
            return TypedResults.Ok(book);
        } 

        private static async Task<IResult> UpdateBook(int id, int authorID, IBookRepository bookRepository)
        {
            var updated = await bookRepository.UpdateBook(id, authorID);

            return TypedResults.Ok(updated);
        }
    }
}
