using GTranslate.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace GTranslate
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider serviceProvider;

        public App()
        {
            var services = new ServiceCollection();

            services.AddSingleton<ClipboardHelper>();
            services.AddSingleton<GeminiTranslateProvider>();
            services.AddSingleton<GoogleTranslateProvider>();
            services.AddSingleton<MainWindowViewmodel>();
            services.AddSingleton(s => CreateMainWindow(s));

            this.serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            this.serviceProvider.GetRequiredService<ClipboardHelper>();
            this.serviceProvider.GetRequiredService<MainWindow>().Show();
        }

        protected MainWindow CreateMainWindow(IServiceProvider serviceProvider)
        {
            return new MainWindow()
            {
                DataContext = serviceProvider.GetRequiredService<MainWindowViewmodel>()
            };
        }
    }
}
