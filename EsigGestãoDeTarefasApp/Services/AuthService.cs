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

        public bool Register(RegisterDto registerDto)
        {
            

            var existingUser = _employeeRepository.GetEmployeeByEmail(registerDto.Email);
            if (existingUser != null)
                return false;


            var hashedPassword = _authHelpers.HashPassword(registerDto.Password);


            var newUser = new Employee
            {
                Email = registerDto.Email,
                Password = hashedPassword,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Role = registerDto.Role

            };

            _employeeRepository.CreateEmployee(newUser);
            return true;
                
        }
    }
}

