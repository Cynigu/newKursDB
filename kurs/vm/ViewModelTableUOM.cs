using kurs.commands;
using kurs.model;
using kurs.models;
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
    class ViewModelTableUOM : AbstractViewModelTableBaseWithEdit
    {
        private readonly int _idPZ;
        public ViewModelTableUOM(string SelectedCommandText,string t = "Учтенные операции (мощности)") : base(SelectedCommandText, t)
        {
            InizialistModel();
        }

        public ViewModelTableUOM(string SelectedCommandText, int idPZ, string t = "Учтенные операции (мощности)") : base(SelectedCommandText, t)
        {
            DT.DefaultView.RowFilter = "id_ПЗ =" + idPZ;
            _idPZ = idPZ;
            
            InizialistModel();
        }

        #region fields
        UOMmodel uommodel;
        #endregion
        #region Properties
        public UOMmodel UOMModel
        {
            get { return uommodel; }
            set { this.RaiseAndSetIfChanged(ref uommodel, value); }
        }
        #endregion

        #region Commands
        // GenerateTableCommand

        private RelayCommand generateTableCommand;
        public ICommand GenerateTableCommand
        {
            get
            {
                return generateTableCommand ??
                  (generateTableCommand = new RelayCommand(obj =>
                  {
                      GenerateTable();
                  }));
            }
        }
        #endregion

        #region Abstract Methods

        public override void InizialistModel()
        {
            UOMModel = new UOMmodel(_idPZ);
            ModelTable = UOMModel;
        }


        #endregion

        private void GenerateTable()
        {
            DT.AcceptChanges();
            foreach (DataRow dr in DT.Rows)
            {
                if (dr.RowState == DataRowState.Deleted)
                {
                    return;
                }
                if (dr.Field<int>("id_ПЗ") == _idPZ)
                {
                    dr.Delete();
                }
            }
            DB _dBOP;
            string commandFillOP = "select * from операция where id_Маршрута = (select Маршрут from нп where id_НП = (select id_НП from пз where id_ПЗ = " + _idPZ + "));";
            DataTable OP = new DataTable();
            //string commandFillNP = "select id_НП from нп; ";
            _dBOP = new DB(OP);
            _dBOP.AddCommandSelectTable(commandFillOP);
            _dBOP.FillTable();

            foreach (DataRow drop in OP.Rows)
            {
                DataRow dr = DT.NewRow();
                //DT.Rows["id_ПЗ"] =
                dr["id_ПЗ"] = _idPZ;
                dr["id_Операция"] = drop.Field<int>("id_Операция");
                dr["РЦ"] = drop.Field<int>("РЦ");
                dr["план (мин)"] = drop.Field<int>("Время_обработки") + drop.Field<int>("Время_перехода") + drop.Field<int>("Время_наладки");
                DT.Rows.Add(dr);
            }
        }
    }
}
