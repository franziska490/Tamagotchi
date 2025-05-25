using System.Configuration;
using System.Data;
using System.Windows;
using MyTamagotchi.Models;

namespace MyTamagotchi
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Call TestMain logic
            await TestApi.Main(new string[0]);
        }
    }

}
