using System;
using System.Collections.Generic;
using System.Linq;
using EsigGestãoDeTarefasApp.Enums;
using EsigGestãoDeTarefasApp.Helpers;
using EsigGestãoDeTarefasApp.Models;
using Task = EsigGestãoDeTarefasApp.Models.Task;

namespace EsigGestãoDeTarefasApp.Data
{
    public class Seed
    {
        private readonly DataContext _dataContext;
        private readonly AuthHelpers _authHelpers;

        public Seed(DataContext context, AuthHelpers authHelpers)
        {
            _dataContext = context;
            _authHelpers = authHelpers;
        }

        public void SeedDataContext()
        {
            if (_dataContext.Employees.Any() || _dataContext.Tasks.Any())
            {
                return;
            }

            // Criação dos funcionários
            var employees = new List<Employee>
            {
                new Employee
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    Password = _authHelpers.HashPassword("password123")
                },
                new Employee
                {
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@example.com",
                    Password = _authHelpers.HashPassword("password456")
                },
                new Employee
                {
                    FirstName = "Alice",
                    LastName = "Johnson",
                    Email = "alice.johnson@example.com",
                    Password = _authHelpers.HashPassword("password789")
                }
            };

            // Adicionando os funcionários ao contexto
            _dataContext.Employees.AddRange(employees);
            _dataContext.SaveChanges(); // Salvar os funcionários para obter os IDs

            // Criação das tarefas
            var tasks = new List<Task>
            {
                new Task
                {
                    Title = "Develop login system",
                    Description = "Implement authentication and authorization logic",
                    Priority = "High",
                    Status = StatusEnum.Pending,
                    Deadline = new DateTime(2024, 12, 31),
                    EmployeeId = employees[0].Id // Atribuindo a tarefa ao funcionário John
                },
                new Task
                {
                    Title = "Create database schema",
                    Description = "Design the database schema for the application",
                    Priority = "Medium",
                    Status = StatusEnum.Completed,
                    Deadline = new DateTime(2024, 11, 15),
                    EmployeeId = employees[1].Id // Atribuindo a tarefa à funcionária Jane
                },
                new Task
                {
                    Title = "Test API endpoints",
                    Description = "Write unit tests for all API endpoints",
                    Priority = "Low",
                    Status = StatusEnum.InProgress,
                    Deadline = new DateTime(2024, 12, 1),
                    EmployeeId = employees[2].Id // Atribuindo a tarefa à funcionária Alice
                }
            };

            // Adicionando as tarefas ao contexto
            _dataContext.Tasks.AddRange(tasks);
            _dataContext.SaveChanges();
        }
    }
}
