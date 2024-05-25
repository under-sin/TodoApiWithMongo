using MongoDB.Bson;
using MongoDB.Driver;
using TodoApiWithMongo.Data;
using TodoApiWithMongo.Models;

namespace TodoApiWithMongo.Repositories;

public class TodoRepository(IMongoContext context) : ITodoRepository
{
  public async Task<IEnumerable<TodoModel>> GetAll()
  {
    return await context
      .Todos
      .Find(_ => true)
      .ToListAsync();
  }

  public Task<TodoModel> GetOne(long id)
  {
    var filter = FindById(id);
    return context
      .Todos
      .Find(filter)
      .FirstOrDefaultAsync();
  }

  public async Task Create(TodoModel todo)
  {
    await context.Todos.InsertOneAsync(todo);
  }

  public async Task<bool> Delete(long id)
  {
    var filter = FindById(id);

    DeleteResult deleteResult = await context
      .Todos
      .DeleteOneAsync(filter);

    return deleteResult.IsAcknowledged
      && deleteResult.DeletedCount > 0;
  }

  public async Task<bool> Update(TodoModel todo)
  {
    ReplaceOneResult replaceOneResult = await context
      .Todos
      .ReplaceOneAsync(
        filter: g => g.Id == todo.Id,
        replacement: todo
      );

    return replaceOneResult.IsAcknowledged
      && replaceOneResult.ModifiedCount > 0;
  }

  public async Task<long> GetNextId()
  {
    return await context
      .Todos
      .CountDocumentsAsync(new BsonDocument()) + 1;
  }

  private FilterDefinition<TodoModel> FindById(long id)
  {
    return Builders<TodoModel>.Filter.Eq(td => td.Id, id);
  }
}
