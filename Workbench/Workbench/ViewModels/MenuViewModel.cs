using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workbench.ViewModels
{
    public class MenuViewModel
    {
        public IEnumerable<MenuItemViewModel> Items { get; private set; }

        private readonly MenuItemViewModel ViewMenuItemViewModel;

        public MenuViewModel(IEnumerable<DockPanelViewModel> dockPanels)
        {
            var view = ViewMenuItemViewModel = new MenuItemViewModel() { Header = "Panels" };

            foreach (var panel in dockPanels)
                view.Items.Add(GetPanelItemViewModel(panel));

            var items = new List<MenuItemViewModel>();
            items.Add(view);
            Items = items;
        }

        private MenuItemViewModel GetPanelItemViewModel(DockPanelViewModel panelVM)
        {
            var menuItemVM = new MenuItemViewModel();
            menuItemVM.IsCheckable = true;
            menuItemVM.Header = panelVM.Title;
            menuItemVM.IsChecked = !panelVM.IsClosed;

            panelVM.PropertyChanged += (o, e) =>
            {
                if (e.PropertyName == nameof(DockPanelViewModel.IsClosed))
                    menuItemVM.IsChecked = !panelVM.IsClosed;
            };

            menuItemVM.PropertyChanged += (o, e) =>
            {
                if (e.PropertyName == nameof(MenuItemViewModel.IsChecked))
                    panelVM.IsClosed = !menuItemVM.IsChecked;
            };

            return menuItemVM;
        }
    }
}
