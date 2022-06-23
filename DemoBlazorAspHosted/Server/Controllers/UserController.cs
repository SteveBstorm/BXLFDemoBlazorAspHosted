using DemoBlazorAspHosted.Server.Models;
using DemoBlazorAspHosted.Server.Tools;
using DemoBlazorAspHosted.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoBlazorAspHosted.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class UserController : ControllerBase
    {
        private JWTManager jwt;
        private DataContext dc;

        public UserController(JWTManager jwt, DataContext dc)
        {
            this.jwt = jwt;
            this.dc = dc;
        }

        [AllowAnonymous]
        [HttpPost("auth")]
        public IActionResult Login([FromBody] LoginForm form)
        {
            if(!ModelState.IsValid) return BadRequest();

            Stagiaire s = dc.Login(form.Email, form.Password);

            string token = jwt.GenerateToken(s);

            ConnectedUser connectedUser = new ConnectedUser
            {
                Id = s.Id,
                LastName = s.LastName,
                FirstName = s.FirstName,
                Email = s.Email
            };
            connectedUser.Token = token;

            return Ok(connectedUser);
        }
        //[Authorize("isConnected")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(dc.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(dc.GetById(id));
        }

        [HttpPost]
        public IActionResult Create([FromBody] Stagiaire s)
        {
            dc.Create(s);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            dc.Delete(id);
            return Ok();
        }
    }
}
