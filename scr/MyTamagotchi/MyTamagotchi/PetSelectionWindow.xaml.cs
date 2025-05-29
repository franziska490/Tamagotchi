using MyTamagotchi.Models;
using System.Collections.Generic;
using System.Windows;

namespace MyTamagotchi
{
    public partial class PetSelectionWindow : Window
    {
        public List<Pet> pets = new List<Pet>();

        public PetSelectionWindow()
        {
            InitializeComponent();

            if (LoginWindow.IsAdmin)
            {
                EditButton.Visibility = Visibility.Visible;
            }
            else
            {
                EditButton.Visibility = Visibility.Collapsed;
            }

            LoadPets();
        }

        private void LoadPets()
        {
            // zukünftige DB-Anbindung
        }

        private void SealButton_Click(object sender, RoutedEventArgs e)
        {
            StarterPet defaultSeal = new StarterPet(StarterType.ChubbySeal);
            MainWindow mainWindow = new MainWindow(defaultSeal);
            mainWindow.Show();
            this.Close();
        }

        private void PenguinButton_Click(object sender, RoutedEventArgs e)
        {
            StarterPet penguin = new StarterPet(StarterType.Pinguin);
            MainWindow mainWindow = new MainWindow(penguin);
            mainWindow.Show();
            this.Close();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            PetEditSelectionWindow editWindow = new PetEditSelectionWindow(this);
            editWindow.ShowDialog();
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            PetEditSelectionWindow editWindow = new PetEditSelectionWindow(this);
            editWindow.ShowDialog();
        }

        public void AddPet(Pet newPet)
        {
            pets.Add(newPet);
            PetListBox.ItemsSource = null;
            PetListBox.ItemsSource = pets;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }
    }
}
