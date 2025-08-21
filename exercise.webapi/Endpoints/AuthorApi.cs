using exercise.webapi.DTO;
using exercise.webapi.Repository;
using Microsoft.AspNetCore.Mvc;

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

        [ProducesResponseType(StatusCodes.Status200OK)]
        private static async Task<IResult> GetAuthors(IAuthorRepository authorRepository)
        {
            var authors = await authorRepository.GetAllAuthors();
            var dto = authors.Select(a => a.ConvertAuthorDTO());
            return TypedResults.Ok(dto);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        private static async Task<IResult> GetAuthorById(int id, IAuthorRepository authorRepository)
        {
            var author = await authorRepository.GetAuthorById(id);
            var dto = author.ConvertAuthorDTO();
            return TypedResults.Ok(dto);
        }
    }
}
