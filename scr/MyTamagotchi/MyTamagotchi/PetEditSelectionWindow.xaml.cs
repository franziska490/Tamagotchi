using MyTamagotchi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaktionslogik für PetEditSelectionWindow.xaml
    /// </summary>
    public partial class PetEditSelectionWindow : Window
    {
        private PetSelectionWindow parentWindow;

        public PetEditSelectionWindow(PetSelectionWindow parent)
        {
            InitializeComponent();
            parentWindow = parent;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string petName = NameBox.Text.Trim();

            // Name darf nicht leer und nur Buchstaben sein
            if (string.IsNullOrWhiteSpace(petName) || !Regex.IsMatch(petName, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("Nuhu thats not a Name!");
                return;
            }

            // Neues Pet erstellen
            Pet newPet = new Pet(petName);

            // Verfallraten prüfen
            if (!int.TryParse(HungerRateBox.Text, out int hungerRate) || hungerRate < 1 || hungerRate > 20)
            {
                MessageBox.Show("Number between 1-20");
                return;
            }

            if (!int.TryParse(EnergyRateBox.Text, out int energyRate) || energyRate < 1 || energyRate > 20)
            {
                MessageBox.Show("Number between 1-20");
                return;
            }

            if (!int.TryParse(MoodRateBox.Text, out int moodRate) || moodRate < 1 || moodRate > 20)
            {
                MessageBox.Show("Number between 1-20");
                return;
            }

            // Werte setzen
            newPet.HungerDecreaseRate = hungerRate;
            newPet.EnergyDecreaseRate = energyRate;
            newPet.MoodDecreaseRate = moodRate;

            // Pet dem Parent Window hinzufügen
            parentWindow.AddPet(newPet);

            // Log schreiben
            Logger.Log($"New Pet: {newPet.Name})");

            // Fenster schließen
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
