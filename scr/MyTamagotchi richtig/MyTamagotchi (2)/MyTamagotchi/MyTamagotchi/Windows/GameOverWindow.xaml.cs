using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace MyTamagotchi
{
    public partial class GameOverWindow : Window
    {
        public GameOverWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Starte animation
            var shock = (Storyboard)FindResource("ShockTextStoryboard");
            shock.Begin(GameOverText, true);

            // Nach 1 Sekunde: freeze den Text auf Rot
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
            Application.Current.Shutdown();
        }
    }
}
