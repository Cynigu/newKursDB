using kurs.commands;
using kurs.model;
using kurs.models;
using kurs.service;
using kurs.view;
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
    public class ViewModelTablePZ : AbstractViewModelTableBaseWithEdit
    {
        #region Readonly

        #endregion

        #region Constructors

        public ViewModelTablePZ(string SelectedCommandText, string t ="Производственный заказ" ) : base(SelectedCommandText, t)
        {
        }

        #endregion

        #region Fields
        private Visibility visibilityOpenUO;
        private PZmodel pzModel;

        #endregion

        #region Properties
        public PZmodel PzModel
        {
            get { return pzModel; }
            set { this.RaiseAndSetIfChanged(ref pzModel, value); }
        }
        public Visibility VisibilityOpenUO 
        {
            get { return visibilityOpenUO; }
            set { this.RaiseAndSetIfChanged(ref visibilityOpenUO, value); }
        }

        #endregion

        #region Commands

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
                      OpenUOTpotr();
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
                      OpenUOTGPr();
                  }));
            }
        }

        private RelayCommand _computeDataEndCommand;
        public ICommand ComputeDataEndCommand
        {
            get
            {
                return _computeDataEndCommand ??
                  (_computeDataEndCommand = new RelayCommand(obj =>
                  {
                      PzModel.ComputeDataEnd();
                  }));
            }
        }

        #endregion

        #region Methods

        public override void Edit()
        {
            if (int.Parse(SelectedRow.Row["Статус"].ToString()) == 4)
            {
                MessageBox.Show("Заказ завершён, редактировать нельзя");
                return;
            }
            if (!ModelTable.CheckDataForEdit()) return;
            DataRow temp = SelectedRow.Row;
            ModelTable.RowFromTableToModel(temp);
            DataRow newRow = temp;
            ModelTable.RowFromModelToTable(newRow);
        }

        public override void InizialistModel()
        {
            PzModel = new PZmodel();
            ModelTable = PzModel;
        }

        private void OpenUOTpotr() 
        {
            if (int.Parse(SelectedRow.Row["Статус"].ToString()) != 3
                          && int.Parse(SelectedRow.Row["Статус"].ToString()) != 4)
            {
                MessageBox.Show("Учтенные операции есть только для запущенных заказов");
                return;
            }

            string select = "select * from товарные_операции_потребление";
            int idpz = int.Parse(SelectedRow["id_ПЗ"].ToString());
            int countpz = SelectedRow.Row.Field<int>("Кол-во");

            ViewModelTableUOTPOTR vm = new ViewModelTableUOTPOTR(select, idpz, countpz, "Журнал потребления ПЗ №" + idpz);


            if (int.Parse(SelectedRow.Row["Статус"].ToString()) == 4)
            {
                vm.VisibilityEditAddDelete = Visibility.Hidden;
            }

            WindowUOTPOTR UOT = new WindowUOTPOTR() { DataContext = vm };
            UOT.Show();
        }
        private void OpenUOM() 
        {
            if (int.Parse(SelectedRow.Row["Статус"].ToString()) != 3
                          && int.Parse(SelectedRow.Row["Статус"].ToString()) != 4)
            {
                MessageBox.Show("Учтенные операции есть только для запущенных заказов");
                return;
            }

            string select = "select * from учтенные_операции_мощности";
            int id = int.Parse(SelectedRow["id_ПЗ"].ToString());
            
            ViewModelTableUOM vm = new ViewModelTableUOM(select, id, "Учтенные операции (мощности) ПЗ №" + id );


            if (int.Parse(SelectedRow.Row["Статус"].ToString()) == 4)
            {
                vm.VisibilityEditAddDelete = Visibility.Hidden;
            }
            WindowUOM UOM = new WindowUOM() { DataContext = vm };
            UOM.Show();
        }
        private void OpenUOTGPr() 
        {
            if (int.Parse(SelectedRow.Row["Статус"].ToString()) != 3
                          && int.Parse(SelectedRow.Row["Статус"].ToString()) != 4)
            {
                MessageBox.Show("Учтенные операции есть только для запущенных заказов");
                return;
            }

            string select = "select * from товарные_операции_выходгп";
            int idpz = int.Parse(SelectedRow["id_ПЗ"].ToString());
            int countpz = SelectedRow.Row.Field<int>("Кол-во");

            ViewModelTableUOTGP vm = new ViewModelTableUOTGP(select, idpz, countpz, "Журнал выхода ПЗ №" + idpz);


            if (int.Parse(SelectedRow.Row["Статус"].ToString()) == 4)
            {
                vm.VisibilityEditAddDelete = Visibility.Hidden;
            }

            WindowUOTGP UOT = new WindowUOTGP() { DataContext = vm };
            UOT.Show();
        }
        
        #endregion

    }
}
