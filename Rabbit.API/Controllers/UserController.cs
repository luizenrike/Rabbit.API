using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rabbit.API.Configurations;
using Rabbit.API.Models.DataTransferObjects;
using Rabbit.API.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Rabbit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAll();
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [TypeFilter(typeof(ConfigExceptions))]
        [HttpPost]
        public async Task<IActionResult> Insert(UserRequest request)
        {
            await _userService.InsertUser(request);
            return this.Created("User", request);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [TypeFilter(typeof(ConfigExceptions))]
        [HttpPut]
        public async Task<IActionResult> Update([Required][FromQuery]int id, UserRequest request)
        {
            await _userService.Update(id, request);
            return this.NoContent();
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [TypeFilter(typeof(ConfigExceptions))]
        [HttpDelete]
        public async Task<IActionResult> Delete([Required][FromQuery] int id)
        {
            await _userService.Delete(id);
            return this.NoContent();
        }
    }
}
