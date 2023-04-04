//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.Json;
//using System.Threading.Tasks;
//using System.IO;
//using AnimalShelter.Entities;

//namespace AnimalShelter.Repositories
//{
//    public class Persist : IPersist
//    {
//        private const string fileName = "employee_rep.json";

//        private readonly JsonSerializerOptions options = new JsonSerializerOptions
//        {
//            WriteIndented = true,
//        };

//        public Employee? Load()
//        {
//            if (File.Exists(fileName))
//            {
//                var contents = File.ReadAllText(fileName, Encoding.UTF8);

//                var item = JsonSerializer.Deserialize<Employee>(contents, this.options);

//                return item;
//            }
//            return null;
//        }

//        public void SaveToJson(Employee[] item)
//        {
//            var contents = JsonSerializer.Serialize(item, this.options);

//            File.AppendAllText(fileName, contents, Encoding.UTF8);
//        }
//    }

//    internal sealed class Utf8StringWriter : StringWriter
//    {
//        public override Encoding Encoding => Encoding.UTF8;
//    }
//}