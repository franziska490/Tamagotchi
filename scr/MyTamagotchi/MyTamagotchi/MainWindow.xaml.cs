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

using System.Threading.Tasks;

namespace MyTamagotchi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Pet myPet;
        private DispatcherTimer lifeTimer;

        public MainWindow(Pet selectedPet)
        {
            InitializeComponent();
            myPet = selectedPet;
            StartLifeTimer();
            UpdateUI();
            //SelectPet();


        }

        //private void SelectPet()
        //{
        //    PetSelectionWindow selectionWindow = new PetSelectionWindow();
        //    if (selectionWindow.ShowDialog() == true)
        //    {
        //        string selectedType = selectionWindow.SelectedPetType;

        //        switch (selectedType)
        //        {
        //            case "Seal":
        //                myPet = new Pet("Chubby Seal");
        //                PetImage.Source = new BitmapImage(new Uri("/Assets/seal_happy.png", UriKind.Relative));
        //                break;
        //            case "Pinguin":
        //                myPet = new Pet("Sid");
        //                PetImage.Source = new BitmapImage(new Uri("/Assets/seal_happy.png", UriKind.Relative));
        //                break;
        //        }
        //    }
        //    else
        //    {
        //        myPet = new Pet("Seal");
        //        PetImage.Source = new BitmapImage(new Uri("/Assets/seal_happy.png", UriKind.Relative));
        //    }
        //    UpdateUI();
        //}
        private async Task LoadPet()
        {
            var pets = await PetApiService.GetPetsAsync();
            if (pets.Count > 0)
            {
                myPet = pets[0];
                UpdatePetImage();
                UpdateUI();
            }
            else
            {
                MessageBox.Show("Wompi, no saved pet!");
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




        // TASK THREADS
        private void StartLifeTimer()
        {
            lifeTimer = new System.Windows.Threading.DispatcherTimer();
            lifeTimer.Interval = TimeSpan.FromSeconds(5);
            lifeTimer.Tick += LifeTimer_Tick;
            lifeTimer.Start();
        }
        private void LifeTimer_Tick(object sender, EventArgs e)
        {
            //Haustier wird hungriger und müder
            myPet.Hunger = Math.Max(myPet.Hunger - 10, 0);
            myPet.Energy = Math.Max(myPet.Energy - 8, 0);
            myPet.Mood = Math.Max(myPet.Mood - 4, 0);

            UpdateUI();
            //myPet.Hunger = Math.Max(myPet.Hunger - myPet.HungerDecreaseRate, 0);
            //myPet.Energy = Math.Max(myPet.Energy - myPet.EnergyDecreaseRate, 0);
            //myPet.Mood = Math.Max(myPet.Mood - myPet.MoodDecreaseRate, 0);

            //UpdateUI();
        }
        private void UpdateUI()
        {
            HungerBar.Value = myPet.Hunger;
            EnergyBar.Value = myPet.Energy;
            MoodBar.Value = myPet.Mood;
            // Prozent gehen mit
            HungerValue.Text = $"{myPet.Hunger}%";
            EnergyValue.Text = $"{myPet.Energy}%";
            MoodValue.Text = $"{myPet.Mood}%";

            UpdatePetImage(); // Automatische Bildauswahl
        }


        // Buttons

        private async void FeedButton_Click(object sender, RoutedEventArgs e)
        {
            myPet.Feed();
            Logger.Log($"{myPet.Name} was feeded.");

            if (myPet is StarterPet starter)
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
            Logger.Log($"{myPet.Name} did sleep.");

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
            await PetApiService.SavePetAsync(myPet);
            MessageBox.Show("Pet was saved!", "Info");
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            PetSelectionWindow petSelectionWindow = new PetSelectionWindow();
            petSelectionWindow.Show();
            this.Close();
        }


    }
}

