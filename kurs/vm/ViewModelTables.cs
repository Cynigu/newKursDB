using kurs.commands;
using kurs.models;
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

// ВМ для открытия таблиц
namespace kurs.vm
{
    class ViewModelTables: ReactiveObject
    {
        private RelayCommand _openWindowNP;
        public ICommand OpenWindowNP
        {
            get
            {
                return _openWindowNP ??
                  (_openWindowNP = new RelayCommand(obj =>
                  {
                      string select = "select * from нп";
                      ViewModelTableNP vm = new ViewModelTableNP(select);
                      WindowNP NP = new WindowNP() { DataContext = vm };
                      NP.Show();
                  }));
            }
        }
        private RelayCommand _openWindowRC;
        public ICommand OpenWindowRC
        {
            get
            {
                return _openWindowRC ??
                  (_openWindowRC = new RelayCommand(obj =>
                  {
                      string select = "select * from рц";
                      ViewModelTableRC vm = new ViewModelTableRC(select);
                      WindowRC RC = new WindowRC() { DataContext = vm };
                      RC.Show();
                  }));
            }
        }

        private RelayCommand _openWindowCklad;
        public ICommand OpenWindowCklad
        {
            get
            {
                return _openWindowCklad ??
                  (_openWindowCklad = new RelayCommand(obj =>
                  {
                      string select = "select * from склад";
                      ViewModelTableSklad vm = new ViewModelTableSklad(select);
                      WindowSklad Sklad = new WindowSklad() { DataContext = vm };
                      Sklad.Show();
                  }));
            }
        }

        private RelayCommand _openWindowMK;
        public ICommand OpenWindowMK
        {
            get
            {
                return _openWindowMK ??
                  (_openWindowMK = new RelayCommand(obj =>
                  {
                      string select = "select * from мк";
                      ViewModelTableMK vm = new ViewModelTableMK(select);
                      WindowMK MK = new WindowMK() { DataContext = vm };
                      MK.Show();
                  }));
            }
        }

        private RelayCommand _openWindowZapas;
        public ICommand OpenWindowZapas
        {
            get
            {
                return _openWindowZapas ??
                  (_openWindowZapas = new RelayCommand(obj =>
                  {
                      string select = "select * from запас";
                      ViewModelTableZapas vm = new ViewModelTableZapas(select);
                      WindowZapas MK = new WindowZapas() { DataContext = vm };
                      MK.Show();
                  }));
            }
        }

        private RelayCommand _openWindowCP;
        public ICommand OpenWindowCP
        {
            get
            {
                return _openWindowCP ??
                  (_openWindowCP = new RelayCommand(obj =>
                  {
                      string select = "select * from спецификация";
                      ViewModelTableCP vm = new ViewModelTableCP(select);
                      WindowCP CP = new WindowCP() { DataContext = vm };
                      CP.Show();
                  }));
            }
        }

        private RelayCommand _openWindowPZ;
        public ICommand OpenWindowPZ
        {
            get
            {
                return _openWindowPZ ??
                  (_openWindowPZ = new RelayCommand(obj =>
                  {
                      string select = "select * from пз";
                      ViewModelTablePZ vm = new ViewModelTablePZ(select);
                      WindowPZ PZ = new WindowPZ() { DataContext = vm };
                      PZ.Show();
                  }));
            }
        }
    }
}
