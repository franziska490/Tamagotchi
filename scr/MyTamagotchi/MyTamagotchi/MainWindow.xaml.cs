using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyTamagotchi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void FeedButton_Click(object sender, RoutedEventArgs e)
        {
            HungerBar.Value = Math.Min(HungerBar.Value + 10, 100);
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            MoodBar.Value = Math.Min(MoodBar.Value + 10, 100);
        }

        private void SleepButton_Click(object sender, RoutedEventArgs e)
        {
            EnergyBar.Value = Math.Min(EnergyBar.Value + 10, 100);
        }

        private void ShowStatusButton_Click(object sender, RoutedEventArgs e)
        {
            PetEditSelectionWindow statusWindow = new PetEditSelectionWindow();
            statusWindow.Show();
        }
    }
}