using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MovieApp.WPF.Models
{
    public class MovieModel : INotifyPropertyChanged
    {
        private bool _isFavorite;

        public string Title { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
        public string ImdbID { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Poster { get; set; } = string.Empty;

        public bool IsFavorite
        {
            get => _isFavorite;
            set
            {
                if (_isFavorite != value)
                {
                    _isFavorite = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
