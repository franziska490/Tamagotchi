using MyTamagotchi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MyTamagotchi
{
    public partial class PetSelectionWindow : Window
    {
        public List<Pet> pets = new List<Pet>();
        private User currentUser;

        public PetSelectionWindow(User user)
        {
            InitializeComponent();
            currentUser = user;
            Loaded += PetSelectionWindow_Loaded;
        }

        private async void PetSelectionWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Admin sichtbar machen
            if (currentUser.IsAdmin())
                EditButton.Visibility = Visibility.Visible;
            else
                EditButton.Visibility = Visibility.Collapsed;

            await LoadPets(); 
        }

        private async Task LoadPets()
        {
            int currentUserId = currentUser.Id;
            ErrorTextBlock.Text = "";

            try
            {
                pets = await PetApiService.GetOwnerPets(currentUserId);
                if (pets == null)
                {
                    ErrorTextBlock.Text = "No pets :C.";
                    Logger.Log("Null returned by GetOwnerPets for user ID: " + currentUserId);
                    return;
                }
                if (pets.Count == 0)
                {
                    ErrorTextBlock.Text = "Huh no pets found! :O.";
                    Logger.Log("No pets found for user ID: " + currentUserId);
                    return;
                }

                PetListBox.ItemsSource = pets;
            }
            catch (Exception ex)
            {
                ErrorTextBlock.Text = "No pets where loaded! :<";
                Logger.Log("Load Pets: " + ex.Message);
            }
        }

        private void SealButton_Click(object sender, RoutedEventArgs e)
        {
            StarterPet defaultSeal = new StarterPet(StarterType.ChubbySeal)
            {
                OwnerId = currentUser.Id
            };

            MainWindow mainWindow = new MainWindow(defaultSeal, currentUser);
            mainWindow.Show();
            this.Close();
        }

        private void PenguinButton_Click(object sender, RoutedEventArgs e)
        {
            StarterPet penguin = new StarterPet(StarterType.Pinguin)
            {
                OwnerId = currentUser.Id
            };

            MainWindow mainWindow = new MainWindow(penguin, currentUser);
            mainWindow.Show();
            this.Close();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            PetEditSelectionWindow editWindow = new PetEditSelectionWindow(this);
            editWindow.ShowDialog();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }

        private void PetListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PetListBox.SelectedItem is Pet selectedPet)
            {
                MainWindow mainWindow = new MainWindow(selectedPet, currentUser);
                mainWindow.Show();
                this.Close();
            }
        }

        public void AddPet(Pet newPet)
        {
            pets.Add(newPet);
            PetListBox.ItemsSource = null;
            PetListBox.ItemsSource = pets;
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorTextBlock.Text = "";

            if (sender is Button btn && btn.Tag is Pet petToDelete)
            {
                Logger.Log($"Delete request for pet: {petToDelete.Name}");
                bool deleted;
                try
                {
                    deleted = await PetApiService.DeletePetAsync(petToDelete.Id);
                }
                catch (Exception ex)
                {
                    ErrorTextBlock.Text = "Error deleting pet!";
                    Logger.Log("DeletePetAsync error: " + ex.Message);
                    return;
                }

                if (deleted)
                {
                    pets.Remove(petToDelete);
                    PetListBox.ItemsSource = null;
                    PetListBox.ItemsSource = pets;
                    Logger.Log($"Pet deleted: {petToDelete.Name}");
                }
                else
                {
                    ErrorTextBlock.Text = "Delete failed! :o";
                    Logger.Log($"Delete failed for pet: {petToDelete.Name}");
                }
            }
        }
    }
}
