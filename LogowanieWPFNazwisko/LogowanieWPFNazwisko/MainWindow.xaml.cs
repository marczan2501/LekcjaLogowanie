using System.Windows;

namespace LogowanieWPFNazwisko
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result =
                MessageBox.Show(
                    "Czy na pewno chcesz wyjść?",
                    "Uwaga",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes)
            {
                e.Cancel = true;
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title = "Zalogowany "+ Zmienne.nazwaUzytkownika;
        }
    }
}
