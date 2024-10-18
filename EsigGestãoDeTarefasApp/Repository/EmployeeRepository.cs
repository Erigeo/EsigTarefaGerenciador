using System;
using System.Xml.Linq;
using EsigGestãoDeTarefasApp.Data;
using EsigGestãoDeTarefasApp.Interfaces;
using EsigGestãoDeTarefasApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EsigGestãoDeTarefasApp.Repository
{
    public class EmployeeRepository : IEmployeeRepository
	{
		private readonly DataContext _context;
		public EmployeeRepository(DataContext context)
		{
			_context = context;
		}


		public bool CreateEmployee(Employee employee)
		{
            _context.Employees.Add(employee);
            return Save();

        }

       

        public bool DeleteEmployee(Employee employee)
        {
            _context.Employees.Remove(employee);
            return Save();
        }

        public Employee? GetEmployeeById(int id)
        {
            if (EmployeeExist(id))
            {
                return _context.Employees
                           .FirstOrDefault(e => e.Id == id);
            }
            return null;

        }

        public Employee? GetEmployeeByName(string fname)
        {

            return _context.Employees
        .FirstOrDefault(e => e.FirstName == fname);
        }

        public ICollection<Employee> GetEmployees()
        {
            return _context.Employees.ToList();
        }

        public bool EmployeeExist(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool UpdateEmployee(Employee employee)
        {
            _context.Employees.Update(employee);
            return Save();
        }

        public Employee? GetEmployeeByEmail(string email)
        {
            return _context.Employees
        .FirstOrDefault(e => e.Email  == email);
        }
    }
}

