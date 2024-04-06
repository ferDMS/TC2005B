using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
namespace Tarea_3
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Cover { get; set; }
    }


public static class BookEndpoints
{
	public static void MapBookEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Book").WithTags(nameof(Book));

        group.MapGet("/", () =>
        {
            return new [] { new Book() };
        })
        .WithName("GetAllBooks")
        .WithOpenApi();

        group.MapGet("/{id}", (int id) =>
        {
            //return new Book { ID = id };
        })
        .WithName("GetBookById")
        .WithOpenApi();

        group.MapPut("/{id}", (int id, Book input) =>
        {
            return TypedResults.NoContent();
        })
        .WithName("UpdateBook")
        .WithOpenApi();

        group.MapPost("/", (Book model) =>
        {
            //return TypedResults.Created($"/api/Books/{model.ID}", model);
        })
        .WithName("CreateBook")
        .WithOpenApi();

        group.MapDelete("/{id}", (int id) =>
        {
            //return TypedResults.Ok(new Book { ID = id });
        })
        .WithName("DeleteBook")
        .WithOpenApi();
    }
}
}
