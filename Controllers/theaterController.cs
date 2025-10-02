using CinePlex.models;
using CinePlex.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinePlex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class theaterController : ControllerBase
    {
        private readonly IGenericRepository<theater> _repo;

        public theaterController(IGenericRepository<theater> repo)
        {
            _repo = repo;
        }
        [HttpPost("Addtheater")]
        public ActionResult<theater> Addtheater(theater theater)
        {
            var result = _repo.Add(theater);
            if (result.IsSuccess)
                return Ok(result.Data);
            return BadRequest(result.Data);
        }
        [HttpGet("GetAlltheater")]
        public ActionResult<List<theater>> GetAll()
        {
            var result = _repo.GetAll();
            if (result.IsSuccess)
                return Ok(result.Data);
            return BadRequest(result.Data);
        }
        [HttpGet("GettheaterById")]
        public ActionResult<theater> GettheaterById(int id)
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
        [HttpPut("Updatetheater")]
        public ActionResult<theater> Updatetheater(theater theater)
        {
            var result = _repo.Update(theater);
            if (result.IsSuccess)
                return Ok(result.Data);
            return BadRequest(result.Data);
        }
        [HttpPost("Deletetheater")]
        public ActionResult<theater> Deletetheater(int id)
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
