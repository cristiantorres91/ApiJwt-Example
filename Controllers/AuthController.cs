
using System.ComponentModel.Design.Serialization;
using System;
using ApiJwt.Exceptions;
using ApiJwt.Helpers;
using ApiJwt.Models;
using ApiJwt.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiJwt.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _auth;
        public AuthController(IAuthServices auth)
        {
            _auth = auth;
        }

        [HttpPost]
        public IActionResult Login([FromBody] User user)
        {
            try
            {
                var jwt = _auth.Login(user);
                return Ok(jwt);
            }
            catch (UserNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpGet]
        [AuthorizeRoles(Rol.Admin)]
        public IActionResult AdminRol()
        {
            return Ok("Welcome Admin!");
        }
        [HttpGet]
        [AuthorizeRoles(Rol.User)]
        public IActionResult UserRol()
        {
            return Ok("Welcome User!");
        }
        [HttpGet]
        [AuthorizeRoles(Rol.User, Rol.Admin)]
        public IActionResult AllRol()
        {
            return Ok("Welcome User or Admin!");
        }
    }
}
