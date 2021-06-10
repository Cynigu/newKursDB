using kurs.commands;
using kurs.models;
using kurs.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace kurs.vm
{
    //class ViewModelMK: ViewModelTableBase
    //{
    //    private readonly string _SelectedCommandText;
    //    public ViewModelMK() : base()
    //    {
    //        TableName = "Маршрутная карта";
    //        _SelectedCommandText = "select * from мк;";
    //        _dB.AddCommandTable(_SelectedCommandText, DT);
    //        _dB.FillTable();
    //        VisibilityOpen = Visibility.Visible;
    //    }
    //    #region Commands
        
    //    #endregion
    //    #region Methods
    //    public override void Open()
    //    {
    //        if (SelectedRow == null) return;
    //        int idMK = int.Parse(SelectedRow["id_МК"].ToString());
    //        ViewModelOperations vm = new ViewModelOperations(idMK);
    //        WindowUniversalTable ncWindow = new WindowUniversalTable() { DataContext = vm };
    //        ncWindow.Show();
    //    }
    //    public override void Edit()
    //    {
    //        //if (SelectedRow == null) return;
    //        //DataRow temp = SelectedRow.Row;

    //        //ViewModelEditRC vm = new ViewModelEditRC();
    //        //vm.CopyRowInWindow(temp);

    //        //EditRC view = new EditRC() { DataContext = vm };
    //        //if (view.ShowDialog() == true)
    //        //{
    //        //    DataRow newRow = temp;
    //        //    vm.ChangeRowInTable(newRow);
    //        //    _dB.ChangeBD(DT, "select * from рц");
    //        //}
    //    }

    //    public override void Delete()
    //    {
    //        if (SelectedRow == null) return;
    //        var result = MessageBox.Show("Вы уверены что хотите удалить строку?", "Удалить", MessageBoxButton.OKCancel);
    //        if (result == MessageBoxResult.OK)
    //        {
    //            //string CommandText = "DELETE FROM мк WHERE id_МК=" + SelectedRow["id_МК"] + ";";
    //            //_dB.ChangeTable(DT, CommandText);
    //            //_dB.ChangeBD(DT, _CommandText);
    //            int index = DT.Rows.IndexOf(DT.Select("id_МК =" + SelectedRow["id_МК"].ToString())[0]);
    //            DT.Rows[index].Delete();
    //        }
    //        else { return; }
    //    }
    //    public override void Add()
    //    {
    //        //ViewModelEditRC vm = new ViewModelEditRC();
    //        //EditRC view = new EditRC() { DataContext = vm };
    //        //if (view.ShowDialog() == true)
    //        //{
    //        //    DataRow newRow = DT.NewRow();
    //        //    vm.ChangeRowInTable(newRow);
    //        //    DT.Rows.Add(newRow);
    //        //    _dB.ChangeBD(DT, "select * from рц");
    //        //}
    //    }
    //    #endregion
    //}
}
