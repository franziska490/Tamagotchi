using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using MyTamagotchi.Models;

namespace MyTamagotchi
{
    public partial class MiniGameOverlayControl : UserControl
    {
        private DispatcherTimer? timer;
        private DateTime startTime;
        private Pet? petRef;

        public event Action? OnFinished;

        public MiniGameOverlayControl()
        {
            InitializeComponent();
        }

        public void Start(Pet pet)
        {
            petRef = pet;
            this.Visibility = Visibility.Visible;

            GameText.Text = "Warte...";
            ClickImage.Visibility = Visibility.Collapsed;

            ClickImage.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/target.png"));

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(new Random().Next(2, 5));
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            GameText.Text = "Klick jetzt!";
            ClickImage.Visibility = Visibility.Visible;
            startTime = DateTime.Now;
        }

        private async void ClickImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TimeSpan reaction = DateTime.Now - startTime;
            ClickImage.Visibility = Visibility.Collapsed;

            if (reaction.TotalMilliseconds < 500)
            {
                petRef.Mood = Math.Min(petRef.Mood + 10, 100);
                Logger.Log($"{petRef.Name} wurde durch das Spiel fröhlicher.");
            }
            else
            {
                petRef.Mood = Math.Max(petRef.Mood - 5, 0);
            }

            GameText.Text = $"Reaktion: {reaction.TotalMilliseconds:F0} ms";
            await System.Threading.Tasks.Task.Delay(1500);

            this.Visibility = Visibility.Collapsed;
            OnFinished?.Invoke(); // Event für z. B. UpdateUI()
        }
    }
}
