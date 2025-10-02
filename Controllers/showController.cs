using CinePlex.models;
using CinePlex.Models;
using CinePlex.Repositories;
using CinePlex.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinePlex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class showController : ControllerBase
    {
        private readonly IGenericRepository<show> _repo;
        private readonly IshowService _service;

        public showController(IGenericRepository<show> repo, IshowService service)
        {
            _repo = repo;
            _service = service;
        }

        [HttpPost("Addshow")]
        //[Authorize(Roles = "Admin")]
        public ActionResult<show> Addshow(show show)
        {
            if (_service.ValidShow(show))
                return BadRequest("Ivalid Show");
            var result = _repo.Add(show);
            if (result.IsSuccess)
                return Ok(result.Data);
            return BadRequest(result.Data);
        }

        [HttpGet("GetAllshows")]
        //[Authorize(Roles = "User,Admin")]
        public ActionResult<List<show>> GetAll()
        {
            var result = _repo.GetAll();
            if (result.IsSuccess)
                return Ok(result.Data);
            return BadRequest(result.Data);
        }

        [HttpGet("GetshowById")]
       // [Authorize(Roles = "Admin,User")]
        public ActionResult<show> GetshowById(int id)
        {
            var result = _repo.GetById(id);
            if (result.IsSuccess)
            {
                if (result.NotFound)
                    return NotFound(result.Data);
                return Ok(result.Data);
            }
            return BadRequest(result.Data);
        }

       // [Authorize(Roles = "Admin")]
        [HttpPut("Updateshow")]
        public ActionResult<show> Updateshow(show show)
        {
            if (!(_service.ValidShow (show)))
                return BadRequest("Invalid show");

            var result = _repo.Update(show);
            if (result.IsSuccess)
                return Ok(result.Data);
            return BadRequest(result.Data);
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("Deleteshow")]
        public ActionResult<show> Deleteshow(int id)
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