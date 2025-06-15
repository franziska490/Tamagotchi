using MyTamagotchi.Models;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace MyTamagotchi
{
    public partial class GameOverWindow : Window
    {
        private User currentUser;
        public GameOverWindow(User user)
        {
            InitializeComponent();
            currentUser = user;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Startet Animation
            var shock = (Storyboard)FindResource("ShockTextStoryboard");
            shock.Begin(GameOverText, true);

            // Erstellt einen Timer
            var freeze = new DispatcherTimer();
            freeze.Interval = TimeSpan.FromSeconds(1.0);
            freeze.Tick += (s, args) =>
            {
                freeze.Stop();
                GameOverBrush.Color = Colors.Red;
                shock.Remove(GameOverText); // Storyboard beenden
            };
            freeze.Start();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            //Application.Current.Shutdown();
            PetSelectionWindow back = new PetSelectionWindow(currentUser);
            back.Show();
            this.Close();

        }
    }
}
