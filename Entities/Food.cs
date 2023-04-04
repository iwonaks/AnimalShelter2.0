namespace AnimalShelter.Entities
{
    public class Food : EntityBase
    {
        public string? Name { get; set; }
        public int? Weight { get; set; }
        public int? Quantity { get; set; }
        public string? Destination { get; set; }

        public override string ToString() => $"Id: {Id}, The name of the food: {Name}, Weight: {Weight}, Quantity: {Quantity}, Destination: {Destination}";
    }
}