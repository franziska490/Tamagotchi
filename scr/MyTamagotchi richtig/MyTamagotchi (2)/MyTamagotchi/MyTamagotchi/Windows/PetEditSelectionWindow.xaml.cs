using MyTamagotchi.Models;
using System;
using MyTamagotchi;
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
using System.Text.RegularExpressions;
namespace MyTamagotchi
{
    
    public partial class PetEditSelectionWindow : Window
    {
        private PetSelectionWindow parentWindow;
        List<User> users = new List<User>();

        public PetEditSelectionWindow(PetSelectionWindow parent)
        {
            InitializeComponent();
            parentWindow = parent;
            Loaded += PetEditSelectionWindow_Loaded; 
        }
        private async void PetEditSelectionWindow_Loaded(object sender, RoutedEventArgs e)
        {
            users = await PetApiService.GetUsersAsync();
            UserListBox.ItemsSource = users;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorTextBlock.Text = "";

            string petName = NameBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(petName) || !Regex.IsMatch(petName, @"^[a-zA-Z]+$"))
            {
                ErrorTextBlock.Text = "Invalid name: letters only.";
                Logger.Log("Invalid pet name input.");
                return;
            }

            Pet newPet = new Pet(petName);

            if (!int.TryParse(HungerRateBox.Text, out int hungerRate) || hungerRate < 1 || hungerRate > 20)
            {
                ErrorTextBlock.Text = "Invalid hunger value: must be 1–20.";
                Logger.Log("Invalid hunger decrease rate.");
                return;
            }
            if (!int.TryParse(EnergyRateBox.Text, out int energyRate) || energyRate < 1 || energyRate > 20)
            {
                ErrorTextBlock.Text = "Invalid energy value: must be 1–20.";
                Logger.Log("Invalid energy decrease rate.");
                return;
            }
            if (!int.TryParse(MoodRateBox.Text, out int moodRate) || moodRate < 1 || moodRate > 20)
            {
                ErrorTextBlock.Text = "Invalid mood value: must be 1–20.";
                Logger.Log("Invalid mood decrease rate.");
                return;
            }

            newPet.HungerDecreaseRate = hungerRate;
            newPet.EnergyDecreaseRate = energyRate;
            newPet.MoodDecreaseRate = moodRate;

            parentWindow.AddPet(newPet);
            Logger.Log($"New pet created: {newPet.Name}");
            Close();
        }

        private async void DeleteUsersButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorTextBlock.Text = "";

            if (sender is Button btn && btn.Tag is User userToDelete)
            {
                bool deleted = false;
                try
                {
                    deleted = await PetApiService.DeleteUsers(userToDelete.Id);
                }
                catch (Exception ex)
                {
                    Logger.Log("Error deleting user: " + ex.Message);
                    ErrorTextBlock.Text = "Error delet.";
                    return;
                }

                if (deleted)
                {
                    users.Remove(userToDelete);
                    UserListBox.ItemsSource = null;
                    UserListBox.ItemsSource = users;
                    Logger.Log($"User deleted: {userToDelete.Username}");
                }
                else
                {
                    ErrorTextBlock.Text = "Delete failed.";
                    Logger.Log($"Delete attempt failed: {userToDelete.Username}");
                }
            }
            else
            {
                ErrorTextBlock.Text = "Invalid user selection.";
                Logger.Log("Invalid tag in DeleteUsersButton_Click");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
