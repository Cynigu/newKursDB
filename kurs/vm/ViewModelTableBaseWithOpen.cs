using kurs.commands;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace kurs.vm
{
    abstract class ViewModelTableBaseWithOpen : ViewModelTableBase
    {
        protected ViewModelTableBaseWithOpen(string SelectedCommandText) : base(SelectedCommandText)
        {
            VisibilityOpen = Visibility.Hidden;
        }

        #region Fields
        private Visibility visibilityOpen;

        #endregion
        #region Properties
        public Visibility VisibilityOpen
        {
            get { return visibilityOpen; }
            set { this.RaiseAndSetIfChanged(ref visibilityOpen, value); ; }
        }

        #endregion

        #region Commands
        private RelayCommand _openCommand;
        public ICommand OpenCommand
        {
            get
            {
                return _openCommand ??
                  (_openCommand = new RelayCommand(obj =>
                  {
                      Open();
                  }));
            }
        }
        #endregion

        public abstract void Open();
    }
}
