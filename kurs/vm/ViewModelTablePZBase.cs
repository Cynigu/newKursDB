using kurs.commands;
using kurs.models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

/*Универсальное VM для работы с универсальным окном, в котором находится одна datagrid таблица, соединящаяся с БД
 *Конструктор требует только select запрос, по которому будет строится таблица 
 *Для открытия доп. таблиц внутри этой используются делегаты в названии которых в начале есть слово Open..., 
 *Делегаты следует инициализировать при создании объекта, не забывая про то что кнопки соотвествующие нужным коммандам нужно 
 *сделать visibl, для чего в этом vm есть переменные типа Visibility
 *Также можно задать имя окна*/

namespace kurs.vm
{
    public class ViewModelTablePZBase : ReactiveObject
    {
        #region Readonly
        public readonly DB _dB;
        private readonly string _SelectedCommandText; // Для отображения таблицы 


        #endregion

        #region Constructors
        public ViewModelTablePZBase(string SelectedCommandText, bool t)
        {
            _SelectedCommandText = SelectedCommandText;

            string connect = "SERVER = localhost;Database=kursdb;UID=root; Password=root;  convert zero datetime=True; ";
            _dB = new DB(DT, connect);
            VisibilityOpen = new Visibility();
            VisibilityEditAddDelete = new Visibility();
            VisibilityOpen = Visibility.Hidden;
            VisibilityEditAddDelete = Visibility.Hidden;
            VisibilityOpenUO = Visibility.Hidden;

            DT = new DataTable();

            _dB.AddCommandSelectTable(_SelectedCommandText); // Подключаем таблицу, добавляем комманды
            _dB.FillTable(); // заполняем таблицу 
        }

        public ViewModelTablePZBase(DataTable dt)
        {
            //string connect = "SERVER = localhost;Database=kursdb;UID=root; Password=root;  convert zero datetime=True; ";
            //_dB = new DB(connect);
            VisibilityOpen = Visibility.Hidden;
            VisibilityEditAddDelete = Visibility.Hidden;
            VisibilityOpenUO = Visibility.Hidden;
           
            DT = dt;

            //_dB.AddCommandSelectTable(_SelectedCommandText, DT); // Подключаем таблицу, добавляем комманды
            //_dB.FillTable(); // заполняем таблицу 
        }

        public ViewModelTablePZBase(string SelectedCommandText)
        {
            _SelectedCommandText = SelectedCommandText;

            string connect = "SERVER = localhost;Database=гардероб;UID=root; Password=root;  convert zero datetime=True; ";
            
            VisibilityOpen = new Visibility();
            VisibilityEditAddDelete = new Visibility();
            VisibilityOpen = Visibility.Hidden;
            VisibilityEditAddDelete = Visibility.Hidden;
            VisibilityOpenUO = Visibility.Hidden;

            DT = new DataTable();

            _dB = new DB(DT, connect);
            _dB.AddCommandTable(_SelectedCommandText); // Подключаем таблицу, добавляем комманды
            _dB.FillTable(); // заполняем таблицу 
        }
        #endregion

        #region Fields
        private DataTable dt;
        private Visibility visibilityOpen;
        private Visibility visibilityOpenUO;
        private Visibility visibilityEditAddDelete;
        private DataRowView selectedRow;
        private string tableName;

        private bool enavledButtonFroSelectedRow;
        #endregion
        #region Properties
        public string TableName
        {
            get
            {
                return tableName;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref tableName, value);
            }
        }
        public DataTable DT
        {
            get
            {
                return dt;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref dt, value);
            }
        }
        public Visibility VisibilityOpen
        {
            get { return visibilityOpen; }
            set { this.RaiseAndSetIfChanged(ref visibilityOpen, value); ; }
        }
        public Visibility VisibilityEditAddDelete
        {
            get { return visibilityEditAddDelete; }
            set { this.RaiseAndSetIfChanged(ref visibilityEditAddDelete, value); ; }
        }
        public Visibility VisibilityOpenUO 
        {
            get { return visibilityOpenUO; }
            set { this.RaiseAndSetIfChanged(ref visibilityOpenUO, value); }
        }
        public bool EnavledButtonFroSelectedRow
        {
            get { return enavledButtonFroSelectedRow; }
            set { this.RaiseAndSetIfChanged(ref enavledButtonFroSelectedRow, value); }
        }
        public DataRowView SelectedRow
        {
            get { return selectedRow; }
            set 
            {
                this.RaiseAndSetIfChanged(ref selectedRow, value);
                if (SelectedRow != null) EnavledButtonFroSelectedRow = true;
                else EnavledButtonFroSelectedRow = false;
            }
        }

        public Action Open { get; set; }
        public Action OpenUOT { get; set; }
        public Action OpenUOM { get; set; }
        public Action OpenGPr { get; set; }
        public Action Edit { get; set; }
        public Action Add { get; set; }
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
                        Edit();
                    }));
            }
        }

        private AsyncCommand updateCommand;
        public IAsyncCommand UpdateCommand
        {
            get
            {
                return updateCommand ??
                    (updateCommand = new AsyncCommand(async() =>
                    {
                        await UpdateAsync();
                    }));
            }
        }

        private AsyncCommand fillCommand;
        public IAsyncCommand FillCommand
        {
            get
            {
                return fillCommand ??
                    (fillCommand = new AsyncCommand(async() =>
                    {
                        
                        await FillAsync();
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

        private RelayCommand _openUOMCommand;
        public ICommand OpenUOMCommand
        {
            get
            {
                return _openUOMCommand ??
                  (_openUOMCommand = new RelayCommand(obj =>
                  {
                      OpenUOM();
                  }));
            }
        }

        private RelayCommand _openUOTCommand;
        public ICommand OpenUOTCommand
        {
            get
            {
                return _openUOTCommand ??
                  (_openUOTCommand = new RelayCommand(obj =>
                  {
                      OpenUOT();
                  }));
            }
        }

        private RelayCommand _openGPrCommand;
        public ICommand OpenGPrCommand
        {
            get
            {
                return _openGPrCommand ??
                  (_openGPrCommand = new RelayCommand(obj =>
                  {
                      OpenGPr();
                  }));
            }
        }
        #endregion


        #region Methods

        public void Delete()
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
        public async Task UpdateAsync()
        {
            await _dB.UpdateBDAsync();
            DT.Clear();
            await _dB.FillTableAsync();
        }
        public async Task FillAsync()
        {
            DT.Clear();
            await _dB.FillTableAsync();
        }
        public void Cancel()
        {
            DT.RejectChanges();
        }
        public void Update()
        {
            _dB.UpdateBD();
            DT.Clear();
            _dB.FillTable();
        }
        public void Fill()
        {
            DT.Clear();
            _dB.FillTable();
        }
        #endregion

    }
}
