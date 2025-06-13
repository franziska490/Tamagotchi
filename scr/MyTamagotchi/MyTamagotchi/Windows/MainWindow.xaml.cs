// MainWindow.xaml.cs
using MyTamagotchi.Models;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MyTamagotchi
{
    public partial class MainWindow : Window
    {
        private Pet myPet;
        private DispatcherTimer lifeTimer;
        private User currentUser;

        public MainWindow(Pet selectedPet, User user)
        {
            InitializeComponent();
            myPet = selectedPet;
            currentUser = user;
            StartLifeTimer();
            UpdateUI();

            myPet.OnGameOver += ShowGameOverScreen;
        }

        private void UpdatePetImage()
        {
            if (myPet is StarterPet starter)
            {
                starter.UpdateImage();
                PetImage.Source = starter.PetImage;

                if (starter.Mood == 0 || starter.Energy == 0 || starter.Hunger == 0)
                {
                    lifeTimer.Stop();
                }
            }
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

        private void UpdateUI()
        {
            HungerBar.Value = myPet.Hunger;
            EnergyBar.Value = myPet.Energy;
            MoodBar.Value = myPet.Mood;

            HungerValue.Text = $"{myPet.Hunger}%";
            EnergyValue.Text = $"{myPet.Energy}%";
            MoodValue.Text = $"{myPet.Mood}%";

            UpdatePetImage();
        }

        private async void FeedButton_Click(object sender, RoutedEventArgs e)
        {
            FoodWindow foodWindow = new FoodWindow();
            bool? result = foodWindow.ShowDialog();

            if (result == true && foodWindow.SelectedItem != null)
            {
                foodWindow.SelectedItem.ApplyTo(myPet);
                Logger.Log($"{myPet.Name} hat {foodWindow.SelectedItem.Name} gegessen.");

                if (myPet is StarterPet starter)
                {
                    starter.SetActionImage("nom");
                    PetImage.Source = starter.PetImage;
                }

                await Task.Delay(1500);
                UpdateUI();
            }
        }

        private async void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            myPet.Play();
            Logger.Log($"{myPet.Name} hat gespielt.");

            if (myPet is StarterPet starter)
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
            Logger.Log($"{myPet.Name} hat geschlafen.");

            if (myPet is StarterPet starter)
            {
                starter.SetActionImage("sleep");
                PetImage.Source = starter.PetImage;
            }

            await Task.Delay(1500);
            UpdateUI();
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            myPet.Name = PetNameBox.Text;
            myPet.OwnerId = currentUser.Id;

            bool success = await PetApiService.SavePetAsync(myPet);

            if (success)
            {
                MessageBox.Show("Haustier erfolgreich gespeichert!");
                PetSelectionWindow sel = new PetSelectionWindow(currentUser);
                sel.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Fehler beim Speichern.");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            PetSelectionWindow petSelectionWindow = new PetSelectionWindow(currentUser);
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
