using CinePlex.models;
using CinePlex.Models;

namespace CinePlex.Repositories
{
    public class userRepository: IuserRepository
    {
        private readonly MovieContext _context;

        public userRepository(MovieContext context)
        {
            _context = context;
        }
        public Result<user> GetByusername(string username)
        {
            try
            {
                var entity = _context.Users.FirstOrDefault(x => x.UserName == username);
                if (entity == null)
                {
                    return new Result<user>
                    {
                        IsSuccess = true,
                        NotFound = true,
                        Data = null
                    };
                }
                return new Result<user>
                {
                    IsSuccess = true,
                    Data = entity
                };
            }
            catch (Exception ex)
            {
                return new Result<user>
                {
                    IsSuccess = false,
                    Data = null,
                    Message = ex.Message
                };
            }
        }
    }
}
