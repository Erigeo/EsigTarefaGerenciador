using System;
using EsigGestãoDeTarefasApp.Enums;

namespace EsigGestãoDeTarefasApp.Models
{
	public class Employee
	{
		public int Id { get; set; }
        public   string? FirstName { get; set; }
        public  string? LastName { get; set; }
        public   string? Email { get; set; }
        public  string? Password { get; set; }
        public  RoleEnum? Role { get; set; }
        public ICollection<Task> Tasks { get; set; } = new List<Task>();


        public Employee()
		{
				
		}
    }
}

