using System;
using System.Dynamic;
using EsigGestãoDeTarefasApp.Controllers;
using EsigGestãoDeTarefasApp.Dtos;
using EsigGestãoDeTarefasApp.Enums;
using EsigGestãoDeTarefasApp.Helpers;
using EsigGestãoDeTarefasApp.Interfaces;
using EsigGestãoDeTarefasApp.Models;
using EsigGestãoDeTarefasApp.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ApiTasksTest.tests
{

    public class AuthControllerTests
    {
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;
        private readonly Mock<AuthHelpers> _mockAuthHelpers;
        private readonly AuthService _authService;
        private readonly AuthController _controller;
        private readonly Mock<AuthService> _mockAuthService;

        public AuthControllerTests()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            _mockAuthHelpers = new Mock<AuthHelpers>();
            _authService = new AuthService(_mockEmployeeRepository.Object, _mockAuthHelpers.Object);
            _controller = new AuthController(_mockEmployeeRepository.Object, _authService, _mockAuthHelpers.Object);
            _mockAuthService = new Mock<AuthService>();
        }

        

        [Fact]
        public void Login_ReturnsUnauthorized_WhenUserIsNotAuthenticated()
        {
            // Arrange
            var loginDto = new LoginDto { Email = "test@example.com", Password = "wrongpassword" };
            _mockEmployeeRepository.Setup(repo => repo.GetEmployeeByEmail(loginDto.Email)).Returns((Employee)null);

            // Act
            var result = _controller.Login(loginDto);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public void Register_ReturnsConflict_WhenEmailAlreadyExists()
        {
            // Arrange
            var registerDto = new RegisterDto
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "test@example.com",
                Role = RoleEnum.Employee,
                Password = "password"
            };

            var existingUser = new Employee { Email = registerDto.Email };
            _mockEmployeeRepository.Setup(repo => repo.GetEmployeeByEmail(registerDto.Email)).Returns(existingUser);

            // Act
            var result = _controller.Register(registerDto);

            // Assert
            var conflictResult = Assert.IsType<ObjectResult>(result); // Altere para ObjectResult
            Assert.Equal(409, conflictResult.StatusCode);
        }



        [Fact]
        public void Register_ReturnsCreated_WhenUserIsRegisteredSuccessfully()
        {
            // Arrange
            var registerDto = new RegisterDto
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "test@example.com",
                Role = RoleEnum.Employee,
                Password = "password"
            };

            // Simula que não existe um usuário com o mesmo e-mail
            _mockAuthService.Setup(service => service.Register(registerDto)).Returns(true);

            // Act
            var result = _controller.Register(registerDto);

            // Assert
            var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(201, statusCodeResult.StatusCode); // Verifica se o código de status é 201
        }


    }

}

