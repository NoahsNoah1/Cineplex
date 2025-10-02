namespace CinePlex.Models
{
	public class show
	{
		public int Id { get; set; }
		public string MovieName { get; set; }
		public int MovieId { get; set; }
		public int TheaterId { get; set; }
		public string TheaterName { get; set; }
		public int Price { get; set; }
		public int TicketsRemaining { get; set; }
		public DateTime showTiming { get; set; }
	}
}