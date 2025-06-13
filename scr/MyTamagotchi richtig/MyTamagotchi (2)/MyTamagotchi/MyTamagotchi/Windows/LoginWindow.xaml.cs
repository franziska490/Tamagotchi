using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using MyTamagotchi.Models;

namespace MyTamagotchi
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text.Trim();
            string password = PasswordBox.Password.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Benutzername und Passwort dürfen nicht leer sein!", "Fehler");
                return;
            }

            try
            {
                User? user = await PetApiService.GetUserid(username, password);

                if (user != null)
                {
                    PetSelectionWindow petSelectionWindow = new PetSelectionWindow(user);
                    petSelectionWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Login fehlgeschlagen. Benutzername oder Passwort ist falsch.", "Fehler");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Serverfehler beim Login:\n" + ex.Message, "Fehler");
            }
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text.Trim();
            string password = PasswordBox.Password.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Benutzername und Passwort dürfen nicht leer sein!", "Fehler");
                return;
            }

            try
            {
                bool success = await PetApiService.RegisterUser(username, password);

                if (success)
                {
                    MessageBox.Show("Registrierung erfolgreich! Jetzt einloggen.", "Info");
                }
                else
                {
                    MessageBox.Show("Benutzername bereits vergeben oder Fehler bei Registrierung.", "Fehler");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Serverfehler bei Registrierung:\n" + ex.Message, "Fehler");
            }
        }
    }
}
