using exercise.webapi.Data;
using exercise.webapi.Repository;

namespace exercise.webapi.Endpoints
{
    public static class AuthorApi
    {
        public static void ConfigureAuthorApi(this WebApplication app)
        {
            var books = app.MapGroup("authors");

            app.MapGet("/authors", GetAuthors);
            app.MapGet("/authors/{id:int}", GetAuthorById);
        }

        private static async Task<IResult> GetAuthors(IAuthorRepository authorRepository)
        {
            var authors = await authorRepository.GetAllAuthors();
            return TypedResults.Ok(authors);
        }

        private static async Task<IResult> GetAuthorById(int id, IAuthorRepository authorRepository)
        {
            var author = await authorRepository.GetAuthorById(id);
            return TypedResults.Ok(author);
        }
    }
}
