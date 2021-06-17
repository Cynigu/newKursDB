using kurs.models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace kurs.model
{
    public class UOMmodel : ReactiveObject, ImodelTable
    {
        private readonly int _idPZ;

        public UOMmodel(int idpz)
        {
            _idPZ = idpz;

            DB _dBOP;
            string commandFillOP = "select * from операция where id_Маршрута = (select Маршрут from нп where id_НП = (select id_НП from пз where id_ПЗ = " + _idPZ + "));";
            OP = new DataTable();
            //string commandFillNP = "select id_НП from нп; ";
            _dBOP = new DB(OP);
            _dBOP.AddCommandSelectTable(commandFillOP);
            _dBOP.FillTable();

            DB _dBRC;
            string commandFillRC = "select * from РЦ";
            RC = new DataTable();
            //string commandFillNP = "select id_НП from нп; ";
            _dBRC = new DB(RC);
            _dBRC.AddCommandSelectTable(commandFillRC);
            _dBRC.FillTable();
        }

        #region Fields
        private DataTable op;
        private DataRowView selectedOP;
        private DataTable rc;
        private DataRowView selectedRC;
        private int timefact;
        private int timeplan;
        #endregion

        #region Properties
        public DataTable OP
        {
            get { return op; }
            set { this.RaiseAndSetIfChanged(ref op, value); }
        }
        public DataRowView SelectedOP
        {
            get
            {
                return selectedOP;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref selectedOP, value);
            }
        }

        public DataTable RC
        {
            get { return rc; }
            set { this.RaiseAndSetIfChanged(ref rc, value); }
        }
        public DataRowView SelectedRC
        {
            get
            {
                return selectedRC;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref selectedRC, value);
            }
        }

        public int TimeFact
        {
            get
            {
                return timefact;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref timefact, value);
            }
        }

        public int TimePlan
        {
            get
            {
                return timeplan;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref timeplan, value);
            }
        }
        #endregion

        #region Methods
        public void RowFromModelToTable(DataRow rowForEdit)
        {
            rowForEdit["id_ПЗ"] = _idPZ;
            if (SelectedOP != null)
            {
                rowForEdit["id_Операция"] = SelectedOP.Row.Field<int>("id_Операция");
                DataRow drop = OP.Select("id_Операция =" + SelectedOP.Row.Field<int>("id_Операция"))[0];
                TimePlan = drop.Field<int>("Время_обработки") + drop.Field<int>("Время_перехода") + drop.Field<int>("Время_наладки");
                rowForEdit["план (мин)"] = TimePlan;
            }
            if (SelectedRC != null)
            {
                rowForEdit["РЦ"] = SelectedRC.Row.Field<int>("id_РЦ");
            }
            rowForEdit["факт (мин)"] = TimeFact;
        }

        public void RowFromTableToModel(DataRow rowForEdit)
        {
            if (!rowForEdit.IsNull("id_Операция"))
            {
                int m = int.Parse(rowForEdit["id_Операция"].ToString());
                SelectedOP = OP.DefaultView[OP.Rows.IndexOf(OP.Select("id_Операция =" + m)[0])];
            }
            if (!rowForEdit.IsNull("РЦ"))
            {
                int m = int.Parse(rowForEdit["РЦ"].ToString());
                SelectedRC = RC.DefaultView[RC.Rows.IndexOf(RC.Select("id_РЦ =" + m)[0])];
            }

            if (!rowForEdit.IsNull("факт (мин)"))
            {
                TimeFact = int.Parse(rowForEdit["факт (мин)"].ToString());
            }
        }

        public bool CheckDataForEdit()
        {
            if (SelectedOP == null)
            {
                MessageBox.Show("Введите операцию");
                return false;
            }
            if (SelectedRC == null)
            {
                MessageBox.Show("Введите Рабочий центр");
                return false;
            }
            return true;
        }

        public bool CheckDataForAdd()
        {
            if (SelectedOP == null)
            {
                MessageBox.Show("Введите операцию");
                return false;
            }
            if (SelectedRC == null)
            {
                MessageBox.Show("Введите Рабочий центр");
                return false;
            }
            return true;
        }

        #endregion
    }
}
