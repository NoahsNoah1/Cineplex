using CinePlex.Dtos;
using CinePlex.models;
using CinePlex.Repositories;
using CinePlex.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinePlex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        private readonly IGenericRepository<user> _repo;
        private readonly IAuthService _service;

        public userController(IGenericRepository<user> repo, IAuthService service)
        {
            _repo = repo;
            _service = service;
        }
        [HttpPost("Adduser")]
        public ActionResult<user> Adduser(user user)
        {
            if (_service.UserNameExists(user.UserName))
                return BadRequest("User Name already Exists!");
            var result = _repo.Add(user);
            if (result.IsSuccess)
                return Ok(result.Data);
            return BadRequest(result.Data);
        }
        [HttpPost("Login")]
        public ActionResult Login(LoginDto loginDto) 
        {
            if (!(_service.UserNameExists(loginDto.Username)))
                return BadRequest("User Not Found");
            if (_service.AuthenticatedUser(loginDto))
            {
                var token=_service.GenerateJwtToken(loginDto);
                return Ok(token);
            }
            return BadRequest("Wrong Password");
        }
        [HttpGet("GetAlluser")]
        /*public ActionResult<List<user>> GetAll()
        {
            var result = _repo.GetAll();
            if (result.IsSuccess)
                return Ok(result.Data);
            return BadRequest(result.Data);
        }
        */
        [Authorize(Roles = "User")]
        [HttpGet("GetuserById")]
        public ActionResult<user> GetuserById(int id)
        {
            var result = _repo.GetById(id);
            if (result.IsSuccess)
            {
                if (result.NotFound)
                    return NotFound(result);
                return Ok(result.Data);
            }
            return BadRequest(result.Data);
        }
        [Authorize(Roles = "User")]
        [HttpPut("Updateuser")]
        public ActionResult<user> Updateuser(user user)
        {
            var result = _repo.Update(user);
            if (result.IsSuccess)
                return Ok(result.Data);
            return BadRequest(result.Data);
        }
        [Authorize(Roles = "User")]
        [HttpPost("Deleteuser")]
        public ActionResult<user> Deleteuser(int id)
        {
            var result = _repo.Delete(id);
            if (result.IsSuccess)
            {
                if (result.NotFound)
                    return NotFound(result.Data);
                return Ok(result.Data);
            }
            return BadRequest(result.Data);
        }
    }
}
