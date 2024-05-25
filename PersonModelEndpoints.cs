using Microsoft.AspNetCore.Mvc;
using TodoApiWithMongo.Models;
using TodoApiWithMongo.Repositories;

namespace TodoApiWithMongo;

public static class PersonModelEndpoints
{
  public static void MapPersonModelEndpoints(this IEndpointRouteBuilder routes)
  {
    //GetAllPersonModels
    routes.MapGet("/api/PersonModel", async ([FromServices] IPersonRepository repo) =>
    {
      return await repo.GetAll();
    })
    .WithName("GetAllPersonModels");

    //GetPersonModelById
    routes.MapGet("/api/PersonModel/{id}", async (Guid id, [FromServices] IPersonRepository repo) =>
    {
      return await repo.GetOne(id) is PersonModel model
        ? Results.Ok(model)
        : Results.NotFound();
    })
    .WithName("GetPersonModelById");

    //UpdatePersonModel
    routes.MapPut("/api/PersonModel/{id}", async (Guid id, PersonModel personModel, [FromServices] IPersonRepository repo) =>
    {
      var personFromDb = await repo.GetOne(id);
      if (personFromDb is null)
        return Results.NotFound();

      personModel.Id = personFromDb.Id;
      personModel.InternalId = personFromDb.InternalId;
      await repo.Update(personModel);
      return Results.Ok(personModel);
    })
    .WithName("UpdatePersonModel");

    //CreatePersonModel
    routes.MapPost("/api/PersonModel", async (PersonModel personModel, [FromServices] IPersonRepository repo) =>
    {
      personModel.Id = repo.GetNextId();
      await repo.Create(personModel);

      return Results.Created($"/api/PersonModel/{personModel.InternalId}", personModel);
    })
    .WithName("CreatePersonModel");

    //DeletePersonModel
    routes.MapDelete("/api/PersonModel/{id}", async (Guid id, [FromServices] IPersonRepository repo) =>
    {
      var post = await repo.GetOne(id);
      if (post is null)
        return Results.NotFound();

      await repo.Delete(id);
      return Results.NoContent();
    })
    .WithName("DeletePersonModel");
  }
}
