using System;
using ApiTasksTest.tests;
using EsigGestãoDeTarefasApp.Controllers;
using EsigGestãoDeTarefasApp.Interfaces;
using EsigGestãoDeTarefasApp.Models;
using EsigGestãoDeTarefasApp.Enums;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;
namespace ApiTasksTest.tests
{
	public class EmployeeControllerTests
	{
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;
        private readonly EmployeeController _controller;

        public EmployeeControllerTests()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            _controller = new EmployeeController(_mockEmployeeRepository.Object, null, null);
        }

        [Fact]
        public void GetEmployees_ReturnsOkResult_WhenEmployeesExist()
        {
            // Arrange
            var employees = new List<Employee>
            {
                new Employee
                {
                  Id = 1,
                  FirstName = "John",
                  LastName = "Doe", // Adicionando LastName
                  Email = "john.doe@example.com", // Adicionando Email
                  Password = "securepassword", // Adicionando Password
                  Role = RoleEnum.Employee // Substitua por um valor válido de RoleEnum
                }   
            };
            _mockEmployeeRepository.Setup(repo => repo.GetEmployees()).Returns(employees);

            // Act
            var result = _controller.GetEmployees();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(employees, okResult.Value);
        }
    }
}

