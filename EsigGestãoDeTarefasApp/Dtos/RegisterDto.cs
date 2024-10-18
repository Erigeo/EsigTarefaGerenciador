using System;
using EsigGestãoDeTarefasApp.Enums;

namespace EsigGestãoDeTarefasApp.Dtos
{
	public class RegisterDto
	{


        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required RoleEnum Role { get; set; }
        public required string Password { get; set; }


    }
}

