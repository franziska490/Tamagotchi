using MyTamagotchi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

// async: Methode kann weiterlaufen ohne die Anwendung zu blockieren
// await: Wartet darauf das die Aufgabe fertig ist

namespace MyTamagotchi
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer lifeTimer = new DispatcherTimer();
        private User currentUser;
        private Pet myPet;
        private List<Pet> pets = new();

        public MainWindow(Pet selectedPet, User user)
        {
            InitializeComponent();
            myPet = selectedPet;
            currentUser = user;
            StartLifeTimer();
            UpdateUI();
            myPet.OnGameOver += ShowGameOverScreen;
            Loaded += async (s, e) => await LoadUserPets(); //Haustiere laden nach Start
        }

        private async Task LoadUserPets()
        {
            ErrorTextBlock.Text = "";
            try
            {
                pets = await PetApiService.GetOwnerPets(currentUser.Id);
                if (pets == null || pets.Count == 0)
                {
                    //ErrorTextBlock.Text = "No pets found.";
                    Logger.Log("No pets for user " + currentUser.Username);
                }
                else
                {
                    Logger.Log($"{pets.Count} pets loaded for {currentUser.Username}.");
                }
            }
            catch (Exception ex)
            {
                ErrorTextBlock.Text = "Error loading pets. :o";
                Logger.Log("Error loading pets: " + ex.Message);
            }
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
        private void LifeTimer_Tick(object? sender, EventArgs e)
        {
            myPet.Hunger = Math.Max(myPet.Hunger - myPet.HungerDecreaseRate, 0);
            myPet.Energy = Math.Max(myPet.Energy - myPet.EnergyDecreaseRate, 0);
            myPet.Mood = Math.Max(myPet.Mood - myPet.MoodDecreaseRate, 0);

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
                myPet.Feed();
                Logger.Log($"{myPet.Name} has {foodWindow.SelectedItem.Name} eaten."); 

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
            Logger.Log($"{myPet.Name} played.");

            if (myPet is StarterPet starter)
            {
                starter.SetActionImage("happy2");
                PetImage.Source = starter.PetImage;
            }

            await Task.Delay(1500);
            UpdateUI();

            // Die Methode wird mehrmals aufgerufen, jedes Mal wenn das Event feuert.
            // Um zu verhindern, dass das Event mehrfach aufgerufen wird, entfernen wir es zuerst.
            MiniGame.OnFinished -= UpdateUI; 
            MiniGame.OnFinished += UpdateUI;
            MiniGame.Start(myPet);
        }

        private async void SleepButton_Click(object sender, RoutedEventArgs e)
        {
            myPet.Sleep();
            Logger.Log($"{myPet.Name} is sleeping.");

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
            ErrorTextBlock.Text = "";
            myPet.Name = PetNameBox.Text;
            myPet.OwnerId = currentUser.Id;

            try
            {
                bool success = await PetApiService.SavePetAsync(myPet);
                if (success)
                {
                    ErrorTextBlock.Text = "Pet saved! :D";
                    Logger.Log($"Pet {myPet.Name} saved.");

                    Igel.Visibility = Visibility.Visible;
                    await Task.Delay(2000);
                    Igel.Visibility = Visibility.Collapsed;
                }
                else
                {
                    ErrorTextBlock.Text = "Error saving pet! O_O";
                    Logger.Log($"Error saving {myPet.Name}.");
                }
            }
            catch (Exception ex)
            {
                ErrorTextBlock.Text = "Server error while saving.";
                Logger.Log("Saving failed: " + ex.Message);
            }
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            myPet.OnGameOver -= ShowGameOverScreen; // verhindert GameOver beim absichtlichen Verlassen
            PetSelectionWindow petSelectionWindow = new PetSelectionWindow(currentUser);
            petSelectionWindow.Show();
            this.Close();
        }

        private void ShowGameOverScreen()
        {
            // Erzwingt die Ausführung im UI-Thread.
            // Notwendig, wenn du dich gerade nicht im UI - Thread befindest(z.B.aus einem Hintergrundthread heraus).
            // Invoke blockiert bis die Aktion abgeschlossen ist (im Gegensatz zu BeginInvoke).
            Dispatcher.Invoke(() =>
            {
                var gameOverWindow = new GameOverWindow(currentUser);
                gameOverWindow.Show();
                this.Close();
            });
        }
    }
}
