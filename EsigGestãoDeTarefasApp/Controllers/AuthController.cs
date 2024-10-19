using System;
using EsigGestãoDeTarefasApp.Dtos;
using EsigGestãoDeTarefasApp.Helpers;
using EsigGestãoDeTarefasApp.Interfaces;
using EsigGestãoDeTarefasApp.Models;
using EsigGestãoDeTarefasApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace EsigGestãoDeTarefasApp.Controllers
{

    [Route("api/")]
    [ApiController]
    public class AuthController : Controller
    {
       

        private readonly IEmployeeRepository _employeeRepository;
        private readonly AuthService _authService;
        private readonly AuthHelpers _authHelpers;
        
        public AuthController(IEmployeeRepository employeeRepository, AuthService auth, AuthHelpers authHelpers
            )
        {
            _employeeRepository = employeeRepository;
            _authService = auth;
            _authHelpers = authHelpers;
        }


        
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            /// <summary>
            /// Realiza o Login, retornando um JWT token.
            /// </summary>
            /// <param name="loginDto"></param>
            /// <returns></returns>
            if (loginDto == null)
            {
                return BadRequest("Login information cannot be null.");
            }

            var user = _authService.AuthenticateUser(loginDto);

            if (user == null)
                return Unauthorized();

            var token = _authHelpers.GenerateJwtToken(user);
            return Ok(new { Token = token });
        }


        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto registerDto)


        {

            if(registerDto == null)
            {
                return BadRequest("Register information cannot be null.");
            }

            var isRegistered = _authService.Register(registerDto);

            if (!isRegistered)
            {
                return StatusCode(409, "Email already in use.");
            }
            //change this return
            return StatusCode(201, "User created successfully.");
        }
    }
}

