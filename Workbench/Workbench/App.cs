using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using Workbench.Infrastructure.DI;
using Workbench.Infrastructure.Services;
using Workbench.ViewModels;
using Workbench.Views;

namespace Workbench
{
    class App : Application
    {
        public IDependencyInjectionEngine DependencyInjectionEngine { get; private set; }

        private ILoggingService loggingService;
        public App()
        {
            DependencyInjectionEngine = new NinjectEngine();
        }

        private void CreateWorkbenchWindow()
        {
            MainWindow = new MainWindow();
            MainWindow.DataContext = new MainViewModel();
            MainWindow.Show();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DependencyInjectionEngine.LoadAssembly(Assembly.GetExecutingAssembly());
            loggingService = DependencyInjectionEngine.Get<ILoggingService>();
            loggingService.Info("Welcome to Workbench");
            CreateWorkbenchWindow();
        }

        [STAThread]
        public static void Main()
        {
            App app = new App();
            app.Run();
        }
    }
}
