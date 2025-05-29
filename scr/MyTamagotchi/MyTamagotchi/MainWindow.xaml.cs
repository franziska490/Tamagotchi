using MyTamagotchi.Models;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Threading.Tasks;

namespace MyTamagotchi
{
    public partial class MainWindow : Window
    {
        private Pet myPet;
        private DispatcherTimer lifeTimer;

        public MainWindow(Pet selectedPet)
        {
            InitializeComponent();
            myPet = selectedPet;

            myPet.OnGameOver += ShowGameOverScreen;

            StartLifeTimer();
            UpdateUI();
        }

        private async Task LoadPet()
        {
            var pets = await PetApiService.GetPetsAsync();
            if (pets.Count > 0)
            {
                myPet = pets[0];
                UpdatePetImage();
                UpdateStatsUI();
            }
            else
            {
                MessageBox.Show("Wompi, no saved pet!");
            }
        }

        private void UpdatePetImage()
        {
            if (myPet.Name.ToLower().Contains("pinguin"))
            {
                PetImage.Source = new BitmapImage(new Uri("/Assets/pinguin.png", UriKind.Relative));
            }
            else if (myPet is StarterPet starter)
            {
                starter.UpdateImage();
                PetImage.Source = starter.PetImage;
            }
        }

        private string GetMoodName(int mood)
        {
            if (mood > 66) return "happy";
            if (mood > 33) return "neutral";
            return "sad";
        }

        private void StartLifeTimer()
        {
            lifeTimer = new DispatcherTimer();
            lifeTimer.Interval = TimeSpan.FromSeconds(5);
            lifeTimer.Tick += LifeTimer_Tick;
            lifeTimer.Start();
        }

        private void LifeTimer_Tick(object sender, EventArgs e)
        {
            myPet.Hunger = Math.Max(myPet.Hunger - 10, 0);
            myPet.Energy = Math.Max(myPet.Energy - 8, 0);
            myPet.Mood = Math.Max(myPet.Mood - 4, 0);

            UpdateUI();
            myPet.CheckGameOver();
        }

        private void UpdateStatsUI()
        {
            HungerBar.Value = myPet.Hunger;
            EnergyBar.Value = myPet.Energy;
            MoodBar.Value = myPet.Mood;

            HungerValue.Text = $"{myPet.Hunger}%";
            EnergyValue.Text = $"{myPet.Energy}%";
            MoodValue.Text = $"{myPet.Mood}%";
        }

        private void UpdateUI()
        {
            UpdateStatsUI();
            UpdatePetImage();
        }

        private async void FeedButton_Click(object sender, RoutedEventArgs e)
        {
            myPet.Feed();
            Logger.Log($"{myPet.Name} was feeded.");

            if (myPet.Name.ToLower().Contains("pinguin"))
            {
                PetImage.Source = new BitmapImage(new Uri("/Assets/pinguin.png", UriKind.Relative));
            }
            else if (myPet is StarterPet starter)
            {
                starter.SetActionImage("nom");
                PetImage.Source = starter.PetImage;
            }

            await Task.Delay(1500);
            UpdateUI();
        }

        private async void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            myPet.Play();
            Logger.Log($"{myPet.Name} had played.");

            if (myPet.Name.ToLower().Contains("pinguin"))
            {
                PetImage.Source = new BitmapImage(new Uri("/Assets/pinguin.png", UriKind.Relative));
            }
            else if (myPet is StarterPet starter)
            {
                starter.SetActionImage("happy2");
                PetImage.Source = starter.PetImage;
            }

            await Task.Delay(1500);
            UpdateUI();
        }

        private async void SleepButton_Click(object sender, RoutedEventArgs e)
        {
            myPet.Sleep();
            Logger.Log($"{myPet.Name} did sleep.");

            if (myPet.Name.ToLower().Contains("pinguin"))
            {
                PetImage.Source = new BitmapImage(new Uri("/Assets/pinguin.png", UriKind.Relative));
            }
            else if (myPet is StarterPet starter)
            {
                starter.SetActionImage("sleep");
                PetImage.Source = starter.PetImage;
            }

            await Task.Delay(1500);
            UpdateUI();
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            await PetApiService.SavePetAsync(myPet);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            PetSelectionWindow petSelectionWindow = new PetSelectionWindow();
            petSelectionWindow.Show();
            this.Close();
        }

        private void ShowGameOverScreen()
        {
            Dispatcher.Invoke(() =>
            {
                var gameOverWindow = new GameOverWindow();
                gameOverWindow.Show();
                this.Close();
            });
        }
    }
}
