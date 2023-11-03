using DabinSA.Model;
using DabinSA.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DabinSA
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public new static App Current => (App)Application.Current;
        public IServiceProvider Services { get; }

        public App()
        {
            Services = ConfigureService();
            var mainView = Services.GetRequiredService<MainWindow>();
            mainView.Show();

        }

        private IServiceProvider ConfigureService()
        {
            var services = new ServiceCollection();


            //ViewModels
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<ChartRepository>();

            //전역스타일 등록
            ResourceDictionary resourceDictionary = new ResourceDictionary();
            resourceDictionary.Source = new Uri("/Styles.xaml", UriKind.Relative);
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);


            //Views
            services.AddSingleton(s => new MainWindow()
            {
                DataContext = s.GetRequiredService<MainViewModel>()
            });

            return services.BuildServiceProvider();

        }
    }
}
