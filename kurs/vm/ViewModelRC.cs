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

namespace kurs.vm
{
    //class ViewModelRC: ViewModelTableBase
    //{
    //    private readonly string _SelectedCommandText;
    //    #region Constructor
    //    public ViewModelRC(): base()
    //    {
    //        TableName = "Рабочий центр";
    //        _SelectedCommandText = "select * from рц;";
    //        _dB.AddCommandTable(_SelectedCommandText, DT);
    //        _dB.FillTable();
    //    }
    //    #endregion

    //    #region Fields

    //    #endregion

    //    #region Properties

    //    #endregion

    //    #region Command

    //    #endregion

    //    #region Methods

    //    public override void Edit()
    //    {
    //        if (SelectedRow == null) return;
    //        DataRow temp = SelectedRow.Row;

    //        ViewModelEditRC vm = new ViewModelEditRC();
    //        vm.CopyRowInWindow(temp);

    //        EditRC view = new EditRC() { DataContext = vm };
    //        if (view.ShowDialog() == true)
    //        {
    //            DataRow newRow = temp;
    //            vm.ChangeRowInTable(newRow);
    //           //_dB.ChangeBD(DT, "select * from рц");
    //        }
    //    }
    //    public override void Delete()
    //    {
    //        if (SelectedRow == null) return;
    //        var result = MessageBox.Show("Вы уверены что хотите удалить строку?", "Удалить", MessageBoxButton.OKCancel);
    //        if (result == MessageBoxResult.OK)
    //        {
    //            //string CommandText = "DELETE FROM рц WHERE id_РЦ=" + SelectedRow["id_РЦ"] + ";";
    //            //_dB.ChangeTable(DT, CommandText);
    //            int index = DT.Rows.IndexOf(DT.Select("id_РЦ =" + SelectedRow["id_РЦ"].ToString())[0]);
    //            DT.Rows[index].Delete();
    //            //_dB.ChangeBD(DT, "select * from рц");
    //        }
    //        else { return; }
    //    }
    //    public override void Add()
    //    {
    //        ViewModelEditRC vm = new ViewModelEditRC();
    //        EditRC view = new EditRC() { DataContext = vm };
    //        if (view.ShowDialog() == true)
    //        {
    //            DataRow newRow = DT.NewRow();
    //            vm.ChangeRowInTable(newRow);
    //            DT.Rows.Add(newRow);
    //        }
    //    }
    //    #endregion
    //}
}
