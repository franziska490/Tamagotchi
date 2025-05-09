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
    /// Interaktionslogik für PetSelectionWindow.xaml
    /// </summary>
    public partial class PetEditSelectionWindow : Window
    {
        public PetEditSelectionWindow()
        {
            InitializeComponent();
        }

        private void UpdateStatusButton_Click(object sender, RoutedEventArgs e)
        {
            // Holt Werte aus MainWindow
            if (Application.Current.Windows[0] is MainWindow mainWindow)
            {
                HungerStatusText.Text = $"Hunger: {(int)mainWindow.HungerBar.Value}";
                EnergyStatusText.Text = $"Energie: {(int)mainWindow.EnergyBar.Value}";
                MoodStatusText.Text = $"Stimmung: {(int)mainWindow.MoodBar.Value}";
            }
        }
    }
}
