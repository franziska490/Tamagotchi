using MyTamagotchi.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyTamagotchi
{
    public partial class FoodWindow : Window
    {
        public FoodItem SelectedItem { get; private set; }

        public FoodWindow()
        {
            InitializeComponent();
            LoadFoodItems();
        }

        private void LoadFoodItems()
        {
            var foodList = new List<FoodItem>
            {
                new FoodItem("Fisch", 30, 10, "fibsh.png"),
                new FoodItem("Melone", 15, 20, "fibsh2.png"),
                //new FoodItem("Eis", 10, 30, "Assets/icecream.png"),
            };

            foreach (var food in foodList)
            {
                var img = new Image
                {
                    Source = food.LoadImage(),
                    Width = 100,
                    Height = 100,
                    Margin = new Thickness(5),
                    Tag = food
                };

                img.MouseLeftButtonDown += (s, e) =>
                {
                    SelectedItem = (FoodItem)((Image)s).Tag;
                    DialogResult = true;
                    Close();
                };

                FoodPanel.Children.Add(img);
            }

        }
    }
}