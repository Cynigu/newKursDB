using kurs.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace kurs.vm
{
    //class ViewModelOperations: ViewModelTableBase
    //{
    //    private readonly int _idMK;
    //    private readonly string _SelectedCommandText;
    //    public ViewModelOperations(int idMK) : base()
    //    {
    //        TableName = "Операции";
    //        _idMK = idMK;
    //        _SelectedCommandText = "select id_Операция,Время_обработки, Время_перехода, Время_наладки, РЦ, Описание" +
    //            " from операция Where id_Маршрута =" + _idMK + ";";
    //        _dB.AddCommandTable(_SelectedCommandText, DT);
    //        _dB.FillTable();
    //    }
    //    #region Methods
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
