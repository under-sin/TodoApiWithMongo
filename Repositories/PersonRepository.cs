using MongoDB.Bson;
using MongoDB.Driver;
using TodoApiWithMongo.Data;
using TodoApiWithMongo.Models;

namespace TodoApiWithMongo.Repositories;

public class PersonRepository(IMongoContext context) : IPersonRepository
{
  public async Task<IEnumerable<PersonModel>> GetAll()
  {
    return await context
      .Persons
      .Find(_ => true)
      .ToListAsync();
  }

  public Task<PersonModel> GetOne(Guid id)
  {
    var filter = FindById(id);
    return context
      .Persons
      .Find(filter)
      .FirstOrDefaultAsync();
  }

  public async Task Create(PersonModel person)
  {
    await context.Persons.InsertOneAsync(person);
  }

  public async Task<bool> Delete(Guid id)
  {
    var filter = FindById(id);

    DeleteResult deleteResult = await context
      .Persons
      .DeleteOneAsync(filter);

    return deleteResult.IsAcknowledged
      && deleteResult.DeletedCount > 0;
  }

  public async Task<bool> Update(PersonModel person)
  {
    ReplaceOneResult replaceOneResult = await context
      .Persons
      .ReplaceOneAsync(
        filter: g => g.Id == person.Id,
        replacement: person
      );

    return replaceOneResult.IsAcknowledged
      && replaceOneResult.ModifiedCount > 0;
  }

    public Guid GetNextId()
    {
        return Guid.NewGuid();
    }

    private FilterDefinition<PersonModel> FindById(Guid id)
  {
    return Builders<PersonModel>.Filter.Eq(pd => pd.Id, id);
  }

}
