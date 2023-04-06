using Microsoft.EntityFrameworkCore;
using AnimalShelter.Entities;
//using Newtonsoft.Json;
using System.Text.Json;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace AnimalShelter.Repositories;

public class SqlRepository<T> : IRepository<T> where T : class, IEntity, new()
{
    private readonly DbSet<T> _dbSet;
    private readonly DbContext _dbContext;

    //private readonly Action<T>? _itemAdded;
    //private readonly Action<T>? _itemRemoved;

    public SqlRepository(DbContext dbContext, Action<T>? ItemAdded = null, Action<T>? ItemRemoved = null)
    {
        _dbContext= dbContext;
        _dbSet= _dbContext.Set<T>();
        //_itemAdded= ItemAdded;
        //_itemRemoved=ItemRemoved;
    }

    public event EventHandler<T>? ItemAdded;
    public event EventHandler<T>? ItemRemoved;

    public IEnumerable<T> GetAll()
    {
        return _dbSet.ToList();
    }

    public T GetById(int id)
    {
        return _dbSet.Find(id);
    }

    public void Add(T item)
    {
        _dbSet.Add(item);
        ItemAdded?.Invoke(this, item);
    }

    public void Remove(T item)
    {
        _dbSet.Remove(item);
        ItemRemoved?.Invoke(this, item);    
    }
    public void Save()
    {
        _dbContext.SaveChanges();
    }
    public IEnumerable<T> Read()
    {
        return _dbSet.ToList();
    }
}