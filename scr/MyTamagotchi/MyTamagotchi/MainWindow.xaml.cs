using MyTamagotchi.Models;
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
using System.Windows.Threading;

namespace MyTamagotchi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Pet myPet;
        private DispatcherTimer lifeTimer;

        public MainWindow()
        {
            InitializeComponent();
            myPet = new Pet("Fluffy");

            UpdateUI();

            // Lebenssimulation Timer alle 5 Sekunden
            lifeTimer = new DispatcherTimer();
            lifeTimer.Interval = TimeSpan.FromSeconds(5);
            lifeTimer.Tick += LifeTimer_Tick;
            lifeTimer.Start();
        }

        private void LifeTimer_Tick(object sender, EventArgs e)
        {
            // Haustier wird hungriger und müder
            myPet.Hunger = Math.Max(myPet.Hunger - 2, 0);
            myPet.Energy = Math.Max(myPet.Energy - 2, 0);
            myPet.Mood = Math.Max(myPet.Mood - 1, 0);

            UpdateUI();
        }

        private void UpdateUI()
        {
            HungerBar.Value = myPet.Hunger;
            EnergyBar.Value = myPet.Energy;
            MoodBar.Value = myPet.Mood;
        }

        private void FeedButton_Click(object sender, RoutedEventArgs e)
        {
            myPet.Feed();
            UpdateUI();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            myPet.Play();
            UpdateUI();
        }

        private void SleepButton_Click(object sender, RoutedEventArgs e)
        {
            myPet.Sleep();
            UpdateUI();
        }

        private void ShowStatusButton_Click(object sender, RoutedEventArgs e)
        {
            StatusWindow statusWindow = new StatusWindow(myPet);
            statusWindow.Show();
        }
    }



}