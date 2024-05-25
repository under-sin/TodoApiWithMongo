using TodoApiWithMongo.Models;

namespace TodoApiWithMongo.Repositories;

public interface IPersonRepository
{
  Task<IEnumerable<PersonModel>> GetAll();
  Task<PersonModel> GetOne(Guid id);
  Task Create(PersonModel person);
  Task<bool> Update(PersonModel person);
  Task<bool> Delete(Guid id);
  Guid GetNextId();
}
