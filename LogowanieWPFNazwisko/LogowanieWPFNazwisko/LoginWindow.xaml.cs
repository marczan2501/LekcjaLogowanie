using System.Windows;

namespace LogowanieWPFNazwisko
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }
        SqlCon sqlCon = new SqlCon();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sqlCon.IfDatabaseExist("BazaSklep.sqlite", "Users");
            sqlCon.CloseConnection();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            
            if (txtUserName.Text == "")
            {
                MessageBox.Show("Pole użytkownik nie może być puste", "Uwaga", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtPassword.Password == "")
            {
                MessageBox.Show("Pole hasło nie może być puste", "Uwaga", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            sqlCon.ConnectToDatabase("BazaSklep.sqlite");

            if (sqlCon.LogOnUser(txtUserName.Text, txtPassword.Password, "Users") == 1)
            {
                sqlCon.CloseConnection();
                Zmienne.nazwaUzytkownika = txtUserName.Text;
                MainWindow mainWindow = new MainWindow();
                mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                mainWindow.Show();
                this.Close();
            }
            else if (sqlCon.LogOnUser(txtUserName.Text, txtPassword.Password, "Users") == 2)
            {
                sqlCon.CloseConnection();
                MessageBox.Show("Nieprawidłowe hasło", "Uwaga", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (sqlCon.LogOnUser(txtUserName.Text, txtPassword.Password, "Users") == 3)
            {
                MessageBoxResult result =
                MessageBox.Show(
                    "Czy chcesz dodać użytkownika?",
                    "Inforamcja",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    if (sqlCon.AddUser(txtUserName.Text, txtPassword.Password, "Users"))
                    {
                        MessageBox.Show(string.Format("Dodałem użytkownika:\t{0}", txtUserName.Text), "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                        sqlCon.CloseConnection();
                    }
                }
            }
            else
            {
                MessageBox.Show("Błąd", "Uwaga", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }
    }
}