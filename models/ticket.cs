namespace CinePlex.Models
{
	public class ticket
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int showId { get; set; }
		public string MovieName { get; set; }
		public string TheaterName { get; set; }
		public DateTime showTiming { get; set; }
		public int NoOfTickets { get; set; }
		public string SeatNos { get; set; }
	}
}