using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ToDoList.BL.LogicInterfaces;
using ToDoList.BL.ServiceInterfaces;
using ToDoList.Models.Model.User;

namespace ToDoList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserLogic _userLogic;
        private readonly ITokenLogic _tokenLogic;
        private readonly IEmailProvider _emailProvider;

        public UserController(IUserLogic userLogic, ITokenLogic tokenLogic, IEmailProviderFabric emailProviderFabric)
        {
            _userLogic = userLogic;
            _tokenLogic = tokenLogic;
            _emailProvider = emailProviderFabric.GetEmailProvider();
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            var user = _userLogic.Authenticate(model.Email, model.Password);
            if (user == null)
            {
                return BadRequest(new {message = "Username or password is incorrect"});
            }

            return Ok(new
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = _tokenLogic.GenerateToken(user.Id)
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserModel model)
        {
            await _userLogic.Create(model);

            var subject = $"Welcome {model.FirstName} {model.LastName}.";
            var htmlMessage = $"<div>Hello {model.FirstName} {model.LastName}.</div>";
            _emailProvider.Send(model.Email, subject, htmlMessage);

            return Ok();
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var users = _userLogic.GetAll();

            return Ok(users);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userLogic.GetByIdAsync(id);

            return Ok(user);
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(Guid id, [FromBody] UpdateModel model)
        {
            _userLogic.Update(id, model);

            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            _userLogic.Delete(id);

            return Ok();
        }
    }
}