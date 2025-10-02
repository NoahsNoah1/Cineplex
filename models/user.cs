using System.ComponentModel.DataAnnotations;

namespace CinePlex.models
{
    public class user
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserType { get; set;}
        //[DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
