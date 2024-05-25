using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TodoApiWithMongo.Models;

public class PersonModel
{
  [BsonId]
  public ObjectId InternalId { get; set; }
  public Guid Id { get; set; }
  public string Email { get; set; } = string.Empty;
  public string Name { get; set; } = string.Empty;
  public string Phone { get; set; } = string.Empty;
  public DateTime BirthDate { get; set; }
}
