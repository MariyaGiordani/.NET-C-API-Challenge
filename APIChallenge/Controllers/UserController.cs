using APIChallenge.Models;
using APIChallenge.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace APIChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ISecurityRepository _securityRepository;

        public UserController(IUserRepository userRepository, ISecurityRepository securityRepository)
        {
            _userRepository = userRepository;
            _securityRepository = securityRepository;
        }


        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            string message = "";

            if (!user.UserIsValid(ref message))
            {
                Response response = new Response
                {
                    Mensagem = $"It wasn't possible to register the user, because it is missing {message}!",
                    Sucesso = false
                };

                return BadRequest(response);
            }

            try
            {
                User _user = _userRepository.FindByUser(user);

                if (_user == null)
                {
                    _userRepository.Add(user);

                    Response response = new Response
                    {
                        Mensagem = "User register with success!",
                        Sucesso = true
                    };

                    return StatusCode(201, response);
                }
                else
                {
                    Response response = new Response
                    {
                        Mensagem = "User already exist in the sistem!",
                        Sucesso = false
                    };

                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                Response response = new Response
                {
                    Mensagem = $"Error during registration of the user. Reason: {e.InnerException}.",
                    Sucesso = false
                };

                return BadRequest(response);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] User user)
        {
            if (user == null || user.Id == id)
            {
                return BadRequest();
            }

            string message = "";

            if (!user.UserIsValid(ref message))
            {
                Response response = new Response
                {
                    Mensagem = $"It wasn't possible to update the user, because it is missing {message}!",
                    Sucesso = false
                };

                return BadRequest(response);
            }

            User _user = _userRepository.Find(id);

            if (_user == null)
            {
                Response response = new Response
                {
                    Mensagem = "The user was not found! Please, check the information.",
                    Sucesso = false
                };
                return NotFound(response);
            }

            try
            {
                _user.Email = user.Email;
                _user.Password = user.Password;

                _userRepository.Update(_user);

                Response response = new Response
                {
                    Mensagem = "User updated with success!",
                    Sucesso = true
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                Response response = new Response
                {
                    Mensagem = $"Error during registration of the user. Reason: {e.InnerException}.",
                    Sucesso = false
                };

                return StatusCode(500, response);
            }
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] User user)
        {

            long idUser = _userRepository.FindByUserLong(user);

            if (idUser == 0)
            {
                Response response = new Response
                {
                    Mensagem = "Invalid user!",
                    Sucesso = false
                };

                return Unauthorized(response);
            }
            else
            {
                byte[] salt = _securityRepository.Find(idUser);
                string pass = user.HashString(user.Password, salt);

                User User = _userRepository.Login(user.Email, pass);

                if (User != null)
                {
                    Response retorno = new Response
                    {
                        Mensagem = "Login was successful!",
                        Sucesso = true,
                        Objeto = User
                    };

                    var serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };

                    string json = JsonConvert.SerializeObject(retorno, Formatting.Indented, serializerSettings);

                    return Content(json, "application/json");
                }
                else
                {
                    Response response = new Response
                    {
                        Mensagem = "Wrong password!",
                        Sucesso = false
                    };

                    return Unauthorized(response);
                }
            }
        }
    }
}
