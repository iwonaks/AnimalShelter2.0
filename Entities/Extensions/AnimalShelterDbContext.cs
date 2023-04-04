using Microsoft.EntityFrameworkCore;
using AnimalShelter.Entities;

namespace AnimalShelter.Data;

public class AnimalShelterDbContext : DbContext 
{
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Dog> Dogs => Set<Dog>();
    public DbSet<Volunteer> Volunteers => Set<Volunteer>();
    public DbSet<Food> Foods => Set<Food>();
    public DbSet<Medicament> Medicaments => Set<Medicament>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseInMemoryDatabase("StorageAnimalShelterDb");
    }
}
