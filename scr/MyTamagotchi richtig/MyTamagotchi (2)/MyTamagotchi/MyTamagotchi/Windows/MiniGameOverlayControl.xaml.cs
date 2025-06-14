using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using MyTamagotchi.Models;

namespace MyTamagotchi
{
    public partial class MiniGameOverlayControl : UserControl
    {
        private Pet? petRef;
        private int clickCount = 0;
        private Random rnd = new();

        public event Action? OnFinished;

        public MiniGameOverlayControl()
        {
            InitializeComponent();
            this.Visibility = Visibility.Collapsed;
        }

        public void Start(Pet pet)
        {
            petRef = pet;
            clickCount = 0;
            this.Visibility = Visibility.Visible;
            ClickImage.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/fibsh6.png", UriKind.Absolute));
            NextImagePosition();
        }

        private void NextImagePosition()
        {
            ClickImage.Visibility = Visibility.Visible;

            double maxX = Math.Max(0, ActualWidth - ClickImage.Width);
            double maxY = Math.Max(0, ActualHeight - ClickImage.Height);

            double x = rnd.NextDouble() * maxX;
            double y = rnd.NextDouble() * maxY;

            Canvas.SetLeft(ClickImage, x);
            Canvas.SetTop(ClickImage, y);
        }

        private void ClickImage_MouseLeftButtonDown(object? sender, MouseButtonEventArgs e)
        {
            if (petRef == null) return;

            petRef.Mood = Math.Min(petRef.Mood + 10, 100);
            clickCount++;

            if (clickCount >= 5)
            {
                ClickImage.Visibility = Visibility.Collapsed;
                this.Visibility = Visibility.Collapsed;
                OnFinished?.Invoke();
            }
            else
            {
                NextImagePosition();
            }
        }
    }
}
