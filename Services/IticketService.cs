using CinePlex.Models;

namespace CinePlex.Services
{
    public interface IticketService
    {
        bool TicketAvailable(ticket ticket);
    }
}
