using MyTamagotchi.Models;
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
    /// Interaktionslogik für PetEditSelectionWindow.xaml
    /// </summary>
    public partial class PetEditSelectionWindow : Window
    {
        private Pet myPet;

        public PetEditSelectionWindow(Pet pet)
        {
            InitializeComponent();
            myPet = pet;
            UpdateStatus();
        }

        private void UpdateStatus()
        {
            HungerStatusText.Text = $"Hunger: {myPet.Hunger}";
            EnergyStatusText.Text = $"Energie: {myPet.Energy}";
            MoodStatusText.Text = $"Stimmung: {myPet.Mood}";
        }

        private void UpdateStatusButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateStatus();
        }
    }
}
