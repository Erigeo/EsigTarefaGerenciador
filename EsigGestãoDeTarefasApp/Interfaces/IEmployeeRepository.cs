using System;
using EsigGestãoDeTarefasApp.Models;

namespace EsigGestãoDeTarefasApp.Interfaces
{
	public interface IEmployeeRepository
	{
		ICollection<Employee> GetEmployees();
		Employee? GetEmployeeByEmail(string email);
		Employee? GetEmployeeById(int id);
		Employee? GetEmployeeByName(string fname);
		bool CreateEmployee(Employee employee);
        bool UpdateEmployee(Employee employee);
        bool DeleteEmployee(Employee employee);
		bool EmployeeExist(int id);
		bool Save();
    }
}

