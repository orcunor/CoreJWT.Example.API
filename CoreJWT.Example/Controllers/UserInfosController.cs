using CoreJWT.Example.IServices;
using CoreJWT.Example.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreJWT.Example.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfosController : ControllerBase
    {
        private readonly IUserInfoService _userInfoService;

        public UserInfosController(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")] // api/UserInfos/authenticate
        public IActionResult Authenticate([FromBody] AuthenticationModel authenticationModel)
        {
            var user = _userInfoService.Authenticate(authenticationModel.UserName, authenticationModel.Password);
            if (user == null)
            {
                return BadRequest(new { message = "Username and password incorrect" }) ; ;
            }

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("signup")] // api/UserInfos/authenticate
        public IActionResult SignUp([FromBody] SignUpUserModel userInfo)
        {
            var newUser = new UserInfo
            {
                UserName = userInfo.UserName,
                Email = userInfo.Email,
                Password = userInfo.Password,
                ConfirmPassword = userInfo.ConfirmPassword,
                FullName = userInfo.FullName
            };

            _userInfoService.SignUp(newUser);

            return Ok(userInfo);
        }



        // GET: api/<UserInfosController>
        [HttpGet]
        
        public IActionResult Get()
        {
            var users = _userInfoService.GetAll();
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }

        // GET api/<UserInfosController>/5
        [HttpGet("{id}")]
        [Obsolete]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserInfosController>
        [HttpPost]
        [Obsolete]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserInfosController>/5
        [HttpPut("{id}")]
        [Obsolete]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserInfosController>/5
        [HttpDelete("{id}")]
        [Obsolete]
        public void Delete(int id)
        {
        }
    }
}
