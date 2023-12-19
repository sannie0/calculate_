using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace calculate_
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var serviceCollection = new ServiceCollection(); //экземпляр
            serviceCollection.AddSingleton<MainViewModel>();

            serviceCollection.AddSingleton<IMemory, RAM>(); //при запросе IMemory возвращается экз RAM
            //                          контракт реализация

            //serviceCollection.AddSingleton<IMemory, FileM>();
           // serviceCollection.AddSingleton<IMemory, BDM>();

            Provider = serviceCollection.BuildServiceProvider();
        }


        public static ServiceProvider Provider { get; private set; }

    }
}
