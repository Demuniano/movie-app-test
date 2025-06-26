using System;
using System.Windows;
using System.Windows.Controls;
using MovieApp.WPF.ViewModels;

namespace MovieApp.WPF.Views
{
    public partial class LoginView : UserControl
    {
        public event EventHandler? LoginSucceeded;
        public event EventHandler? NavigateToRegister;
        public event EventHandler? ContinueAsGuest;

        public LoginView()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();
        }

        private async void OnLoginClicked(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as LoginViewModel;
            if (viewModel == null)
            {
                MessageBox.Show("Error interno: ViewModel no disponible.");
                return;
            }

            string password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(viewModel.Email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return;
            }

            try
            {
                var result = await viewModel.LoginAsync(password);
                if (result.Success)
                {
                    LoginSucceeded?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    MessageBox.Show("Credenciales incorrectas");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado:\n" + ex.Message);
            }
        }



        private void OnRegisterClicked(object sender, RoutedEventArgs e)
        {
            NavigateToRegister?.Invoke(this, EventArgs.Empty);
        }

        private void OnContinueAsGuestClicked(object sender, RoutedEventArgs e)
        {
            ContinueAsGuest?.Invoke(this, EventArgs.Empty);
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordPlaceholder.Visibility = string.IsNullOrEmpty(PasswordBox.Password)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }
    }
}
