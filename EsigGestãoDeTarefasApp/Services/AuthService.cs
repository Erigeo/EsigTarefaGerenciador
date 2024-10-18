using System;
using EsigGestãoDeTarefasApp.Dtos;
using EsigGestãoDeTarefasApp.Helpers;
using EsigGestãoDeTarefasApp.Interfaces;
using EsigGestãoDeTarefasApp.Models;

namespace EsigGestãoDeTarefasApp.Services
{
	public class AuthService
	{


        private readonly IEmployeeRepository _employeeRepository;
        private readonly AuthHelpers _authHelpers; // Adiciona a instância de AuthHelpers

        public AuthService(IEmployeeRepository employeeRepository, AuthHelpers authHelpers)
        {
            _employeeRepository = employeeRepository;
            _authHelpers = authHelpers; // Inicializa a instância de AuthHelpers
        }


        public Employee? AuthenticateUser(LoginDto loginDto)
        {


            var user = _employeeRepository.GetEmployeeByEmail(loginDto.Email);


            if (user == null || !_authHelpers.VerifyPassword(loginDto.Password, user.Password))
            {
                return null;
            }

            return user;
        }
    }
}

