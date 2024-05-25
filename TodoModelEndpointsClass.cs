using Microsoft.AspNetCore.Mvc;
using TodoApiWithMongo.Models;
using TodoApiWithMongo.Repositories;

namespace TodoApiWithMongo;

public static class TodoModelEndpointsClass
{
  public static void MapTodoModelEndpoints(this IEndpointRouteBuilder routes)
  {
    //GetAllTodoModels
    routes.MapGet("/api/TodoModel", async ([FromServices] ITodoRepository repo) =>
    {
      return await repo.GetAll();
    })
    .WithName("GetAllTodoModels");

    //GetTodoModelById
    routes.MapGet("/api/TodoModel/{id}", async (long id, [FromServices] ITodoRepository repo) =>
    {
      return await repo.GetOne(id) is TodoModel model
        ? Results.Ok(model)
        : Results.NotFound();
    })
    .WithName("GetTodoModelById");

    //UpdateTodoModel
    routes.MapPut("/api/TodoModel/{id}", async (long id, TodoModel todoModel, [FromServices] ITodoRepository repo) =>
    {
      var todoFromDb = await repo.GetOne(id);
      if (todoFromDb is null)
        return Results.NotFound();

      todoModel.Id = todoFromDb.Id;
      todoModel.InternalId = todoFromDb.InternalId;
      await repo.Update(todoModel);
      return Results.Ok(todoModel);
    })
    .WithName("UpdateTodoModel");

    //CreateTodoModel
    routes.MapPost("/api/TodoModel", async (TodoModel todoModel, [FromServices] ITodoRepository repo) =>
    {
      todoModel.Id = await repo.GetNextId();
      await repo.Create(todoModel);

      return Results.Created($"/api/TodoModel/{todoModel.InternalId}", todoModel);
    })
    .WithName("CreateTodoModel");

    //DeleteTodoModel
    routes.MapDelete("/api/TodoModel/{id}", async (long id, [FromServices] ITodoRepository repo) =>
    {
      var post = await repo.GetOne(id);
      if (post is null)
        return Results.NotFound();

      await repo.Delete(id);
      return Results.NoContent();
    })
    .WithName("DeleteTodoModel");
  }
}
