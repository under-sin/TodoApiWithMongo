using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TodoApiWithMongo.Models;

namespace TodoApiWithMongo.Data;

public class MongoContext : IMongoContext
{
  private readonly IMongoDatabase _db;
  public MongoContext(IOptions<MongoConfiguration> config)
  {
    var client = new MongoClient(config.Value.ConnectionString);
    _db = client.GetDatabase(config.Value.Database);
  }

  public IMongoCollection<TodoModel> Todos => _db.GetCollection<TodoModel>("Todos");
  public IMongoCollection<PersonModel> Persons => _db.GetCollection<PersonModel>("Persons");
}
