namespace AnimalShelter.Repositories;

using AnimalShelter.Entities;

public interface IFileRepository<T> : IRepository<T> where T : class, IEntity
{
    T? FindEntityById();

}