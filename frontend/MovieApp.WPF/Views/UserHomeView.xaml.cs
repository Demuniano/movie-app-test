using System;
using System.Windows;
using System.Windows.Controls;
using MovieApp.WPF.ViewModels;
using MovieApp.WPF.Models;
using MovieApp.WPF.Services;

namespace MovieApp.WPF.Views
{
    public partial class UserHomeView : UserControl
    {
        public event EventHandler? LogoutRequested;
        public UserHomeView()
        {
            InitializeComponent();
            DataContext = new UserHomeViewModel();
            Loaded += async (_, __) =>
            {
                if (DataContext is UserHomeViewModel viewModel)
                {
                    await viewModel.RefreshFavoritesAsync();
                }
            };

        }

        private void OnLogoutClicked(object sender, RoutedEventArgs e)
        {
            LogoutRequested?.Invoke(this, EventArgs.Empty);
        }

        private async void LoadFavorites()
        {
            if (DataContext is UserHomeViewModel viewModel)
            {
                var results = await MovieService.GetFavoritesAsync();
                viewModel.Favorites.Clear();
                foreach (var movie in results)
                {
                    viewModel.Favorites.Add(movie);
                }
            }
        }

        private void OnToggleFavoriteClicked(object sender, RoutedEventArgs e)
        {
            if (sender is Button button &&
                button.DataContext is MovieModel movie &&
                DataContext is UserHomeViewModel viewModel)
            {
                viewModel.ToggleFavorite(movie);
            }
        }
    }
}
