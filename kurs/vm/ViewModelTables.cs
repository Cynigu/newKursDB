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
                      
                      ViewModelTablePZBase vm = new ViewModelTablePZBase(select);
                      vm.TableName = "НП";
                      vm.VisibilityEditAddDelete = Visibility.Visible;
                      vm.Edit = () => 
                      {
                          if (vm.SelectedRow == null) return;
                          DataRow temp = vm.SelectedRow.Row;

                          ViewModelEditNP vmNew = new ViewModelEditNP();
                          vmNew.CopyRowInWindow(temp);

                          EditNP viewNew = new EditNP() { DataContext = vmNew };
                          if (viewNew.ShowDialog() == true)
                          {
                              DataRow newRow = temp;
                              vmNew.ChangeRowInTable(newRow);
                              //_dB.UpdateBD();
                          }
                      };
                      vm.Add = () =>
                      {
                          ViewModelEditNP vmN = new ViewModelEditNP();
                          EditNP viewN = new EditNP() { DataContext = vmN };
                          if (viewN.ShowDialog() == true)
                          {
                              DataRow newRow = vm.DT.NewRow();
                              vmN.ChangeRowInTable(newRow);
                              vm.DT.Rows.Add(newRow);
                              //_dB.UpdateBD();
                          }
                      };
                      WindowUniversalTable view = new WindowUniversalTable() { DataContext = vm };
                      view.Show();
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
                      ViewModelTablePZBase vm = new ViewModelTablePZBase(select);
                      vm.TableName = "РЦ";
                      vm.VisibilityEditAddDelete = Visibility.Visible;
                      vm.Edit = () =>
                      {
                          if (vm.SelectedRow == null) return;
                          DataRow temp = vm.SelectedRow.Row;

                          ViewModelEditRC vmNew = new ViewModelEditRC();
                          vmNew.CopyRowInWindow(temp);

                          EditRC viewNew = new EditRC() { DataContext = vmNew };
                          if (viewNew.ShowDialog() == true)
                          {
                              DataRow newRow = temp;
                              vmNew.ChangeRowInTable(newRow);
                              //_dB.UpdateBD();
                          }
                      };
                      vm.Add = () =>
                      {
                          ViewModelEditRC vmN = new ViewModelEditRC();
                          EditRC viewN = new EditRC() { DataContext = vmN };
                          if (viewN.ShowDialog() == true)
                          {
                              DataRow newRow = vm.DT.NewRow();
                              vmN.ChangeRowInTable(newRow);
                              vm.DT.Rows.Add(newRow);
                              //_dB.UpdateBD();
                          }
                      };

                      WindowUniversalTable view = new WindowUniversalTable() { DataContext = vm };
                      view.Show();
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
                      ViewModelTablePZBase vm = new ViewModelTablePZBase(select);
                      vm.TableName = "Склады";
                      WindowUniversalTable view = new WindowUniversalTable() { DataContext = vm };
                      view.Show();
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
                      ViewModelTablePZBase vm = new ViewModelTablePZBase(select);
                      vm.TableName = "Маршрутные карты";
                      vm.VisibilityOpen = Visibility.Visible;
                      vm.Open = () =>
                      {
                          if (vm.SelectedRow == null) return;
                          int idMK = int.Parse(vm.SelectedRow["id_МК"].ToString());
                          string _SelectedCommandText = "select id_Операция,Время_обработки," +
                          " Время_перехода, Время_наладки, РЦ, Описание" +
                          " from операция Where id_Маршрута =" + idMK + ";";
                          ViewModelTablePZBase vmN = new ViewModelTablePZBase(_SelectedCommandText);
                          vmN.TableName = "Состав маршрутной карты №" + idMK;
                          WindowUniversalTable vinOperations = new WindowUniversalTable() { DataContext = vmN };
                          vinOperations.Show();
                      };
                      WindowUniversalTable view = new WindowUniversalTable() { DataContext = vm };
                      view.Show();
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
                      ViewModelTablePZBase vm = new ViewModelTablePZBase(select);
                      vm.TableName = "Запас";
                      WindowUniversalTable view = new WindowUniversalTable() { DataContext = vm };
                      view.Show();
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
                      ViewModelTablePZBase vm = new ViewModelTablePZBase(select);
                      vm.TableName = "Спецификация";
                      vm.VisibilityOpen = Visibility.Visible;
                      vm.Open = () =>
                      {
                          if (vm.SelectedRow == null) return;
                          int idCP = int.Parse(vm.SelectedRow["id_Спецификация"].ToString());
                          string _SelectedCommandText = "select * "+
                          " from компонент;";
                          //string _SelectedCommandText = "select Позиция, НП, `Кол-во`" +
                          //" from компонент Where Спецификация =" + idCP + ";";
                          ViewModelTablePZBase vmN = new ViewModelTablePZBase(_SelectedCommandText);
                          vmN.DT.DefaultView.RowFilter = "Спецификация =" + idCP;
                          vmN.VisibilityEditAddDelete = Visibility.Visible;
                          vmN.Edit = () =>
                          {
                              if (vmN.SelectedRow == null) return;
                              DataRow temp = vmN.SelectedRow.Row;

                              ViewModelEditKom vmNew = new ViewModelEditKom();
                              vmNew.CopyRowInWindow(temp);

                              EditKom viewNew = new EditKom() { DataContext = vmNew };
                              if (viewNew.ShowDialog() == true)
                              {
                                  DataRow newRow = temp;
                                  newRow["Спецификация"] = idCP;
                                  vmNew.ChangeRowInTable(newRow);
                                  //_dB.UpdateBD();
                              }
                          };
                          vmN.Add = () =>
                          {
                              ViewModelEditKom vmN2 = new ViewModelEditKom();
                              EditKom viewN = new EditKom() { DataContext = vmN2 };
                              if (viewN.ShowDialog() == true)
                              {
                                  DataRow newRow = vmN.DT.NewRow();
                                  vmN2.ChangeRowInTable(newRow);
                                  newRow["Спецификация"] = idCP;
                                  vmN.DT.Rows.Add(newRow);
                                  //_dB.UpdateBD();
                              }
                          };
                          vmN.TableName = "Состав спецификации №" + idCP;
                          WindowUniversalTable vinOperations = new WindowUniversalTable() { DataContext = vmN };
                          vinOperations.Show();
                      };
                      vm.VisibilityEditAddDelete = Visibility.Visible;
                      vm.Edit = () =>
                      {
                          if (vm.SelectedRow == null) return;
                          DataRow temp = vm.SelectedRow.Row;

                          ViewModelEditCP vmNew = new ViewModelEditCP();
                          vmNew.CopyRowInWindow(temp);

                          EditCP viewNew = new EditCP() { DataContext = vmNew };
                          if (viewNew.ShowDialog() == true)
                          {
                              DataRow newRow = temp;
                              vmNew.ChangeRowInTable(newRow);
                              //_dB.UpdateBD();
                          }
                      };
                      vm.Add = () =>
                      {
                          ViewModelEditCP vmN = new ViewModelEditCP();
                          EditCP viewN = new EditCP() { DataContext = vmN };
                          if (viewN.ShowDialog() == true)
                          {
                              DataRow newRow = vm.DT.NewRow();
                              vmN.ChangeRowInTable(newRow);
                              vm.DT.Rows.Add(newRow);
                              //_dB.UpdateBD();
                          }
                      };
                      WindowUniversalTable view = new WindowUniversalTable() { DataContext = vm };
                      view.Show();
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
                      ViewModelTablePZBase vm = new ViewModelTablePZBase(select);
                      vm.TableName = "Производственный заказ";
                      //vm.VisibilityOpen = Visibility.Visible;
                      //vm.Open = () =>
                      vm.VisibilityOpenUO = Visibility.Visible;
                      vm.OpenGPr = () =>
                      {
                          if (vm.SelectedRow == null) return;
                          if (int.Parse(vm.SelectedRow.Row["Статус"].ToString()) != 3 
                          && int.Parse(vm.SelectedRow.Row["Статус"].ToString()) != 4)
                          {
                              MessageBox.Show("Учтенные операции есть только для запущенных заказов");
                              return;
                          }
                          int id = int.Parse(vm.SelectedRow["id_ПЗ"].ToString());
                          string _SelectedCommandText = "select * from товарные_операции_выходгп";
                          ViewModelTablePZBase vmN = new ViewModelTablePZBase(_SelectedCommandText);
                          vmN.DT.DefaultView.RowFilter = "id_ПЗ =" + id;
                          vmN.TableName = "Товарные операции (выход ГП) ПЗ №" + id;
                          
                          vmN.VisibilityEditAddDelete = Visibility.Visible;
                          if (int.Parse(vm.SelectedRow.Row["Статус"].ToString()) == 4)
                          {
                              vmN.VisibilityEditAddDelete = Visibility.Hidden;
                          }
                          vmN.Edit = () =>
                          {
                              if (vmN.SelectedRow == null) return;

                              ViewModelEditAddUOT vmNew = new ViewModelEditAddUOT("select НП from запас; ", "НП");
                              vmNew.IsAddEnable = false;
                              DataRow temp = vmN.SelectedRow.Row;
                              vmNew.CopyRowInWindow(temp);

                              EditAddUOT viewNew = new EditAddUOT() { DataContext = vmNew };
                              if (viewNew.ShowDialog() == true)
                              {
                                  DataRow newRow = temp;
                                  newRow["id_ПЗ"] = id;
                                  vmNew.ChangeRowInTable(newRow);
                                  //_dB.UpdateBD();
                              }
                          };
                          vmN.Add = () =>
                          {
                              ViewModelEditAddUOT vmNew = new ViewModelEditAddUOT("select НП from запас; ", "НП");
                              vmNew.IsAddEnable = true;
                              EditAddUOT viewNew = new EditAddUOT() { DataContext = vmNew };
                              if (viewNew.ShowDialog() == true)
                              {
                                  DataRow newRow = vmN.DT.NewRow();
                                  newRow["id_ПЗ"] = id;
                                  vmNew.ChangeRowInTable(newRow);
                                  vmN.DT.Rows.Add(newRow);
                                  //_dB.UpdateBD();
                              }
                          };
                          WindowUniversalTable vinOperations = new WindowUniversalTable() { DataContext = vmN };
                          vinOperations.Show();
                      };
                      vm.OpenUOM = () =>
                      {
                          if (vm.SelectedRow == null) return;
                          if (int.Parse(vm.SelectedRow.Row["Статус"].ToString()) != 3
                          && int.Parse(vm.SelectedRow.Row["Статус"].ToString()) != 4) 
                          {
                              MessageBox.Show("Учтенные операции есть только для запущенных заказов");
                              return; 
                          }
                          int id = int.Parse(vm.SelectedRow["id_ПЗ"].ToString());
                          string _SelectedCommandText = "select * from учтенные_операции_мощности;";
                          ViewModelTablePZBase vmN = new ViewModelTablePZBase(_SelectedCommandText);
                          vmN.DT.DefaultView.RowFilter = "id_ПЗ =" + id;
                          vmN.TableName = "Учтенные операции (мощности) ПЗ №" + id;
                         

                          vmN.VisibilityEditAddDelete = Visibility.Visible;
                          if (int.Parse(vm.SelectedRow.Row["Статус"].ToString()) == 4)
                          {
                              vmN.VisibilityEditAddDelete = Visibility.Hidden;
                          }
                          vmN.Edit = () =>
                          {
                              int idNP = int.Parse(vm.SelectedRow["id_НП"].ToString());
                              if (vmN.SelectedRow == null) return;
                              ViewModelEditUOM vmNew = new ViewModelEditUOM(idNP);
                              vmNew.IsAddEnable = false;
                              DataRow temp = vmN.SelectedRow.Row;
                              vmNew.CopyRowInWindow(temp);
                              EditUOM viewNew = new EditUOM() { DataContext = vmNew };
                              if (viewNew.ShowDialog() == true)
                              {
                                  DataRow newRow = temp;
                                  newRow["id_ПЗ"] = id;
                                  vmNew.ChangeRowInTable(newRow);
                                  //_dB.UpdateBD();
                              }
                          };
                          vmN.Add = () =>
                          {
                              int idNP = int.Parse(vm.SelectedRow["id_НП"].ToString());
                              ViewModelEditUOM vmNew = new ViewModelEditUOM(idNP);
                              vmNew.IsAddEnable = true;
                              EditUOM viewNew = new EditUOM() { DataContext = vmNew };
                              if (viewNew.ShowDialog() == true)
                              {
                                  DataRow newRow = vmN.DT.NewRow();
                                  newRow["id_ПЗ"] = id;
                                  vmNew.ChangeRowInTable(newRow);
                                  vmN.DT.Rows.Add(newRow);
                                  //_dB.UpdateBD();
                              }
                          };

                          WindowUniversalTable vinOperations = new WindowUniversalTable() { DataContext = vmN };
                          vinOperations.Show();
                      };
                      vm.OpenUOT = () =>
                      {
                          if (vm.SelectedRow == null) return;
                          if (int.Parse(vm.SelectedRow.Row["Статус"].ToString()) != 3
                          && int.Parse(vm.SelectedRow.Row["Статус"].ToString()) != 4)
                          {
                              MessageBox.Show("Учтенные операции есть только для запущенных заказов");
                              return;
                          }
                          
                          int id = int.Parse(vm.SelectedRow["id_ПЗ"].ToString());
                          string _SelectedCommandText = "select *" +
                          " from товарные_операции_потребление;";
                          ViewModelTablePZBase vmN = new ViewModelTablePZBase(_SelectedCommandText);
                          
                          vmN.DT.DefaultView.RowFilter = "id_ПЗ =" + id;
                          vmN.TableName = "Товарные операции (потребление) ПЗ №" + id;

                          vmN.VisibilityEditAddDelete = Visibility.Visible;
                          if (int.Parse(vm.SelectedRow.Row["Статус"].ToString()) == 4)
                          {
                              vmN.VisibilityEditAddDelete = Visibility.Hidden;
                          }
                          vmN.Edit = () =>
                          {
                              if (vmN.SelectedRow == null) return;
                              ViewModelEditAddUOT vmNew = new ViewModelEditAddUOT("select НП from запас; ", "НП");
                              vmNew.IsAddEnable = false;
                              DataRow temp = vmN.SelectedRow.Row;
                              vmNew.CopyRowInWindow(temp);

                              EditAddUOT viewNew = new EditAddUOT() { DataContext = vmNew };
                              if (viewNew.ShowDialog() == true)
                              {
                                  DataRow newRow = temp;
                                  newRow["id_ПЗ"] = id;
                                  vmNew.ChangeRowInTable(newRow);
                                  //_dB.UpdateBD();
                              }
                          };
                          vmN.Add = () =>
                          {
                              ViewModelEditAddUOT vmNew = new ViewModelEditAddUOT("select НП from запас; ", "НП");
                              vmNew.IsAddEnable = true;
                              EditAddUOT viewNew = new EditAddUOT() { DataContext = vmNew };
                              if (viewNew.ShowDialog() == true)
                              {
                                  DataRow newRow = vmN.DT.NewRow();
                                  newRow["id_ПЗ"] = id;
                                  vmNew.ChangeRowInTable(newRow);
                                  vmN.DT.Rows.Add(newRow);
                                  //_dB.UpdateBD();
                              }
                          };

                          WindowUniversalTable vinOperations = new WindowUniversalTable() { DataContext = vmN };
                          vinOperations.Show();
                      };

                      vm.VisibilityEditAddDelete = Visibility.Visible;
                      vm.Edit = () =>
                      {
                          if (vm.SelectedRow == null) return;
                          if (int.Parse(vm.SelectedRow.Row["Статус"].ToString()) == 4)
                          {
                              MessageBox.Show("Заказ завершён");
                              return;
                          }
                          DataRow temp = vm.SelectedRow.Row;

                          ViewModelEditPZ vmNew = new ViewModelEditPZ();
                          vmNew.CopyRowInWindow(temp);

                          EditPZ viewNew = new EditPZ() { DataContext = vmNew };
                          if (viewNew.ShowDialog() == true)
                          {
                              DataRow newRow = temp;
                              vmNew.ChangeRowInTable(newRow);
                              //_dB.UpdateBD();
                          }
                      };
                      vm.Add = () =>
                      {
                          ViewModelEditPZ vmN = new ViewModelEditPZ();
                          EditPZ viewN = new EditPZ() { DataContext = vmN };
                          if (viewN.ShowDialog() == true)
                          {
                              DataRow newRow = vm.DT.NewRow();
                              vmN.ChangeRowInTable(newRow);
                              vm.DT.Rows.Add(newRow);
                              //_dB.UpdateBD();
                          }
                      };

                      WindowUniversalTable view = new WindowUniversalTable() { DataContext = vm };
                      view.Show();
                  }));
            }
        }


        private RelayCommand _openWindowDRealis;
        public ICommand OpenWindowDRealis
        {
            get
            {
                return _openWindowDRealis ??
                  (_openWindowDRealis = new RelayCommand(obj =>
                  {
                      string select = "select * from документ_реализации";
                      ViewModelTablePZBase vm = new ViewModelTablePZBase(select);
                      vm.TableName = "Документы реализации";
                      vm.VisibilityOpen = Visibility.Visible;
                      vm.Open = () =>
                      {
                          if (vm.SelectedRow == null) return;
                          int id = int.Parse(vm.SelectedRow["id_Документ"].ToString());
                          string _SelectedCommandText = "select НП, `Кол-во` from реализация Where Id_Документа =" + id + ";";
                          ViewModelTablePZBase vmN = new ViewModelTablePZBase(_SelectedCommandText);
                          vmN.TableName = "Состав документы реализации №" + id;
                          WindowUniversalTable vinOperations = new WindowUniversalTable() { DataContext = vmN };
                          vinOperations.Show();
                      };
                      WindowUniversalTable view = new WindowUniversalTable() { DataContext = vm };
                      view.Show();
                  }));
            }
        }

        private RelayCommand _openWindowDPost;
        public ICommand OpenWindowDPost
        {
            get
            {
                return _openWindowDPost ??
                  (_openWindowDPost = new RelayCommand(obj =>
                  {
                      string select = "select * from документ_поступления";
                      ViewModelTablePZBase vm = new ViewModelTablePZBase(select);
                      vm.TableName = "Документы поступления";
                      vm.VisibilityOpen = Visibility.Visible;
                      vm.Open = () =>
                      {
                          if (vm.SelectedRow == null) return;
                          int id = int.Parse(vm.SelectedRow["id_Документ"].ToString());
                          string _SelectedCommandText = "select НП, `Кол-во` from поступление Where Id_Документа =" + id + ";";
                          ViewModelTablePZBase vmN = new ViewModelTablePZBase(_SelectedCommandText);
                          vm.TableName = "Состав документа поступления №" + id;
                          WindowUniversalTable vinOperations = new WindowUniversalTable() { DataContext = vmN };
                          vinOperations.Show();
                      };
                      WindowUniversalTable view = new WindowUniversalTable() { DataContext = vm };
                      view.Show();
                  }));
            }
        }
    }
}
