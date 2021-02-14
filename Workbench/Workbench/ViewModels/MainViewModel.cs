using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workbench.ViewModels.Panels;

namespace Workbench.ViewModels
{
    public class MainViewModel
    {
        public DockManagerViewModel DockManagerViewModel { get; private set; }
        public MenuViewModel MenuViewModel { get; private set; }

        public MainViewModel()
        {
            var panels = new List<DockPanelViewModel>();

            panels.Add(new ConsolePanelViewModel() { Title = "Console" });

            DockManagerViewModel = new DockManagerViewModel(panels);
            MenuViewModel = new MenuViewModel(panels);
        }
    }
}
