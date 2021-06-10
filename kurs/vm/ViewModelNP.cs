using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using kurs.commands;
using kurs.models;
using kurs.view;
using MySql.Data.MySqlClient;
using ReactiveUI;

namespace kurs.vm
{
    //class ViewModelNP : ViewModelTableBase
    //{
    //    #region Readonly
    //    private readonly string _SelectedCommandText;
    //    #endregion

    //    #region Constructors
    //    public ViewModelNP() : base()
    //    {
    //        TableName = "Номенклатурные позиции";
    //        _SelectedCommandText = "select * from нп;";
    //        _dB.AddCommandTable(_SelectedCommandText, DT);
    //        _dB.FillTable();
    //    }
    //    #endregion

    //    #region Fields

    //    #endregion
    //    #region Properties

    //    #endregion

    //    #region Commands


    //    #endregion

    //    #region Methods

    //    public override void Edit()
    //    {
    //        if (SelectedRow == null) return;
    //        DataRow temp = SelectedRow.Row;

    //        ViewModelEditNP vm = new ViewModelEditNP();
    //        vm.CopyRowInWindow(temp);

    //        EditNP view = new EditNP() { DataContext = vm };
    //        if (view.ShowDialog() == true)
    //        {
    //            DataRow newRow = temp;
    //            vm.ChangeRowInTable(newRow);
    //            //_dB.UpdateBD();
    //        }
    //    }
    //    public override void Add()
    //    {
    //        ViewModelEditNP vm = new ViewModelEditNP();
    //        EditNP view = new EditNP() { DataContext = vm };
    //        if (view.ShowDialog() == true)
    //        {
    //            DataRow newRow = DT.NewRow();
    //            vm.ChangeRowInTable(newRow);
    //            DT.Rows.Add(newRow);
    //            //_dB.UpdateBD();
    //        }
    //    }
    //    #endregion
    //}
}
