using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EsigGestãoDeTarefasApp.Models;
using EsigGestãoDeTarefasApp.Dtos;

namespace EsigGestãoDeTarefasApp.Helpers
{
	public class AuthHelpers


	{

        public AuthHelpers()
        {
            // Parameterless constructor
        }

        private readonly IConfiguration _configuration; 

        
        public AuthHelpers(IConfiguration configuration)
        {
            _configuration = configuration; 
        }

        public string GenerateJwtToken(Employee user)
        {

            var jwtKey = _configuration["Jwt:JWT_Secret"];


            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new ArgumentNullException(nameof(jwtKey), "A chave JWT não pode ser nula ou vazia.");
            }


            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),

        new Claim("id", user.Id.ToString()),
        new Claim("role", user.Role.ToString()),


    };



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var tokenDescriptor = new SecurityTokenDescriptor {
                Issuer = _configuration["Jwt:Issuer"],
                Audience =_configuration["Jwt:Audience"],
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = creds
                };

            var tokenObject = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);
            string token = new JwtSecurityTokenHandler().WriteToken(tokenObject);
            return token;
        }




        public bool VerifyPassword(string password, string storedHash)
        {
           
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }


    }
}

