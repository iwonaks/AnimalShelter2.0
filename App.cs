using AnimalShelter.Entities;
using AnimalShelter.Repositories;

namespace AnimalShelter;

public class App : IApp
{
    private readonly IRepository<Employee> _employeeRepository;
    private readonly IRepository<Dog> _dogRepository;

    public App(IRepository<Employee> employeeRepository, IRepository<Dog> dogRepository)
    {
        _employeeRepository = employeeRepository;
        _dogRepository = dogRepository;
    }

    public void Run()
    {
        //var dogRepositorySql = new SqlRepository<Dog>(new AnimalShelterDbContext());
        //var employeeRepositorySql = new SqlRepository<Employee>(new AnimalShelterDbContext());
        //var _employeeRepository = new FileRepository<Employee>();
        //var _dogRepository = new FileRepository<Dog>();

        var _specificRepoDog = (FileRepository<Dog>)_dogRepository;
        var _specificRepoEmployee = (FileRepository<Employee>)_employeeRepository;

        _specificRepoDog.ItemAdded += DogRepositoryOnItemAdded;
        _specificRepoDog.ItemRemoved += DogRepositoryOnItemRemoved;

        _specificRepoEmployee.ItemAdded += EmplyeeRepositoryOnItemAdded;
        _specificRepoEmployee.ItemRemoved += EmplyeeRepositoryOnItemRemoved;
        //employeeRepositoryJson.ItemAdded += EmloyeeSaveToFileChanges;

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
                    Console.WriteLine(
                        "\tD - View all dogs\n" +
                        "\tE - View all employees\n" +
                        "\tAny Other key - leave and go to MENU");

                    var viewEntity = CheckIsNullOrEmptyAndUpper();

                    if (viewEntity == "D")
                    {
                        _dogRepository.Read();
                        var allDogs = _dogRepository.GetAll();

                        foreach (var entity in allDogs)
                        {
                            Console.WriteLine(entity);
                        }
                    }

                    if (viewEntity == "E")
                    {
                        _employeeRepository.Read();
                        var allEmployees = _employeeRepository.GetAll();
                        foreach (var entity in allEmployees)
                        {
                            Console.WriteLine(entity);
                        }
                    }
                    break;

                case "2":

                    Console.WriteLine(
                        "\tD - View dog by Id\n" +
                        "\tE - View employee by Id\n" +
                        "\tAny Other key - leave and go to MENU");

                    var viewIdEntity = CheckIsNullOrEmptyAndUpper();

                    if (viewIdEntity == "D")
                    { 
                        _specificRepoDog.FindEntityById();
                    }

                    if (viewIdEntity == "E")
                    {
                        _specificRepoEmployee.FindEntityById();
                    }
                    break;

                case "3":
                    Console.WriteLine(
                        "\tA - add \n" +
                        "\tR - remove \n" +
                        "\tAny Other key - leave and go to MENU");

                    var changeData = CheckIsNullOrEmptyAndUpper();

                    if (changeData == "A")
                    {
                        Console.WriteLine(
                        "\tD - add dog\n" +
                        "\tE - add employee\n" +
                        "\tAny Other key - leave and go to MENU");

                        var changeDataEntity = CheckIsNullOrEmptyAndUpper();

                        if (changeDataEntity == "D")
                        {
                            AddDog(_dogRepository);
                        }

                        if (changeDataEntity == "E")
                        {
                            AddEmployee(_employeeRepository);
                        }
                    }

                    if (changeData == "R")
                    {
                        Console.WriteLine(
                        "\tD - remove dog\n" +
                        "\tE - remove employee\n" +
                        "\tAny Other key - leave and go to MENU");

                        var changeDataEntity = CheckIsNullOrEmptyAndUpper();

                        if (changeDataEntity == "D")
                        {
                            var result = _specificRepoDog.FindEntityById();
                            if (result!=null)
                            {
                                _dogRepository.Remove(result);
                                _dogRepository.Save();
                            }
                            else
                            {
                                Console.WriteLine($"Try again or search the base. Select 1 then D");
                            }
                        }

                        if (changeDataEntity == "E")
                        {
                            var result = _specificRepoEmployee.FindEntityById();
                            if (result!=null)
                            {
                                _employeeRepository.Remove(result);
                                _employeeRepository.Save();
                            }
                            else
                            {
                                Console.WriteLine($"Try again or search the base. Select 1 then E");
                            }
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
            Console.WriteLine($"Employee removed => {e.Name} from {sender?.GetType().Name}");
        }

        static void AddDog(IRepository<Dog> dogRepository)
        {
            while (true)
            {
                Console.WriteLine("Dog name:");
                var dogname = CheckIsNullOrEmptyAndUpper();

                Console.WriteLine("Dog gender: F or M");
                var doggender = CheckIsNullOrEmptyAndUpper();

                while (doggender!="M" && doggender!="F")
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
        static string CheckIsNullOrEmptyAndUpper()
        {
            string input = Console.ReadLine().ToUpper();

            while (String.IsNullOrEmpty(input))
            {
                Console.WriteLine("Give correct value");
                break;
            }
            return input;
        }

        //static void EmloyeeSaveToFileChanges(object? sender, Employee e)
        //{
        //    DateTime saveUtcNow = DateTime.UtcNow;

        //    using (var writer = File.AppendText("changesDataEmployeeReport.txt"))
        //    {
        //        writer.WriteLine($"Add new employee [{saveUtcNow}], Name: {e.Name}, Surname:{e.SurName}, Education: {e.Education}");
        //    }
        //}
    }
}

