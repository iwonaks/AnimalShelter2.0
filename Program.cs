using AnimalShelter.Entities;
using AnimalShelter.Repositories;
using AnimalShelter.Data;
using Microsoft.EntityFrameworkCore.Diagnostics;
using AnimalShelter.Repositories.Extensions;
using System.Runtime.CompilerServices;
using System.IO;
using System.Text.Json;
using AnimalShelter.Entities.Extensions;
using Microsoft.AspNetCore.Http.Features;

//var dogRepositorySql = new SqlRepository<Dog>(new AnimalShelterDbContext());
//var employeeRepositorySql = new SqlRepository<Employee>(new AnimalShelterDbContext());
var employeeRepositoryJson = new FileRepository<Employee>();
var dogRepositoryJson = new FileRepository<Dog>();

dogRepositoryJson.ItemAdded += DogRepositoryOnItemAdded;
dogRepositoryJson.ItemRemoved += DogRepositoryOnItemRemoved;

employeeRepositoryJson.ItemAdded += EmplyeeRepositoryOnItemAdded;
employeeRepositoryJson.ItemRemoved += EmplyeeRepositoryOnItemRemoved;
//employeeRepositoryJson.ItemAdded += EmloyeeSaveToFileChanges;

static void DogRepositoryOnItemAdded(object? sender, Dog d)
{
    Console.WriteLine($"Dog added => {d.Name} from {sender?.GetType().Name}");
}

static void DogRepositoryOnItemRemoved(object? sender, Dog d)
{
    Console.WriteLine($"Dog removed => {d.Name} from {sender?.GetType().Name}");
}

static void EmplyeeRepositoryOnItemAdded(object? sender, Employee e)
{
    Console.WriteLine($"Employee added => {e.Name} from {sender?.GetType().Name}");
}
static void EmplyeeRepositoryOnItemRemoved(object? sender, Employee e)
{
    Console.WriteLine($"Employee added => {e.Name} from {sender?.GetType().Name}");
}

//static void EmloyeeSaveToFileChanges(object? sender, Employee e)
//{
//    DateTime saveUtcNow = DateTime.UtcNow;

//    using (var writer = File.AppendText("changesDataEmployeeReport.txt"))
//    {
//        writer.WriteLine($"Add new employee [{saveUtcNow}], Name: {e.Name}, Surname:{e.SurName}, Education: {e.Education}");
//    }
//}

//static void DogRepositoryMale(IRepository<Dog> dogRepository)
//{
//    var items = dogRepository.GetAll();

//    foreach (var item in items)
//    {
//        if (item.Gender=="M")
//        {
//            Console.WriteLine($"Dog Male => {item.Id}, {item.Name}");
//        }
//    }
//}

//static void DogRepositoryFemale(IRepository<Dog> dogRepository)
//{
//    var items = dogRepository.GetAll();

//    foreach (var item in items)
//    {
//        if (item.Gender=="F")
//        {
//            Console.WriteLine($"Dog Female => {item.Id}, {item.Name}");
//        }
//    }
//}

//var volunteerRepository = new SqlRepository<Volunteer>(new AnimalShelterDbContext());
//var foodRepository = new SqlRepository<Food>(new AnimalShelterDbContext());
//var medicamentRepository = new SqlRepository<Medicament>(new AnimalShelterDbContext());

//static void WriteAllToConsole(IReadRepository<IEntity> repository)
//{
//    var items = repository.GetAll();

//    foreach (var item in items)
//    {
//        Console.WriteLine(item);
//    }
//}
static void ReadAllToConsole(IReadRepository<IEntity> repository)
{
    var items = repository.Read();

    if (items!=null)
    {
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
    }
    Console.WriteLine("Empty file");
}

static void AddDog(IRepository<Dog> dogRepository)
{
    while (true)
    {
        Console.WriteLine("Dog name:");
        var dogname = CheckIsNullOrEmptyAndUpper();

        Console.WriteLine("Dog gender: F or M");
        var doggender = CheckIsNullOrEmptyAndUpper();

        if (doggender!="M" && doggender!="F")
        {
            Console.WriteLine("Incorect information.");
            doggender = CheckIsNullOrEmptyAndUpper();
        }
        var newDog = new Dog { Name = dogname, Gender = doggender };

        dogRepository.Add(newDog);
        dogRepository.Save();
        break;
    }
}

static void AddEmployee(IRepository<Employee> employeeRepository)
{
    while (true)
    {
        Console.WriteLine("Employee name:");
        var employeename = CheckIsNullOrEmptyAndUpper();

        Console.WriteLine("Employee surneme:");
        var employeesurname = CheckIsNullOrEmptyAndUpper();

        Console.WriteLine("Employee education:");
        var employeeeducation = CheckIsNullOrEmptyAndUpper();

        var newEmployee = new Employee { Name = employeename, SurName = employeesurname, Education = employeeeducation };

        employeeRepository.Add(newEmployee);
        employeeRepository.Save();

        break;
    }
}

static T? FindEntityById<T>(IRepository<T> repository) where T : class, IEntity
{
    while (true)
    {
        Console.WriteLine($"Enter the Id number of the item in {typeof(T).Name}");
        var idInput = Console.ReadLine();
        int idValue = 0;

        var idEntityYouWant = int.TryParse(idInput, out idValue);

        if (!idEntityYouWant)
        {
            Console.WriteLine("Give me natural number");
        }
        else
        {
            var items = repository.Read();
            var result = repository.GetById(idValue);

            if (result == null)
            {
                Console.WriteLine($"There is no element in the {idValue} position in {typeof(T).Name}. ");
            }
            else
            {
                Console.WriteLine(repository.GetById(idValue));
            }
            return result;
        }
    }
}

Console.WriteLine("Welcome to the database Animal Shelter");
Console.WriteLine(
    "---Menu--- \n" +
    "You can do: \n" +
    "1 - View list of entity \n" +
    "2 - Find entity by Id \n" +
    "3 - Change data \n" +
    "Q - Close app \n");

while (true)
{
    Console.WriteLine("1,2, 3, Q choose what you want to do");
    var input = CheckIsNullOrEmptyAndUpper();

    switch (input)
    {
        case "1":
            Console.WriteLine("D - View all dogs\n" +
                "E - View all employees\n" +
                "Any Other key - leave and go to MENU");

            var viewEntity = CheckIsNullOrEmptyAndUpper();

            if (viewEntity == "D")
            {
                ReadAllToConsole(dogRepositoryJson);
            }

            if (viewEntity == "E")
            {
                ReadAllToConsole(employeeRepositoryJson);
            }
            break;

        case "2":

            Console.WriteLine("D - View dog by Id\n" +
                "E - View employee by Id\n" +
                "Any Other key - leave and go to MENU");

            var viewIdEntity = CheckIsNullOrEmptyAndUpper();

            if (viewIdEntity == "D")
            {
                FindEntityById(dogRepositoryJson);
            }

            if (viewIdEntity == "E")
            {
                FindEntityById(employeeRepositoryJson);
            }
            break;

        case "3":
            Console.WriteLine("A - add \n" +
                "R - remove \n" +
                "Any Other key - leave and go to MENU");

            var changeData = CheckIsNullOrEmptyAndUpper();

            if (changeData == "A")
            {
                Console.WriteLine("D - add dog\n" +
                "E - add employee\n" +
                "Any Other key - leave and go to MENU");

                var changeDataEntity = CheckIsNullOrEmptyAndUpper();


                if (changeDataEntity == "D")
                {
                    AddDog(dogRepositoryJson);
                }

                if (changeDataEntity == "E")
                {
                    AddEmployee(employeeRepositoryJson);
                }
            }
            if (changeData == "R")
            {
                Console.WriteLine("D - add dog\n" +
                "E - add employee\n" +
                "Any Other key - leave and go to MENU");

                var changeDataEntity = CheckIsNullOrEmptyAndUpper();

                if (changeDataEntity == "D")
                {
                    var result = FindEntityById(dogRepositoryJson);
                    dogRepositoryJson.Remove(result);
                    dogRepositoryJson.Save();
                }

                if (changeDataEntity == "E")
                {
                    var result = FindEntityById(employeeRepositoryJson);
                    employeeRepositoryJson.Remove(result);
                    employeeRepositoryJson.Save();
                }
            }
            break;

        case "Q":
            System.Environment.Exit(0);
            break;

        default:
            Console.WriteLine("Choose the correct key from the menu");
            break;
    }
}
static string CheckIsNullOrEmptyAndUpper()
{
    string input = Console.ReadLine().ToUpper();

    while (String.IsNullOrEmpty(input))
    {
        Console.WriteLine("Give correct value");
    }
    return input;
}