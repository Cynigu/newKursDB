using kurs.commands;
using kurs.model;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace kurs.vm
{
    public abstract class AbstractViewModelTableBaseWithEdit : AbstractViewModelTableBase
    {
        protected AbstractViewModelTableBaseWithEdit(string SelectedCommandText, string tablename) : base(SelectedCommandText, tablename)
        {
            InizialistModel();
        }

        #region Fields

        private Visibility visibilityEditAddDelete;
        private ImodelTable modelTable;

        #endregion

        #region Properties
        public Visibility VisibilityEditAddDelete
        {
            get { return visibilityEditAddDelete; }
            set { this.RaiseAndSetIfChanged(ref visibilityEditAddDelete, value); ; }
        }
        public ImodelTable ModelTable
        {
            get { return modelTable; }
            set { this.RaiseAndSetIfChanged(ref modelTable, value); }
        }
        #endregion

        #region Commands
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
                        if (SelectedRow == null) return ;
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
                      if (SelectedRow == null) return;
                      Delete();
                  }));
            }
        }

        private RelayCommand _exportDataFromRowToEdit;
        public ICommand ExportDataFromRowToEdit
        {
            get
            {
                return _exportDataFromRowToEdit ??
                  (_exportDataFromRowToEdit = new RelayCommand(obj =>
                  {
                      if (SelectedRow == null) return;
                      ExportDataFromRowToEditMet();
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

        // абстрактные методы

        public abstract void InizialistModel();

        // Не абстрактные методы
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
        public virtual void Edit() 
        {

            if(!ModelTable.CheckDataForEdit()) return;
            DataRow temp = SelectedRow.Row;
            ModelTable.RowFromTableToModel(temp);
            DataRow newRow = temp;
            ModelTable.RowFromModelToTable(newRow);
        }
        public virtual void Add()
        {
            
            if(!ModelTable.CheckDataForAdd()) return;
            DataRow newRow = DT.NewRow();
            ModelTable.RowFromModelToTable(newRow);
            DT.Rows.Add(newRow);
        }
        
        public void ExportDataFromRowToEditMet()
        {
            if (SelectedRow == null) return;
            DataRow temp = SelectedRow.Row;
            ModelTable.RowFromTableToModel(temp);
        }
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
        #endregion
    }
}
