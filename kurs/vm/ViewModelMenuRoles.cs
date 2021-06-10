using kurs.commands;
using kurs.models;
using kurs.view;
using NLog;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


// ВМ для выбора пользователя (на данный момент выбор происходит простым нажатием на кнопку)

namespace kurs.vm
{
    enum Roles { ADMIN, MASTER, STOREKEEPER, MANAGER, TECHNOLOGIST, CHIEF }
    class ViewModelMenuRoles: ReactiveObject
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public ViewModelMenuRoles()
        {
            _logger.Info("Запуск программы");
        }

        #region command
        private RelayCommand _openWindowAdmin;
        public ICommand OpenWindowAdmin
        {
            get
            {
                return _openWindowAdmin ??
                  (_openWindowAdmin = new RelayCommand(obj =>
                  {
                      ViewModelTables vm = new ViewModelTables();
                      Tables tablesWindow = new Tables() { DataContext = vm };
                      tablesWindow.Show();
                  }));
            }
        }
        #endregion
    }
}
