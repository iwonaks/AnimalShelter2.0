using AnimalShelter.Repositories;
namespace AnimalShelter.Entities

{
    public class Employee : EntityBase
    {
        public string? Name { get; set; }
        public string? SurName { get; set; }
        public string? Education { get; set; }
        public override string ToString() => $"Id: {Id} Employee's name: {Name}, Surname: {SurName}, Education: {Education} ";
    }
}
