using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workbench.ViewModels;

namespace Workbench.ViewModels
{
    public class DockManagerViewModel
    {
        public ObservableCollection<DockPanelViewModel> Documents { get; private set; }
        public ObservableCollection<object> Anchorables { get; private set; }

        public DockManagerViewModel(IEnumerable<DockPanelViewModel> panels)
        {
            Documents = new ObservableCollection<DockPanelViewModel>();
            Anchorables = new ObservableCollection<object>();

            foreach(var panel in panels)
            {
                panel.PropertyChanged += Panel_PropertyChanged;
                if(!panel.IsClosed)
                {
                    Documents.Add(panel);
                }
            }
        }

        private void Panel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            DockPanelViewModel panel = sender as DockPanelViewModel;
            if(panel != null)
            {
                if(e.PropertyName == nameof(DockPanelViewModel.IsClosed))
                {
                    if (panel.IsClosed)
                        Documents.Remove(panel);
                    else
                        Documents.Add(panel);
                }
            }
        }
    }
}
