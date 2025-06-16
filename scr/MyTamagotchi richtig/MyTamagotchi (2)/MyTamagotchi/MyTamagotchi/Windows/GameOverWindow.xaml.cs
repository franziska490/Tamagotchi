using MyTamagotchi.Models;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace MyTamagotchi
{
    public partial class GameOverWindow : Window
    {
        private User currentUser;
        private Pet deadPet;

        public GameOverWindow(User user, Pet pet)
        {
            InitializeComponent();
            currentUser = user;
            deadPet = pet;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string deadPath;

            if (deadPet is StarterPet starter)
            {
                string type = starter.Type switch
                {
                    StarterType.Pinguin => "pinguin",
                    StarterType.ChubbySeal => "seal",
                    _ => "seal"
                };
                deadPath = $"/Assets/{type}_dead.png";
            }
            else
            {
                deadPath = "/Assets/seal_dead.png";
            }

            try
            {
                DeadImage.Source = new BitmapImage(new Uri($"pack://application:,,,{deadPath}", UriKind.Absolute));
            }
            catch
            {
                DeadImage.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/seal_dead.png", UriKind.Absolute));
            }

            var shock = (Storyboard)FindResource("ShockTextStoryboard");
            shock.Begin(GameOverText, true);

            var freeze = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            freeze.Tick += (s, args) =>
            {
                freeze.Stop();
                GameOverBrush.Color = Colors.Red;
                shock.Remove(GameOverText);
            };
            freeze.Start();
        }


        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            PetSelectionWindow back = new PetSelectionWindow(currentUser);
            back.Show();
            this.Close();
        }
    }
}
