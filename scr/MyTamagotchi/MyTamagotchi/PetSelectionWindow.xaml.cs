
using MyTamagotchi;
using MyTamagotchi.Models;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Windows;


namespace MyTamagotchi
{
    public partial class PetSelectionWindow : Window
    {
        public List<Pet> pets = new List<Pet>();

        public PetSelectionWindow()
        {
            InitializeComponent();

            // Sichtbarkeit des "Eigenschaften ändern"-Buttons abhängig vom Admin-Status
            if (LoginWindow.IsAdmin)
            {

                EditButton.Visibility = Visibility.Visible;
            }
            else
            {
                EditButton.Visibility = Visibility.Collapsed;
            }

            LoadPets(); // (falls du LoadPets hast, einfach wie bisher)
        }


        private void LoadPets()
        {
            // Hier werden später echte Haustiere aus der DB geladen
            // Im Moment fügen wir nichts hinzu
        }

        private void SealButton_Click(object sender, RoutedEventArgs e)
        {
            Pet defaultSeal = new Pet("Chubby Seal"); // Neues Standard-Pet erstellen
            MainWindow mainWindow = new MainWindow(defaultSeal); // Pet übergeben
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

        // Hilfsmethode: neues Pet in Liste einfügen
        public void AddPet(Pet newPet)
        {
            pets.Add(newPet);
            PetListBox.ItemsSource = null;
            PetListBox.ItemsSource = pets;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}