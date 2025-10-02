using CinePlex.models;
using Microsoft.EntityFrameworkCore;

namespace CinePlex.Models
{
	public class MovieContext : DbContext
	{
		public MovieContext(DbContextOptions<MovieContext> options) : base(options) { }
		public DbSet<Movie> Movies { get; set; }
		public DbSet<Movie> Theaters { get; set; }
		public DbSet<show> shows { get; set; }
		public DbSet<user> Users { get; set; }
		public DbSet<Movie> Tickets { get; set; }
	}
}