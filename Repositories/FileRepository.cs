using AnimalShelter.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace AnimalShelter.Repositories
{
    public class FileRepository<T> : IRepository<T> where T : class, IEntity, new()
    {
        private readonly List<T> _items = new();

        private int lastUsedId = 1;

        public event EventHandler<T>? ItemAdded, ItemRemoved;

        private readonly string path = $"{typeof(T).Name}_save.json";

        public IEnumerable<T> GetAll()
        {
            return _items.ToList();
        }

        public T GetById(int id)
        {
            var itemById = _items.SingleOrDefault(item => item.Id == id);
            if (itemById == null)
            {
                Console.WriteLine($"Element {id} at the base '{typeof(T).Name}' is empty.");
            }
            return itemById;
        }

        public void Add(T item)
        {
            if (_items.Count == 0)
            {
                item.Id = lastUsedId;
                lastUsedId++;
            }
            else if (_items.Count > 0)
            {
                lastUsedId = _items[_items.Count - 1].Id;
                item.Id = ++lastUsedId;
            }

            _items.Add(item);
            ItemAdded?.Invoke(this, item);
        }
        public void ReadAllToConsole(IReadRepository<IEntity> repository)
        {
            var items = repository.Read();

            if (items!=null && items.Count()!=0)
            {
                foreach (var item in items)
                {
                    Console.WriteLine(item);
                }
            }
            else
            {
                Console.WriteLine("Empty file");
            }
        }

        public T? FindEntityById<T>(IRepository<T> repository) where T : class, IEntity
        {
            while (true)
            {
                Console.WriteLine($"Enter the Id number of the item in {typeof(T).Name}");
                var idInput = Console.ReadLine();

                var idEntityYouWant = int.TryParse(idInput, out int idValue);

                if (!idEntityYouWant)
                {
                    Console.WriteLine("Give me natural number");
                }
                else
                {
                    repository.Read();
                    var result = repository.GetById(idValue);

                    Console.WriteLine(result);
                    return result;
                }
            }
        }

        public void Remove(T item)
        {
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

                _items.Clear();
                if (deserializedObjects != null)
                {
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
