using System;
using EsigGestãoDeTarefasApp.Dtos;
using EsigGestãoDeTarefasApp.Helpers;
using EsigGestãoDeTarefasApp.Interfaces;
using EsigGestãoDeTarefasApp.Models;
using EsigGestãoDeTarefasApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace EsigGestãoDeTarefasApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
       

        private readonly IEmployeeRepository _employeeRepository;
        private readonly AuthService _authService;
        private readonly AuthHelpers _authHelpers;
        public AuthController(IEmployeeRepository employeeRepository, AuthService auth, AuthHelpers authHelpers
            )
        {
            _employeeRepository = employeeRepository;
            _authService = auth;
            _authHelpers = authHelpers;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {

            var user = _authService.AuthenticateUser(loginDto);

            if (user == null)
                return Unauthorized();

            var token = _authHelpers.GenerateJwtToken(user);
            return Ok(new { Token = token });
        }


        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto registerDto)
        {
            if (registerDto == null)
                return BadRequest("Register object is null");

            var existingUser = _employeeRepository.GetEmployeeByEmail(registerDto.Email);
            if (existingUser != null)
                return Conflict("Email already in use");


            var hashedPassword = _authHelpers.HashPassword(registerDto.Password);


            var newUser = new Employee
            {
                Email = registerDto.Email,
                Password = hashedPassword,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Role = registerDto.Role

            };

            if (!_employeeRepository.CreateEmployee(newUser))
                return StatusCode(500, "An error occurred while creating the user");

            return StatusCode(201, "Employee created successfully.");
        }
    }
}

