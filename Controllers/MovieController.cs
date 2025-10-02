using CinePlex.models;
using CinePlex.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinePlex.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(Roles ="Admin")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IGenericRepository<Movie> _repo;

        public MovieController(IGenericRepository<Movie> repo)
        {
            _repo = repo;
        }
        [HttpPost("AddMovie")]
        public ActionResult<Movie> AddMovie(Movie Movie)
        {
            var result = _repo.Add(Movie);
            if (result.IsSuccess)
                return Ok(result.Data);
            return BadRequest(result.Data);
        }
        [HttpGet("GetAllMovies")]
        public ActionResult<List<Movie>> GetAll()
        {
            var result = _repo.GetAll();
            if (result.IsSuccess)
                return Ok(result.Data);
            return BadRequest(result.Data);
        }
        [HttpGet("GetMovieById")]
        public ActionResult<Movie> GetMovieById(int id)
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
        [HttpPut("UpdateMovie")]
        public ActionResult<Movie> UpdateMovie(Movie Movie)
        {
            var result = _repo.Update(Movie);
            if (result.IsSuccess)
                return Ok(result.Data);
            return BadRequest(result.Data);
        }
        [HttpPost("DeleteMovie")]
        public ActionResult<Movie> DeleteMovie(int id)
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
