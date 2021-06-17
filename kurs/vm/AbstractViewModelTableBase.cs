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
    public abstract class AbstractViewModelTableBase : ReactiveObject
    {
        #region Readonly
        public readonly DB _dB;
        private readonly string _SelectedCommandText; // Для отображения таблицы 


        #endregion

        #region Constructors

        public AbstractViewModelTableBase(string SelectedCommandText, string tablename)
        {
            _SelectedCommandText = SelectedCommandText;
            TableName = tablename;

            DT = new DataTable();
            _dB = new DB(DT);
            _dB.AddCommandTable(_SelectedCommandText); // Подключаем таблицу, добавляем комманды
            _dB.FillTable(); // заполняем таблицу 
        }
        #endregion

        #region Fields
        private DataTable dt;
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
        
        public bool EnavledButtonFroSelectedRow
        {
            get { return enavledButtonFroSelectedRow; }
            set { this.RaiseAndSetIfChanged(ref enavledButtonFroSelectedRow, value); }
        } // для блокировки возможностей (кнопок), которые не используется без выделенное строки 

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

        #endregion

        #region Commands
        private AsyncCommand fillCommand;
        public IAsyncCommand FillCommand
        {
            get
            {
                return fillCommand ??
                    (fillCommand = new AsyncCommand(async () =>
                    {

                        await FillAsync();
                    }));
            }
        }
        #endregion

        #region Methods
        public async Task FillAsync()
        {
            DT.Clear();
            await _dB.FillTableAsync();
        }
        public void Fill()
        {
            DT.Clear();
            _dB.FillTable();
        }
        #endregion

    }
}
