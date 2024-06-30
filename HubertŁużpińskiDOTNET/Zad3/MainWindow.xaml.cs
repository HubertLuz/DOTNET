using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Zad3
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<ItemViewModel> Items { get; set; } = new ObservableCollection<ItemViewModel>();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            // Dodaj przykładowe dane
            Items.Add(new ItemViewModel { Title = "Film 1", DirectorOrArtist = "Reżyser 1", PublisherOrStudio = "Studio 1", MediaType = MediaType.DVD });
            Items.Add(new ItemViewModel { Title = "Film 2", DirectorOrArtist = "Reżyser 2", PublisherOrStudio = "Studio 2", MediaType = MediaType.BlueRay });
            Items.Add(new ItemViewModel { Title = "Muzyka 1", DirectorOrArtist = "Artysta 1", PublisherOrStudio = "Wydawca 1", MediaType = MediaType.CD });
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Otwórz okno dodawania nowego elementu
            EditWindow editWindow = new EditWindow();
            editWindow.Owner = this;

            // Jeśli użytkownik zatwierdzi dodanie
            if (editWindow.ShowDialog() == true)
            {
                Items.Add(editWindow.DataContext as ItemViewModel);
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (ItemsList.SelectedItem is ItemViewModel selectedItem)
            {
                // Otwórz okno edycji wybranego elementu
                EditWindow editWindow = new EditWindow(selectedItem);
                editWindow.Owner = this;

                // Jeśli użytkownik zatwierdzi edycję
                if (editWindow.ShowDialog() == true)
                {
                    // Aktualizuj element w kolekcji
                    int index = Items.IndexOf(selectedItem);
                    Items[index] = editWindow.DataContext as ItemViewModel;
                }
            }
            else
            {
                MessageBox.Show("Proszę wybrać element do edycji.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ItemsList.SelectedItem is ItemViewModel selectedItem)
            {
                MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz usunąć wybrany element?", "Potwierdzenie usunięcia", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Items.Remove(selectedItem);
                }
            }
            else
            {
                MessageBox.Show("Proszę wybrać element do usunięcia.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            // Implementacja importu listy z pliku
            MessageBox.Show("Funkcja importu jeszcze nie zaimplementowana.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            // Implementacja eksportu listy do pliku
            MessageBox.Show("Funkcja eksportu jeszcze nie zaimplementowana.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    // Klasa ViewModel dla elementu (filmu/muzyki)
    public class ItemViewModel : INotifyPropertyChanged
    {
        private string _title;
        private string _directorOrArtist;
        private string _publisherOrStudio;
        private MediaType _mediaType;

        public string Title
        {
            get => _title;
            set { _title = value; NotifyPropertyChanged(); }
        }

        public string DirectorOrArtist
        {
            get => _directorOrArtist;
            set { _directorOrArtist = value; NotifyPropertyChanged(); }
        }

        public string PublisherOrStudio
        {
            get => _publisherOrStudio;
            set { _publisherOrStudio = value; NotifyPropertyChanged(); }
        }

        public MediaType MediaType
        {
            get => _mediaType;
            set { _mediaType = value; NotifyPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // Typ wyliczeniowy dla typu nośnika (MediaType)
    public enum MediaType
    {
        VHS,
        DVD,
        BlueRay,
        Cassette,
        CD,
        Pendrive
    }
}