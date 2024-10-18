using System;
using EsigGestãoDeTarefasApp.Dtos;
using EsigGestãoDeTarefasApp.Helpers;
using EsigGestãoDeTarefasApp.Interfaces;
using EsigGestãoDeTarefasApp.Models;
using Microsoft.AspNetCore.Mvc;
using EsigGestãoDeTarefasApp.Services;

namespace EsigGestãoDeTarefasApp.Controllers
{




	[Route("api/[controller]")]
	[ApiController]
	public class EmployeeController : Controller

	{
        private readonly IEmployeeRepository _employeeRepository;
        private readonly AuthService _authService;
        private readonly AuthHelpers _authHelpers;
        public EmployeeController(IEmployeeRepository employeeRepository, AuthService auth, AuthHelpers authHelpers
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
                LastName = registerDto.LastName
                                           
            };

            if (!_employeeRepository.CreateEmployee(newUser))
                return StatusCode(500, "An error occurred while creating the user");

            return CreatedAtAction(nameof(GetEmployeeById), new { id = newUser.Id }, newUser);
        }


        // GET: api/employees
        [HttpGet]
        public IActionResult GetEmployees()
        {
            var employees = _employeeRepository.GetEmployees();
            if (employees == null || !employees.Any())
                return NotFound("No employees found");

            return Ok(employees);
        }

        // GET: api/employees/{id}
        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            var employee = _employeeRepository.GetEmployeeById(id);
            if (employee == null)
                return NotFound($"No employee with ID {id} found");

            return Ok(employee);
        }

        // GET: api/employees/name/{name}
        [HttpGet("name/{name}")]
        public IActionResult GetEmployeeByName(string name)
        {
            var employee = _employeeRepository.GetEmployeeByName(name);
            if (employee == null)
                return NotFound($"No employee with name '{name}' found");

            return Ok(employee);
        }

        // POST: api/employees
        [HttpPost]
        public IActionResult CreateEmployee([FromBody] Employee employee)
        {
            if (employee == null)
                return BadRequest("Employee object is null");

            if (!_employeeRepository.CreateEmployee(employee))
                return StatusCode(500, "An error occurred while creating the employee");

            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
        }

        // PUT: api/employees/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, [FromBody] Employee employee)
        {
            if (employee == null || employee.Id != id)
                return BadRequest("Employee object is null or ID mismatch");

            if (!_employeeRepository.EmployeeExist(id))
                return NotFound($"No employee with ID {id} found");

            if (!_employeeRepository.UpdateEmployee(employee))
                return StatusCode(500, "An error occurred while updating the employee");

            return NoContent();
        }

        // DELETE: api/employees/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = _employeeRepository.GetEmployeeById(id);
            if (employee == null)
                return NotFound($"No employee with ID {id} found");

            if (!_employeeRepository.DeleteEmployee(employee))
                return StatusCode(500, "An error occurred while deleting the employee");

            return NoContent();
        }


    }
}

