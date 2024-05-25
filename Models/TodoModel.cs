using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TodoApiWithMongo.Models;

public class TodoModel
{
  [BsonId]
  public ObjectId InternalId { get; set; }
  public long Id { get; set; }
  public string Title { get; set; } = string.Empty;
  public string Content { get; set; } = string.Empty;
} 
