namespace AnimalShelter.Repositories;

using AnimalShelter.Entities;

public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T> where T : class, IEntity
{

}