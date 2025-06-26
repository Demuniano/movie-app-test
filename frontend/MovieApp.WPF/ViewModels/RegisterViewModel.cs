using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MovieApp.WPF.Helpers;
using MovieApp.WPF.Services;
using System.Windows;
using MovieApp.WPF.Models;

namespace MovieApp.WPF.ViewModels;
public class RegisterViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private string _email = string.Empty;
    public string Email
    {
        get => _email;
        set { _email = value; OnPropertyChanged(); }
    }

    private string _password = string.Empty;
    public string Password
    {
        get => _password;
        set { _password = value; OnPropertyChanged(); }
    }


    public ICommand RegisterCommand => new RelayCommand(async () =>
    {
        var result = await AuthService.RegisterAsync(Email, Password);
        if (result.Success)
        {
            MessageBox.Show("Cuenta creada con éxito");
        }
        else
        {
            MessageBox.Show($"Error al registrar usuario: {result.ErrorMessage}");
        }
    });



    protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    public async Task<RegisterResult> RegisterAsync(string password)
    {
        return await AuthService.RegisterAsync(Email, password);
    }

}
