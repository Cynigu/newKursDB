using Autofac;
using kurs.models;
using kurs.view;
using kurs.vm;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace kurs
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public App()
        {
            this.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(App_DispatcherUnhandledException);

        }
        protected override void OnStartup(StartupEventArgs e)
        {
            logger.Info("Запуск приложения...");
            var builderBase = new ContainerBuilder();
            //builderBase.RegisterType<DB>().As<IDB>();
            builderBase.RegisterType<ViewModelTables>().AsSelf();
            var containerBase = builderBase.Build();
            var viewmodelBase = containerBase.Resolve<ViewModelTables>();
            var viewBase = new Tables { DataContext = viewmodelBase };
            viewBase.Show();
        }

        void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Исключение");
            logger.Fatal(e.Exception.StackTrace + " " + "Исключение: "
                + e.Exception.GetType().ToString() + " " + e.Exception.Message);

            e.Handled = true;
        }
    }
}
