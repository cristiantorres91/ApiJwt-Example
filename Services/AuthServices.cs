using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using ApiJwt.Models;
using Microsoft.Extensions.Configuration;
using System.Text;
using ApiJwt.Exceptions;
using System.Collections.Generic;

namespace ApiJwt.Services
{
    public class AuthServices : IAuthServices
    {
        private IConfiguration _configuration;
        private List<User> users;

        public AuthServices(IConfiguration configuration)
        {
            _configuration = configuration;

            users = new List<User>
            {
                new User {Id = 1, UserName = "user", Password = "123", IdRol = new UserRol{Id = 1, NameRol = "user"}},
                new User {Id = 2, UserName = "admin", Password = "123", IdRol = new UserRol{Id = 2, NameRol = "admin"}},
            };
        }
        public string Login(User user)
        {
            var u = users.Find(x => x.UserName == user.UserName && x.Password == user.Password);

             if (u == null)
                throw new UserNotFoundException("Incorrect credentials.");
            var jwt = GenerateJwt(u);
            return jwt ; 
        }
        private string GenerateJwt(User user)
        {
            //create claim
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(ClaimTypes.Role,user.IdRol.Id.ToString()),
                new Claim("Rol",user.IdRol.NameRol.ToString()),
                new Claim("IdUser",user.Id.ToString()),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer: "http://localhost:5000", audience: "http://localhost:5000", claims, expires: DateTime.Now.AddDays(1), signingCredentials: signIn);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}