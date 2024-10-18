using System;
using EsigGestãoDeTarefasApp.Enums;

namespace EsigGestãoDeTarefasApp.Dtos
{
	public class EmployeeDto
	{
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required RoleEnum Role { get; set; }
    }
}

