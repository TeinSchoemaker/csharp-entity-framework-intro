namespace exercise.webapi.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public AuthorDTO? Author { get; set; }
    }
}
