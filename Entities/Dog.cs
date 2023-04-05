namespace AnimalShelter.Entities;
public class Dog : EntityBase
{
    public string? Name { get; set; }
    public string? Gender { get; set; }
    public override string ToString() => $"Id: {Id} , Dog's name: {Name}, Gender: {Gender}";
}
