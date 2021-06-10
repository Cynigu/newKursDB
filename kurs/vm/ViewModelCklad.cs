using kurs.models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace kurs.vm
{
    //class ViewModelCklad: ViewModelTableBase
    //{
    //    #region Readonly
    //    private readonly string _SelectedCommandText;
    //    #endregion

    //    #region Constructors
    //    public ViewModelCklad(): base()
    //    {
    //        TableName = "Склады";
    //        _SelectedCommandText = "select * from склад;";
    //        _dB.AddCommandTable(_SelectedCommandText, DT);
    //        _dB.FillTable();
    //    }
    //    #endregion

    //    #region Fields

    //    #endregion
    //    #region Properties

    //    #endregion

    //    #region Methods
    //    public override void Edit()
    //    {
    //        //if (SelectedRow == null) return;
    //        //DataRow temp = SelectedRow.Row;

    //        //ViewModelEditNP vm = new ViewModelEditNP(_dB);
    //        //vm.CopyRowInWindow(temp);

    //        //EditNP view = new EditNP() { DataContext = vm };
    //        //if (view.ShowDialog() == true)
    //        //{
    //        //    DataRow newRow = temp;
    //        //    vm.ChangeRowInTable(newRow);
    //        //    _dB.ChangeBD(DT, "select * from нп");
    //        //}
    //    }
    //    public override void Delete()
    //    {
    //        if (SelectedRow == null) return;
    //        var result = MessageBox.Show("Вы уверены что хотите удалить строку?", "Удалить", MessageBoxButton.OKCancel);
    //        if (result == MessageBoxResult.OK)
    //        {
    //            int index = DT.Rows.IndexOf(DT.Select("id_Склад=" + SelectedRow["id_Склад"].ToString())[0]);
    //            DT.Rows[index].Delete();
    //           //_dB.UpdateBD(DT);
    //        }
    //        else { return; }
    //    }
    //    public override void Add()
    //    {
    //        //ViewModelEditNP vm = new ViewModelEditNP(_dB);
    //        //EditNP view = new EditNP() { DataContext = vm };
    //        //if (view.ShowDialog() == true)
    //        //{
    //        //    DataRow newRow = DT.NewRow();
    //        //    vm.ChangeRowInTable(newRow);
    //        //    DT.Rows.Add(newRow);
    //        //    _dB.ChangeBD(DT, "select * from нп");
    //        //}
    //    }
    //    #endregion
    //}

 }
