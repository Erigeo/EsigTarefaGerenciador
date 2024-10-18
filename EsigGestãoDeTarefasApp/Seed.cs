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


            var tasks = new List<Models.Task>
                {
                new Task{
                    Title = "Develop login system",
                    Description = "Implement authentication and authorization logic",
                    Priority = "High",
                    Status = StatusEnum.Pending,
                    Deadline = new DateTime(2024, 12, 31) },
                new Task{
                    Title = "Create database schema",
                    Description = "Design the database schema for the application",
                    Priority = "Medium",
                    Status = StatusEnum.Completed,
                    Deadline = new DateTime(2024, 11, 15) },
                new Task{
                    Title = "Test API endpoints",
                    Description = "Write unit tests for all API endpoints",
                    Priority = "Low",
                    Status = StatusEnum.InProgress,
                    Deadline = new DateTime(2024, 12, 1) },

                
                
                };


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

           
            _dataContext.Employees.AddRange(employees);
            _dataContext.Tasks.AddRange(tasks);
            _dataContext.SaveChanges(); 

            
            var employeeTasks = new List<EmployeeTask>
            {
                new EmployeeTask
                {
                    EmployeeId = employees[0].Id, 
                    TaskId = tasks[0].Id,         
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

            
            _dataContext.EmployeeTasks.AddRange(employeeTasks);
            _dataContext.SaveChanges();
        }
    }
}
