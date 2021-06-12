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
    abstract class ViewModelTableBaseWithEditAndOpen : ViewModelTableBase
    {
        protected ViewModelTableBaseWithEditAndOpen(string SelectedCommandText) : base(SelectedCommandText)
        {
            VisibilityEditAddDelete = Visibility.Hidden;
            VisibilityOpen = Visibility.Hidden;
        }

        #region Fields
        private Visibility visibilityEditAddDelete;
        private Visibility visibilityOpen;
        #endregion

        #region Properties
        public Visibility VisibilityEditAddDelete
        {
            get { return visibilityEditAddDelete; }
            set { this.RaiseAndSetIfChanged(ref visibilityEditAddDelete, value); ; }
        }
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
        // Добавить новую строку
        private ICommand addCommand;
        public ICommand AddCommand
        {
            get
            {
                return addCommand ??
                    (addCommand = new RelayCommand(obj =>
                    {
                        Add();
                    }));
            }
        }
        // Редактировать выбраннуб строку
        private ICommand _editCommand;
        public ICommand EditCommand
        {
            get
            {
                return _editCommand ??
                    (_editCommand = new RelayCommand(obj =>
                    {
                        Edit();
                    }));
            }
        }

        // Удалить строку 
        private ICommand _deleteRowCommand;
        public ICommand DeleteRowCommand
        {
            get
            {
                return _deleteRowCommand ??
                  (_deleteRowCommand = new RelayCommand(obj =>
                  {
                      Delete();
                  }));
            }
        }

        // Отменить изменения
        private ICommand _CancelChangeCommand;
        public ICommand CancelChangeCommand
        {
            get
            {
                return _CancelChangeCommand ??
                  (_CancelChangeCommand = new RelayCommand(obj =>
                  {
                      Cancel();
                  }));
            }
        }

        // Сохранить изменения
        private AsyncCommand updateCommand;
        public IAsyncCommand UpdateCommand
        {
            get
            {
                return updateCommand ??
                    (updateCommand = new AsyncCommand(async () =>
                    {
                        await UpdateAsync();
                    }));
            }
        }
        #endregion

        #region Methods
        private void Delete()
        {
            if (SelectedRow == null) return;
            var result = MessageBox.Show("Вы уверены что хотите удалить строку?", "Удалить", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {

                int index = DT.Rows.IndexOf(SelectedRow.Row);
                DT.Rows[index].Delete();
                //_dB.UpdateBD(DT);
            }
            else { return; }
        }
        private void Cancel()
        {
            DT.RejectChanges();
        }
        public abstract void Edit();
        public abstract void Add();

        public void Update()
        {
            _dB.UpdateBD();
            DT.Clear();
            _dB.FillTable();
        }
        // Сохранить изменения
        public async Task UpdateAsync()
        {
            await _dB.UpdateBDAsync();
            DT.Clear();
            await _dB.FillTableAsync();
        }
        public abstract void Open();
        #endregion
    }
}
