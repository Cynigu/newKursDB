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
    class OPmodel : ReactiveObject, ImodelTable
    {
        #region Constructors
        public OPmodel(int idmk)
        {
            this.idmk = idmk;
            OP = new DataTable();
            string commandFillOP = "select * from операция where id_Маршрута ="+ this.idmk + ";";
            DB _dBOP = new DB(OP);
            _dBOP.AddCommandSelectTable(commandFillOP);
            _dBOP.FillTable();

            RC = new DataTable();
            string commandFillRC = "select * from рц;";
            DB _dBRC = new DB(RC);
            _dBRC.AddCommandSelectTable(commandFillRC);
            _dBRC.FillTable();
        }
        #endregion

        #region Fields

        private int idmk;
        private string description;
        private int timeOBR;
        private int timePERE;
        private int timeNAL;

        private DataTable rc;
        private DataTable op;
        private DataRowView selectedrc;
        private DataRowView selectedop;

        #endregion

        #region Properities
        public DataTable RC
        {
            get { return rc; }
            set { this.RaiseAndSetIfChanged(ref rc, value); }
        }
        public DataTable OP
        {
            get { return op; }
            set { this.RaiseAndSetIfChanged(ref op, value); }
        }
        public DataRowView SelectedRC
        {
            get { return selectedrc; }
            set { this.RaiseAndSetIfChanged(ref selectedrc, value); }
        }
        public DataRowView SelectedOP
        {
            get { return selectedop; }
            set { this.RaiseAndSetIfChanged(ref selectedop, value); }
        }

        public string Description
        {
            get { return description; }
            set { this.RaiseAndSetIfChanged(ref description, value); }
        }
        public int TimeOBR
        {
            get { return timeOBR; }
            set { this.RaiseAndSetIfChanged(ref timeOBR, value); }
        }
        public int TimePERE
        {
            get { return timePERE; }
            set { this.RaiseAndSetIfChanged(ref timePERE, value); }
        }
        public int TimeNAL
        {
            get { return timeNAL; }
            set { this.RaiseAndSetIfChanged(ref timeNAL, value); }
        }
        #endregion

        public bool CheckDataForAdd()
        {
            if (Description == null || Description == "")
            {
                MessageBox.Show("Введите описание");
                return false;
            }
            if (TimeOBR == 0 || TimePERE ==0 || TimeNAL == 0)
            {
                MessageBox.Show("Введите кол-во минут для проведения операции");
                return false;
            }
            return true;
        }
        public bool CheckDataForEdit()
        {
            if (Description == null || Description == "")
            {
                MessageBox.Show("Введите описание");
                return false;
            }
            if (TimeOBR == 0 || TimePERE == 0 || TimeNAL == 0)
            {
                MessageBox.Show("Введите кол-во минут для проведения операции");
                return false;
            }
            return true;
        }
        public void Clear()
        {
            SelectedOP = null;
            SelectedRC = null;
            TimeOBR = 0;
            TimePERE = 0;
            TimeNAL = 0;
        }
        public void RowFromModelToTable(DataRow rowForEdit)
        {
            rowForEdit["id_Маршрута"] = idmk;
            if (SelectedOP != null)
            {
                if (SelectedOP.Row.Field<int>("id_Операция") == rowForEdit.Field<int>("id_Операция"))
                {
                    MessageBox.Show("Следующая операция совпадает с текущей");
                    return;
                }
                rowForEdit["Следующая операция"] = SelectedOP.Row.Field<int>("id_Операция");
            }
            rowForEdit["Описание"] = Description;
            rowForEdit["Время_обработки"] = TimeOBR;
            rowForEdit["Время_перехода"] = TimePERE;
            rowForEdit["Время_наладки"] = TimeNAL;
            if (SelectedRC != null)
            {
                rowForEdit["РЦ"] = SelectedRC.Row.Field<int>("id_РЦ");
            } 
        }

        public void RowFromTableToModel(DataRow rowForEdit)
        {
            idmk = rowForEdit.Field<int>("id_Маршрута");
            Description = rowForEdit.Field<string>("Описание");
            TimeOBR = rowForEdit.Field<int>("Время_обработки");
            TimePERE = rowForEdit.Field<int>("Время_перехода");
            TimeNAL = rowForEdit.Field<int>("Время_наладки");
            if (!rowForEdit.IsNull("РЦ"))
            {
                SelectedRC = RC.DefaultView[RC.Rows.IndexOf(RC.Select("id_РЦ =" + rowForEdit.Field<int>("РЦ"))[0])];
            }
            if (!rowForEdit.IsNull("Следующая операция"))
            {
                SelectedOP = OP.DefaultView[OP.Rows.IndexOf(OP.Select("id_Операция =" + rowForEdit.Field<int>("Следующая операция"))[0])];
            }
        }
    }
}
