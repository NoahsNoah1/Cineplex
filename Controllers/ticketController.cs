using CinePlex.models;
using CinePlex.Models;
using CinePlex.Repositories;
using CinePlex.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinePlex.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ticketController : ControllerBase
    {
        private readonly IGenericRepository<ticket> _repo;
        private readonly IticketService _service;

        public ticketController(IGenericRepository<ticket> repo, IticketService service)
        {
            _repo = repo;
            _service = service;
        }
        [Authorize(Roles = "User")]
        [HttpPost("Addticket")]
        public ActionResult<ticket> Addticket(ticket ticket)
        {
            if (!(_service.TicketAvailable(ticket)))
                return BadRequest("Tickets Not Available!");
            var result = _repo.Add(ticket);
            if (result.IsSuccess)
                return Ok(result.Data);
            return BadRequest(result.Data);
        }
        [HttpGet("GetAllticket")]
        public ActionResult<List<ticket>> GetAll()
        {
            var result = _repo.GetAll();
            if (result.IsSuccess)
                return Ok(result.Data);
            return BadRequest(result.Data);
        }
        [Authorize(Roles = "User")]
        [HttpGet("GetticketById")]
        public ActionResult<ticket> GetticketById(int id)
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
        [HttpPut("Updateticket")]
        public ActionResult<ticket> Updateticket(ticket ticket)
        {

            var result = _repo.Update(ticket);
            if (result.IsSuccess)
                return Ok(result.Data);
            return BadRequest(result.Data);
        }
        [Authorize(Roles = "User")]
        [HttpPost("Deleteticket")]
        public ActionResult<ticket> Deleteticket(int id)
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
