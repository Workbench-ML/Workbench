using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Workbench.Commands;
using Workbench.Infrastructure;
using Workbench.ViewModels;

namespace Workbench.ViewModels
{
    public class DockPanelViewModel : ObservableObject
    {
        #region Properties
        private ICommand _closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                if(_closeCommand == null)
                {
                    _closeCommand = new RelayCommand(call => Close(), pred => CanPanelClose());
                }
                return _closeCommand;
            }
        }

        private bool _isClosed;
        public bool IsClosed
        {
            get => _isClosed;
            set
            {
                if(_isClosed != value)
                {
                    Set(ref _isClosed, value, nameof(IsClosed));
                }
            }
        }
        private bool _canClose;
        public bool CanClose
        {
            get => _canClose;
            set
            {
                if(_canClose != value)
                {
                    Set(ref _canClose, value, nameof(CanClose));
                }
            }
        }
        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                if(_title != value)
                {
                    Set(ref _title, value, nameof(Title));
                }
            }
        }
        #endregion

        public DockPanelViewModel()
        {
            CanClose = true;
            IsClosed = false;
        }

        private bool CanPanelClose()
        {
            return CanClose;
        }

        public void Close()
        {
            IsClosed = true;
        }
    }
}
