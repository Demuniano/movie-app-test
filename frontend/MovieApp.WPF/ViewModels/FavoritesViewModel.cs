using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MovieApp.WPF.Services;
using System.Threading.Tasks;
using MovieApp.WPF.Models;

namespace MovieApp.WPF.ViewModels;
public class FavoritesViewModel : INotifyPropertyChanged
{
    private ObservableCollection<MovieModel> _favorites = new();
    public ObservableCollection<MovieModel> Favorites
    {
        get => _favorites;
        set
        {
            _favorites = value;
            OnPropertyChanged();
        }
    }

    public async Task LoadFavoritesAsync()
    {
        var movies = await MovieService.GetFavoritesAsync();
        Favorites = new ObservableCollection<MovieModel>(movies);
    }


    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
