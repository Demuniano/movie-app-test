namespace MovieApp.API.DTOs
{
	public class MovieSearchResponse
	{
		public List<MovieDto>? Search { get; set; }
		public string? TotalResults { get; set; }
		public string? Response { get; set; }
	}
}
