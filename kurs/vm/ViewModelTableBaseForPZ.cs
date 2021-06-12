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
    public class ViewModelTableBaseForPZ : ViewModelTableBaseWithEdit
    {
        #region Readonly

        #endregion

        #region Constructors

        public ViewModelTableBaseForPZ(string SelectedCommandText): base(SelectedCommandText)
        {
            //VisibilityOpen = new Visibility();
            VisibilityEditAddDelete = new Visibility();
            //VisibilityOpen = Visibility.Hidden;
            VisibilityEditAddDelete = Visibility.Hidden;
            VisibilityOpenUO = Visibility.Visible;

            TableName = "Производственный заказ";
        }

        #endregion

        #region Fields
        private Visibility visibilityOpenUO;

        #endregion
        #region Properties

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

        public override void Edit()
        {
            throw new NotImplementedException();
        }

        public override void Add()
        {
            throw new NotImplementedException();
        }

        private void OpenUOT() 
        {
        }
        private void OpenUOM() 
        {
        }
        private void OpenGPr() 
        {
        }
        #endregion

    }
}
