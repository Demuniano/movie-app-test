using System;
using System.Windows;
using System.Windows.Controls;
using MovieApp.WPF.ViewModels;

namespace MovieApp.WPF.Views
{
    public partial class RegisterView : UserControl
    {
        public event EventHandler? NavigateToLogin;
        public event EventHandler? RegisterCompleted;

        public RegisterView()
        {
            InitializeComponent();
            DataContext = new RegisterViewModel();
        }

        private async void OnRegisterClicked(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as RegisterViewModel;
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
                var result = await viewModel.RegisterAsync(password);
                if (result.Success)
                {
                    MessageBox.Show("Cuenta creada con éxito.");
                    RegisterCompleted?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    MessageBox.Show($"Error al registrar: {result.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado:\n" + ex.Message);
            }
        }

        private void OnBackToLoginClicked(object sender, RoutedEventArgs e)
        {
            NavigateToLogin?.Invoke(this, EventArgs.Empty);
        }
    }
}
