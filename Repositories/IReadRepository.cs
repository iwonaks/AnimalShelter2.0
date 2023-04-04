using AnimalShelter.Entities;

namespace AnimalShelter.Repositories;
public interface IReadRepository<out T> where T : class, IEntity
{
    IEnumerable<T> GetAll();
    T GetById(int id);
    IEnumerable<T> Read();
}
