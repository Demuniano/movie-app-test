using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MovieApp.WPF.Models;
using MovieApp.WPF.Services;
using MovieApp.WPF.Helpers;
using System.Windows;

namespace MovieApp.WPF.ViewModels;
public class UserHomeViewModel : INotifyPropertyChanged
{
    private string _searchText = "";
    public string SearchText
    {
        get => _searchText;
        set { _searchText = value; OnPropertyChanged(); }
    }

    public ObservableCollection<MovieModel> Movies { get; set; } = new();
    public ObservableCollection<MovieModel> Favorites { get; set; } = new();

    public ICommand SearchCommand { get; }
    public ICommand AddToFavoritesCommand { get; }

    public UserHomeViewModel()
    {
        SearchCommand = new RelayCommand(async () => await SearchMoviesAsync());
        AddToFavoritesCommand = new RelayCommand<MovieModel>(AddToFavorites);
    }

    private async Task SearchMoviesAsync()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
            return;

        var results = await MovieService.SearchMoviesAsync(SearchText);
        Movies.Clear();

        foreach (var movie in results)
            Movies.Add(movie);
    }

    private void AddToFavorites(MovieModel movie)
    {
        if (!AuthSession.IsAuthenticated)
        {
            MessageBox.Show("Debes iniciar sesión para agregar favoritos.");
            return;
        }

        if (Favorites.Any(f => f.ImdbID == movie.ImdbID))
        {
            MessageBox.Show("Esta película ya está en tus favoritos.");
            return;
        }

        Favorites.Add(movie);
    }

    public async void ToggleFavorite(MovieModel movie)
    {
        if (movie == null) return;

        if (movie.IsFavorite)
        {
            bool success = await MovieService.RemoveFromFavoritesAsync(movie.ImdbID);
            if (success)
            {
                movie.IsFavorite = false;
                await RefreshFavoritesAsync();
            }
        }
        else
        {
            bool success = await MovieService.AddToFavoritesAsync(movie);
            if (success)
            {
                movie.IsFavorite = true;
                await RefreshFavoritesAsync();
            }
        }
    }


    public async Task RefreshFavoritesAsync()
    {
        var results = await MovieService.GetFavoritesAsync();
        Favorites.Clear();
        foreach (var movie in results)
        {
            movie.IsFavorite = true;
            Favorites.Add(movie);
        }

        foreach (var movie in Movies)
        {
            movie.IsFavorite = Favorites.Any(f => f.ImdbID == movie.ImdbID);
        }
    }


    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
