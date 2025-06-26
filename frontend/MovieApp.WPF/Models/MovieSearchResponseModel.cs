namespace MovieApp.WPF.Models;

public class MovieSearchResponseModel
{
    public List<MovieModel>? Search { get; set; }
    public string? TotalResults { get; set; }
    public string? Response { get; set; }
}
