using MongoDB.Driver;
using TodoApiWithMongo.Models;

namespace TodoApiWithMongo.Data;

public interface IMongoContext
{
  IMongoCollection<TodoModel> Todos { get; }
  IMongoCollection<PersonModel> Persons { get; }
}
