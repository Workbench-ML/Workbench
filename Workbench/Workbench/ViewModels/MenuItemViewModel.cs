using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Workbench.Infrastructure;

namespace Workbench.ViewModels
{
    public class MenuItemViewModel : ObservableObject
    {
        #region Properties
        public string Header { get; set; }
        public bool IsCheckable { get; set; }
        public ICollection<MenuItemViewModel> Items { get; private set; }
        public ICommand Command { get; private set; }

        private bool _isChecked;
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                if(_isChecked != value)
                {
                    Set(ref _isChecked, value, nameof(IsChecked));
                }
            }
        }
        #endregion

        public MenuItemViewModel()
        {
            Items = new List<MenuItemViewModel>();
        }
    }
}
