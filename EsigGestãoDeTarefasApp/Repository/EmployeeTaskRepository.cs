using System;
using EsigGestãoDeTarefasApp.Data;
using EsigGestãoDeTarefasApp.Interfaces;
using EsigGestãoDeTarefasApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EsigGestãoDeTarefasApp.Repository
{

	public class EmployeeTaskRepository : IEmployeeTaskRepository
	{
        private readonly DataContext _context;
       

        public EmployeeTaskRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateEmployeeTask(EmployeeTask employee)
        {
            _context.EmployeeTasks.Add(employee);
            return Save();

        }

        public bool DeleteEmployeeTask(EmployeeTask employee)
        {
            _context.EmployeeTasks.Remove(employee);
            return Save();
        }

        public ICollection<EmployeeTask> GetAllEmployeeTask()
        {
            return _context.EmployeeTasks
                   .Include(et => et.Employee)  // Inclui a entidade Employee
                   .Include(et => et.Task)      // Inclui a entidade Task
                   .ToList();
        }

        public ICollection<EmployeeTask> GetEmployeeTaskByEmployeeId(int id)
        {
            return _context.EmployeeTasks
                   .Include(et => et.Employee)  // Inclui a entidade Employee
                   .Include(et => et.Task)      // Inclui a entidade Task
                   .Where(i => i.EmployeeId == id)
                   .ToList();
        }

        public ICollection<EmployeeTask> GetEmployeeTaskByTaskId(int id)
        {
            return _context.EmployeeTasks
                   .Include(et => et.Employee)  // Inclui a entidade Employee
                   .Include(et => et.Task)      // Inclui a entidade Task
                   .Where(i => i.TaskId == id)
                   .ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

 

        public EmployeeTask? GetEmployeeTaskByIds(int taskId, int employeeId)
        {
            return _context.EmployeeTasks.FirstOrDefault(i => i.TaskId == taskId && i.EmployeeId == employeeId);

        }

        public bool UpdateEmployeeTask(EmployeeTask employee)
        {
            _context.Update(employee);
            return Save();
        }
    }
}

