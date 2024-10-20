using System;
using EsigGestãoDeTarefasApp.Dtos;
using EsigGestãoDeTarefasApp.Helpers;
using EsigGestãoDeTarefasApp.Interfaces;
using EsigGestãoDeTarefasApp.Models;
using Microsoft.AspNetCore.Mvc;
using EsigGestãoDeTarefasApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Swashbuckle.AspNetCore.Annotations;

namespace EsigGestãoDeTarefasApp.Controllers
{



    [Authorize]
    [Route("api/employees")]
	[ApiController]
	public class EmployeeController : Controller

	{
        private readonly IEmployeeRepository _employeeRepository;
        private readonly AuthService _authService;
        private readonly AuthHelpers _authHelpers;
        private readonly EmployeeService _employeeService;
        public EmployeeController(IEmployeeRepository employeeRepository, EmployeeService employeeService  , AuthService auth, AuthHelpers authHelpers
            )
		{
            _employeeRepository = employeeRepository;
            _authService = auth;
            _authHelpers = authHelpers;
            _employeeService = employeeService;
		}


        


        // GET: api/employees
        [HttpGet]
        [SwaggerOperation(Summary = "Recebe todos os employees, JWT necessário")]
        public IActionResult GetEmployees()
        {


            var employees = _employeeService.GetAllEmployees();
            
            if (employees == null || !employees.Any())
                return NotFound("No employees found");

            return Ok(employees);
        }

        // GET: api/employees/{id}
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Recebe employee por id")]
        public IActionResult GetEmployeeById(int id)
        {
            var employee = _employeeRepository.GetEmployeeById(id);
            if (employee == null)
                return NotFound($"No employee with ID {id} found");

            return Ok(employee);
        }

       

        // POST: api/employees
        [HttpPost]
        [SwaggerOperation(Summary = "Cadastra um novo employee")]
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
        [SwaggerOperation(Summary = "Realizar a alteração do Employee")]
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
        [SwaggerOperation(Summary = "Remove o Employee pelo Id")]
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

