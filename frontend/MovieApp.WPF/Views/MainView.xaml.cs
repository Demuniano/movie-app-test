using System.Windows;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MovieApp.WPF.ViewModels;
using MovieApp.WPF.Models;
using MovieApp.WPF.Helpers;
using MovieApp.WPF.Services;

namespace MovieApp.WPF.Views
{
    public partial class MainView : UserControl
    {
        public event EventHandler? NavigateToLogin;
        public event EventHandler? NavigateToRegister;

        public MainView()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void OnLoginClicked(object sender, RoutedEventArgs e)
        {
            NavigateToLogin?.Invoke(this, EventArgs.Empty);
        }

        private void OnRegisterClicked(object sender, RoutedEventArgs e)
        {
            NavigateToRegister?.Invoke(this, EventArgs.Empty);
        }
        private void OnFavoriteClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is MovieModel movie)
            {
                if (!AuthSession.IsAuthenticated)
                {
                    (Window.GetWindow(this) as MainWindow)?.ShowLogin();
                    return;
                }

                movie.IsFavorite = !movie.IsFavorite;

                _ = movie.IsFavorite
                    ? MovieService.AddToFavoritesAsync(movie)
                    : MovieService.RemoveFromFavoritesAsync(movie.ImdbID);
            }
        }

    }
}
