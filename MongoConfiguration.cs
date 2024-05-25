namespace TodoApiWithMongo;

public class MongoConfiguration
{
  public string Database { get; set; } = string.Empty;
  public string Host { get; set; } = string.Empty;
  public int Port { get; set; }
  public string User { get; set; } = string.Empty;
  public string Password { get; set; } = string.Empty;
  public string ConnectionString
  {
    get
    {
      if (string.IsNullOrEmpty(User) || string.IsNullOrEmpty(Password))
        return $"mongodb://{Host}:{Port}";

      return $"mongodb://{User}:{Password}@{Host}:{Port}";
    }
  }
}
