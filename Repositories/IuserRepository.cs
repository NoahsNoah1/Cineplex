using CinePlex.models;

namespace CinePlex.Repositories
{
    public interface IuserRepository
    {
        Result<user> GetByusername(string username);
    }
}
