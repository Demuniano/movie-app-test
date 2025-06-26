using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MovieApp.WPF.Models;
using MovieApp.WPF.Services;
using MovieApp.WPF.Helpers;

namespace MovieApp.WPF.ViewModels;
public class MainViewModel : INotifyPropertyChanged
{
    private string _searchText = string.Empty;

    public string SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<MovieModel> Movies { get; set; } = new();

    public ICommand SearchCommand => new RelayCommand(async () =>
    {
        if (string.IsNullOrWhiteSpace(SearchText))
            return;

        var results = await MovieService.SearchMoviesAsync(SearchText);

        Movies.Clear();
        foreach (var movie in results)
        {
            Movies.Add(movie);
        }
    });

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
