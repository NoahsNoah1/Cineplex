using CinePlex.Models;
using CinePlex.Repositories;

namespace CinePlex.Services
{
    public class ticketService: IticketService
    {
        private readonly IGenericRepository<show> _repo;
        public ticketService(IGenericRepository<show> repo)
        {
            _repo = repo;
        }
        public bool TicketAvailable(ticket ticket) { 
            var show= _repo.GetById(ticket.showId);
            if (show.Data.TicketsRemaining < ticket.NoOfTickets)
                return false;
            show.Data.TicketsRemaining=show.Data.TicketsRemaining-ticket.NoOfTickets;
            _repo.Update(show.Data);
            return true;
        }
    }
}
