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
            ErrorTextBlock.Text = "";

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ErrorTextBlock.Text = "Username and password can't be empty!";
                Logger.Log("Empty login input.");
                return;
            }

            try
            {
                User user = await PetApiService.GetUserid(username, password);
                if (user != null)
                {
                    new PetSelectionWindow(user).Show();
                    Close();
                }
                else
                {
                    ErrorTextBlock.Text = "Login failed. Wrong username or password! TwT";
                    Logger.Log("Login failed for user: " + username);
                }
            }
            catch (Exception ex)
            {
                ErrorTextBlock.Text = "Server error!";
                Logger.Log("Login failed: " + ex.Message);
            }
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text.Trim();
            string password = PasswordBox.Password.Trim();
            ErrorTextBlock.Text = "";

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ErrorTextBlock.Text = "Username and password can't be empty!";
                Logger.Log("Empty login input.");
                return;
            }
            try
            {
                bool success = await PetApiService.RegisterUser(username, password);
                if (success)
                {
                    ErrorTextBlock.Text = "Registration successful. Please log in! :D";
                    Logger.Log("Registration successful for: " + username);
                }
                else
                {
                    ErrorTextBlock.Text = "Username taken or error! :/";
                    Logger.Log("Registration failed for: " + username);
                }
            }
            catch (Exception ex)
            {
                ErrorTextBlock.Text = "Server error!";
                Logger.Log("Registration error: " + ex.Message);
            }
        }
    }
}
