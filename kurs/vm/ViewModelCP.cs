using kurs.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace kurs.vm
{
    //class ViewModelCP : ViewModelTableBase
    //{
    //    private readonly string _SelectedCommandText;
    //    public ViewModelCP() : base()
    //    {
    //        TableName = "Спецификация";
    //        _SelectedCommandText = "select * from спецификация;";
    //        _dB.AddCommandTable(_SelectedCommandText, DT);
    //        _dB.FillTable();
    //        VisibilityOpen = Visibility.Visible;
    //    }

    //    #region Methods

    //    public override void Open()
    //    {
    //        if (SelectedRow == null) return;
    //        int idCP = int.Parse(SelectedRow["id_Спецификация"].ToString());
    //        ViewModelKomponents vm = new ViewModelKomponents(idCP);
    //        WindowUniversalTable view = new WindowUniversalTable() { DataContext = vm };
    //        view.Show();
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
