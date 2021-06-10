using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace kurs.vm
{
    //class ViewModelKomponents : ViewModelTableBase
    //{
    //    private readonly int _idCP;
    //    private readonly string _SelectedCommandText;
    //    public ViewModelKomponents(int idCP) : base()
    //    {
    //        TableName = "Компоненты";
    //        _idCP = idCP;
    //        _SelectedCommandText = "select Позиция, НП, `Кол-во`, `Ед_изм` from компонент Where Спецификация =" + _idCP + ";";
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
