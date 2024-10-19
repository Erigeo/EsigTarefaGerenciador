using System;
using EsigGestãoDeTarefasApp.Dtos;
using EsigGestãoDeTarefasApp.Helpers;
using EsigGestãoDeTarefasApp.Interfaces;
using EsigGestãoDeTarefasApp.Models;
using Microsoft.AspNetCore.Mvc;
using EsigGestãoDeTarefasApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EsigGestãoDeTarefasApp.Controllers
{



    [Authorize]
    [Route("api/employees/")]
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

