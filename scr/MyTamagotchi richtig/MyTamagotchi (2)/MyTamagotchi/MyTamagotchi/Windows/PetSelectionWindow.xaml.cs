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
        private User currentUser; // ← NEU: Aktueller User

        public PetSelectionWindow(User user)
        {
            InitializeComponent();
            currentUser = user;
            Loaded += PetSelectionWindow_Loaded; // ← NEU: Ladeevent
        }

        private async void PetSelectionWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Admin sichtbar machen
            if (currentUser.IsAdmin())
                EditButton.Visibility = Visibility.Visible;
            else
                EditButton.Visibility = Visibility.Collapsed;

            await LoadPets(); // ← Jetzt korrekt asynchron geladen
        }

        private async Task LoadPets()
        {
            int currentUserId = currentUser.Id; // TODO: Vom LoginWindow übernehmen

            try
            {
                pets = await PetApiService.GetOwnerPets(currentUserId);

                if (pets == null)
                {
                    MessageBox.Show("Fehler beim Abrufen der Haustiere.");
                    return;
                }

                if (pets.Count == 0)
                {
                    MessageBox.Show("Keine Haustiere gefunden.");
                    return;
                }

                PetListBox.ItemsSource = pets;
            }
            catch
            {
                MessageBox.Show("Fehler beim Laden der Haustiere.");
            }
        }

        private void SealButton_Click(object sender, RoutedEventArgs e)
        {
            StarterPet defaultSeal = new StarterPet(StarterType.ChubbySeal)
            {
                OwnerId = currentUser.Id //  WICHTIG: muss ein existierender User in der Datenbank sein
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
            if (sender is Button btn && btn.Tag is Pet petToDelete)
            {
                var confirm = MessageBox.Show($"Willst du wirklich '{petToDelete.Name}' löschen?", "Löschen?", MessageBoxButton.YesNo);
                if (confirm != MessageBoxResult.Yes) return;

                bool deleted = await PetApiService.DeletePetAsync(petToDelete.Id);
                if (deleted)
                {
                    pets.Remove(petToDelete);
                    PetListBox.ItemsSource = null;
                    PetListBox.ItemsSource = pets;
                }
                else
                {
                    MessageBox.Show("Fehler beim Löschen.");
                }
            }
        }

        
    }
}
