namespace AnimalShelter.Entities
{
    public class Medicament : EntityBase
    {
        public string? Name { get; set; }
        public double? Dose { get; set; }
        public double? Quantity { get; set; }
        public override string ToString() => $"Id: {Id}, Name: {Name}, Dose: {Dose} g, Quantity: {Quantity} g";
    }
}
