using AnimalShelter.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.IO;
using System.Text.Json;

namespace AnimalShelter.Repositories
{
    public class FileRepository<T> : IRepository<T> where T : class, IEntity, new()
    {
        public List<T> _items = new();

        public event EventHandler<T>? ItemAdded;
        public event EventHandler<T>? ItemRemoved;

        private readonly string path = $"{typeof(T).Name}_save.json";

        public IEnumerable<T> GetAll()
        {
            return _items.ToList();
        }

        public T GetById(int id)
        {
            return _items.Single(item => item.Id == id);
        }

        public void Add(T item)
        {
            item.Id = _items.Count + 1;
            _items.Add(item);
            ItemAdded?.Invoke(this, item);
        }

        public void Remove(T item)
        {
            item.Id = _items.Count- 1;
            _items.Remove(item);
            ItemRemoved?.Invoke(this, item);
        }

        public void Save()
        {
            File.Delete(path);
            var objectsSerialized = JsonSerializer.Serialize<IEnumerable<T>>(_items);
            File.WriteAllText(path, objectsSerialized);
          
        }
        public IEnumerable<T> Read()
        {
            if (File.Exists(path))
            {
                var objectsSerialized = File.ReadAllText(path);
                var deserializedObjects = JsonSerializer.Deserialize<IEnumerable<T>>(objectsSerialized);
                if (deserializedObjects != null)
                {
                    _items.Clear();
                    foreach (var item in deserializedObjects)
                    { 
                        _items.Add(item);
                    }
                }
            }
            return _items;
        }
    }
}
