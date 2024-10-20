using System;
using EsigGestãoDeTarefasApp.Enums;

namespace EsigGestãoDeTarefasApp.Dtos
{
	public class EmployeeDtoResponse
	{
		public EmployeeDtoResponse()
		{

		}
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required RoleEnum Role { get; set; }
        public ICollection<TaskResponseDto> Tasks { get; set; } = new List<TaskResponseDto>();
    }
}

