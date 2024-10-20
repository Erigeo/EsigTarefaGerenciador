using System;
using EsigGestãoDeTarefasApp.Dtos;
using EsigGestãoDeTarefasApp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EsigGestãoDeTarefasApp.Services
{
	public class EmployeeService
	{
		

        private readonly ITaskRepository _taskRepository;

        private readonly IEmployeeRepository _employeeRepository;


        public EmployeeService(ITaskRepository taskRepository, IEmployeeRepository employeeRepository
            )
        {
            _taskRepository = taskRepository;
            _employeeRepository = employeeRepository;

        }

        public IEnumerable<EmployeeDtoResponse> GetAllEmployees()
        {
            var employees = _employeeRepository.GetEmployees();


            var tasks = _taskRepository.GetAllTasks();


            return employees.Select(e => new EmployeeDtoResponse
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                Role = (Enums.RoleEnum)e.Role, // Converte o Role para o enum correto
                Tasks = (ICollection<TaskResponseDto>)tasks.Where(t => t.EmployeeId == e.Id).Select(t => new TaskResponseDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Priority = t.Priority, // Supondo que você tenha essa propriedade na Task
                    Status = t.Status, // Supondo que você tenha essa propriedade na Task
                    Deadline = t.Deadline // Supondo que você tenha essa propriedade na Task
                }).ToList()
            }).ToList();
        }


    }
}

