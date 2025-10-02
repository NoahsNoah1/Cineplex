using CinePlex.models;
using CinePlex.Models;
using CinePlex.Repositories;

namespace CinePlex.Services
{
    public class showService: IshowService
    {
        private readonly IGenericRepository<Movie> _movierepo;
        private readonly IGenericRepository<theater> _theaterrepo;
        public showService(IGenericRepository<Movie> movierepo, IGenericRepository<theater> theaterrepo)
        {
            _movierepo = movierepo;
            _theaterrepo = theaterrepo;
        }
        public bool ValidShow(show show)
        {
            var movieid = show.MovieId;
            var movie=_movierepo.GetById(movieid);
            if (movie == null)
                return false;
            var theaterid=show.TheaterId;
            var theater = _theaterrepo.GetById(theaterid);
            if (theater == null) return false;
            if (show.TicketsRemaining > theater.Data.SeatCapacity)
                return false;
            return true;
        }
    }
}
