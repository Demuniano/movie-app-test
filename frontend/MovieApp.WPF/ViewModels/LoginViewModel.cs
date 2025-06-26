using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using MovieApp.WPF.Services;
using MovieApp.WPF.Helpers;
using MovieApp.WPF.Models;

namespace MovieApp.WPF.ViewModels;
public class LoginViewModel : INotifyPropertyChanged
{
    private string _email = string.Empty;
    private string _password = string.Empty;

    public string Email
    {
        get => _email;
        set { _email = value; OnPropertyChanged(); }
    }

    public string Password
    {
        get => _password;
        set { _password = value; OnPropertyChanged(); }
    }

    public ICommand LoginCommand => new RelayCommand(async () => await DoLogin(), () => !IsBusy);

    private async Task DoLogin()
    {
        if (IsBusy) return;

        IsBusy = true;

        try
        {
            var result = await AuthService.LoginAsync(Email, Password);
            if (result.Success)
            {
                MessageBox.Show("Inicio de sesión exitoso");
            }
            else
            {
                MessageBox.Show("Credenciales incorrectas");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ocurrió un error:\n" + ex.Message);
        }
        finally
        {
            IsBusy = false;
            (LoginCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }
    }


    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    public async Task<LoginResult> LoginAsync(string plainPassword)
    {
        Password = plainPassword;
        return await AuthService.LoginAsync(Email, Password);
    }

    private bool _isBusy = false;
    public bool IsBusy
    {
        get => _isBusy;
        set
        {
            _isBusy = value;
            OnPropertyChanged();
        }
    }


}
