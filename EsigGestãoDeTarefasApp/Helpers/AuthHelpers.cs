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
        new Claim("id", user.Id.ToString())

    };

            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            
            return new JwtSecurityTokenHandler().WriteToken(token);
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

