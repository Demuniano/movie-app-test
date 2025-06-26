using System;
using System.Windows;
using MovieApp.WPF.Views;
using MovieApp.WPF.Helpers;

namespace MovieApp.WPF
{
<<<<<<< feat/navigation

=======
>>>>>>> main
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ShowMain();
        }

        public void ShowLogin()
        {
            var loginView = new LoginView();
            loginView.NavigateToRegister += (_, __) => ShowRegister();
            loginView.LoginSucceeded += (_, __) => ShowUserHome();
            loginView.ContinueAsGuest += (_, __) => ShowMain();
            MainContent.Content = loginView;
        }

        private void ShowRegister()
        {
            var registerView = new RegisterView();
            registerView.NavigateToLogin += (s, e) => ShowLogin();
            registerView.RegisterCompleted += (s, e) => ShowLogin();

            MainContent.Content = registerView;
        }

        public void ShowMain()
        {
            var mainView = new MainView();
            mainView.NavigateToLogin += (_, __) => ShowLogin();
            mainView.NavigateToRegister += (_, __) => ShowRegister();
            MainContent.Content = mainView;
        }

        public void ShowUserHome()
        {
            var userHomeView = new UserHomeView();
            userHomeView.LogoutRequested += (_, __) =>
            {
                AuthSession.Clear();
                ShowMain();
            };
            userHomeView.LogoutRequested += (_, __) => ShowLogin();
            MainContent.Content = userHomeView;
        }

    }
}
