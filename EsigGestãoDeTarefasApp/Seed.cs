using System;
using System.Collections.Generic;
using System.Linq; // Certifique-se de incluir esta diretiva
using EsigGestãoDeTarefasApp.Enums;
using EsigGestãoDeTarefasApp.Models;

namespace EsigGestãoDeTarefasApp.Data
{
    public class Seed
    {
        private readonly DataContext _dataContext;

        // Construtor que recebe o DataContext
        public Seed(DataContext context)
        {
            _dataContext = context;
        }

        public void SeedDataContext()
        {
            // Verifica se já existem Employees ou Tasks no banco de dados
            if (_dataContext.Employees.Any() || _dataContext.Tasks.Any())
            {
                return; // O banco de dados já foi populado
            }

            // Cria uma lista de Tasks de exemplo
            var tasks = new List<Models.Task>
            {
                new Models.Task
                {
                    Title = "Develop login system",
                    Description = "Implement authentication and authorization logic",
                    Status = StatusEnum.Pending, // Usando enum
                    Deadline = new DateTime(2024, 12, 31),
                    Priority = "High"
                },
                new Models.Task
                {
                    Title = "Create database schema",
                    Description = "Design the database schema for the application",
                    Status = StatusEnum.Completed, // Usando enum
                    Deadline = new DateTime(2024, 11, 15),
                    Priority = "Medium"
                },
                new Models.Task
                {
                    Title = "Test API endpoints",
                    Description = "Write unit tests for all API endpoints",
                    Status = StatusEnum.InProgress, // Usando enum
                    Deadline = new DateTime(2024, 12, 1),
                    Priority = "Low"
                }
            };

            // Cria uma lista de Employees de exemplo
            var employees = new List<Employee>
            {
                new Employee
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    Password = "password123" // Considere usar hash para senhas
                },
                new Employee
                {
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@example.com",
                    Password = "password456"
                },
                new Employee
                {
                    FirstName = "Alice",
                    LastName = "Johnson",
                    Email = "alice.johnson@example.com",
                    Password = "password789"
                }
            };

            // Adiciona os employees e tasks ao DataContext
            _dataContext.Employees.AddRange(employees);
            _dataContext.Tasks.AddRange(tasks);
            _dataContext.SaveChanges(); // Salva primeiro para garantir que os IDs sejam gerados

            // Atualiza os relacionamentos EmployeeTask com os IDs gerados
            var employeeTasks = new List<EmployeeTask>
            {
                new EmployeeTask
                {
                    EmployeeId = employees[0].Id, // ID agora está disponível
                    TaskId = tasks[0].Id,         // ID agora está disponível
                    AssignedDate = DateTime.Now
                },
                new EmployeeTask
                {
                    EmployeeId = employees[1].Id,
                    TaskId = tasks[1].Id,
                    AssignedDate = DateTime.Now
                },
                new EmployeeTask
                {
                    EmployeeId = employees[2].Id,
                    TaskId = tasks[2].Id,
                    AssignedDate = DateTime.Now
                }
            };

            // Adiciona os relacionamentos EmployeeTask ao DataContext
            _dataContext.EmployeeTasks.AddRange(employeeTasks);
            _dataContext.SaveChanges();
        }
    }
}
