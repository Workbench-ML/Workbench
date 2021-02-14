using Ninject;
using Ninject.Modules;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;

using Workbench.Infrastructure;
using Workbench.ViewModels;
using Workbench.Views;

namespace Workbench
{
    class App : Application
    {
        private IKernel _kernel;
        private readonly List<INinjectModule> _modules;
        public App()
        {
            _modules = new List<INinjectModule>()
            {
                new InfrastructureModule()
            };
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
            _kernel = new StandardKernel(_modules.ToArray());

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
