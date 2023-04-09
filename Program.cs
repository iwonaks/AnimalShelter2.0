using AnimalShelter;
using AnimalShelter.Entities;
using AnimalShelter.Repositories;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddSingleton<IApp,App>();

services.AddSingleton<IFileRepository<Employee>,FileRepository<Employee>>();
services.AddSingleton<IFileRepository<Dog>, FileRepository<Dog>>();

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>();
app.Run();

