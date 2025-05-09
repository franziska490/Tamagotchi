using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyTamagotchi
{
    /// <summary>
    /// Interaktionslogik für LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private static Dictionary<string, string> users = new Dictionary<string, string>();
        private static string adminUsername = "admin";
        private static string adminPassword = "admin123";

        public static bool IsAdmin { get; private set; } = false;

        public LoginWindow()
        {
            InitializeComponent();

            // Admin fix einbauen
            if (!users.ContainsKey(adminUsername))
            {
                users.Add(adminUsername, adminPassword);
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text.Trim();
            string password = PasswordBox.Password.Trim();

            if (users.ContainsKey(username) && users[username] == password)
            {
                MessageBox.Show("Login erfolgreich!", "Info");

                if (username == adminUsername)
                {
                    IsAdmin = true;
                }
                else
                {
                    IsAdmin = false;
                }

                // Weiter zu PetSelectionWindow
                PetSelectionWindow petSelectionWindow = new PetSelectionWindow();
                petSelectionWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Benutzername oder Passwort falsch!", "Fehler");
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text.Trim();
            string password = PasswordBox.Password.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Benutzername und Passwort dÃ¼rfen nicht leer sein!", "Fehler");
                return;
            }

            if (users.ContainsKey(username))
            {
                MessageBox.Show("Benutzer existiert bereits!", "Fehler");
                return;
            }

            users.Add(username, password);
            MessageBox.Show("Registrierung erfolgreich!", "Info");
        }
    }
}
