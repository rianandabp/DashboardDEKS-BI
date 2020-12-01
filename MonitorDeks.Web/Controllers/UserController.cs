using DashboardDeks.Services.Notification;
using DashboardDeks.Services.Program;
using DashboardDeks.Services.Status;
using DashboardDeks.Services.User;
using DashboardDeks.Web.Serialization;
using DashboardDeks.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashboardDeks.Web.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly IStatusService _statusService;

        public UserController(ILogger<UserController> logger, IUserService userService,
            IStatusService statusService)
        {
            _userService = userService;
            _logger = logger;
            _statusService = statusService;
        }

        [HttpPost("/api/user")]
        public ActionResult CreateUser([FromBody] UserModel user)
        {
            _logger.LogInformation("Adding a new User");

            var userData = UserMapper.SerializeUser(user);
            var newUser = _userService.CreateUser(userData);
            return Ok(newUser);
        }


        [HttpGet("/api/user")]
        public ActionResult GetUser()
        {
            _logger.LogInformation("Getting a users");

            if (DateTime.Today.Date.ToString("d").Substring(0, 2) == "02") _statusService.UpdateStatus();
            if (DateTime.Today.Date.ToString("d").Substring(0, 2) == "01" && _statusService.GetStatus())
            {
                Console.WriteLine("Reset status");
                _userService.ResetStat();
                _statusService.UpdateStatus();
            }

            var users = _userService.GetAllUsers();
            var userModels = users.Select(user => new UserModel
            {
                UsernameId = user.UsernameId,
                Name = user.Name,
                UserAccess = user.UserAccess,
                Password = null,
                PunctualityRating = user.PunctualityRating,
                TaskDone = user.TaskDone,
                TaskLate = user.TaskLate

            }).OrderByDescending(c => (c.TaskDone - c.TaskLate)).ToList();

            return Ok(userModels);
        }

        [HttpGet("/api/user/auth/{token}")]
        public bool Auth(string token)
        {
            _logger.LogInformation("Authenticate a users");
            string username = token.Substring(0, token.IndexOf('|'));
            string password = token.Substring(token.IndexOf('|') + 1, (token.Length - 1)-(token.IndexOf('|')));
            bool result = _userService.Authenticate(username, password);
            return result;
        }

        [HttpPatch("/api/user/{token}")]
        public void UpdateStatus(string token)
        {
            _logger.LogInformation("Update user status");
            string username = token.Substring(0, token.IndexOf('|'));
            string df = token.Substring(token.IndexOf('|') + 1, (token.Length - 1) - (token.IndexOf('|')));
            _userService.UpdateStat(username,df);
        }

        [HttpGet("/api/user/{username}")]
        public ActionResult GetByUsernameId(string username)
        {
            _logger.LogInformation("Get a user");
            var result = _userService.GetByUsername(username);
            return Ok(result);
        }

        [HttpDelete("/api/user/{username}")]
        public ActionResult DeleteUser(string username)
        {
            _logger.LogInformation("Deleting a user");
            var response = _userService.DeleteUser(username);
            return Ok(response);
        }

        [HttpPatch("/api/user")]
        public ActionResult UpdateUser([FromBody] UserModel user)
        {
            _logger.LogInformation("Update User complete...");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userData = UserMapper.SerializeUser(user);
            var response = _userService.UpdateUser(userData);
            return Ok(response);
        }
    }
}
