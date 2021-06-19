using kurs.models;
using kurs.service;
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
    public class PZmodel: ReactiveObject, ImodelTable
    {
        #region Fields
        private DataTable np;
        private DataTable status;

        private DataRowView selectedNP;
        private DataRowView selectedStatus;

        private string discription;
        private bool enDataEnd;
        private int quantity;
        private DateTime? dateStart;
        private DateTime? dateEnd;
        #endregion
        #region Properties

        public DataTable NP
        {
            get { return np; }
            set { this.RaiseAndSetIfChanged(ref np, value); }
        }
        public DataTable Status
        {
            get { return status; }
            set { this.RaiseAndSetIfChanged(ref status, value); }
        }
        public DataRowView SelectedNP
        {
            get
            {
                if (selectedNP == null) EnDataEnd = false;
                return selectedNP;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref selectedNP, value);
                if (selectedNP != null && DateStart != null) EnDataEnd = true;
                else EnDataEnd = false;
            }
        }
        public DataRowView SelectedStatus
        {
            get { return selectedStatus; }
            set { this.RaiseAndSetIfChanged(ref selectedStatus, value); }
        }
        public string Discription
        {
            get { return discription; }
            set { this.RaiseAndSetIfChanged(ref discription, value); }
        }
        public int Quantity
        {
            get { return quantity; }
            set { this.RaiseAndSetIfChanged(ref quantity, value); }
        }
        public DateTime? DateStart
        {
            get
            {
                if (dateStart == null) EnDataEnd = false;
                return dateStart;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref dateStart, value);
                if (selectedNP != null && DateStart != null) EnDataEnd = true;
                else EnDataEnd = false;
            }
        }
        public DateTime? DateEnd
        {
            get { return dateEnd; }
            set { this.RaiseAndSetIfChanged(ref dateEnd, value); }
        }
        public bool EnDataEnd
        {
            get { return enDataEnd; }
            set { this.RaiseAndSetIfChanged(ref enDataEnd, value); }
        }
        #endregion

        #region Constructors
        public PZmodel()
        {
            DB _dBNP;
            DB _dBStatus;

            NP = new DataTable();
            string commandFillNP = "select id_НП from нп inner join спецификация on нп.Спецификация =  " +
                "спецификация.id_Спецификация where Метод_возобновления =  " +
                "(select idМетод_возобновления from метод_возобновления where название = \"Производство\") " +
                "and Статус = (select idстатус_спецификации from статус_спецификации where название = \"сертифицирована\")"
                + " and Маршрут in (select id_МК from мк);";

            _dBNP = new DB(NP);
            _dBNP.AddCommandSelectTable(commandFillNP);
            _dBNP.FillTable();

            Status = new DataTable();
            string commandFillCp = "select * from статус_заказа;";
            _dBStatus = new DB(Status);
            _dBStatus.AddCommandSelectTable(commandFillCp);
            _dBStatus.FillTable();

            DateStart = null;
            DateEnd = null;
        }
        #endregion

        #region Methods
        public void RowFromModelToTable(DataRow rowForEdit)
        {
            rowForEdit["Описание"] = Discription;
            rowForEdit["Кол-во"] = Quantity;
            if (SelectedNP != null)
            {
                rowForEdit["id_НП"] = SelectedNP.Row[0];
            }
            if (SelectedStatus != null)
            {
                rowForEdit["Статус"] = SelectedStatus.Row[0];
            }
            if (DateStart != null)
            {
                rowForEdit["Дата_начала"] = DateStart;
            }
            if (DateEnd != null)
            {
                rowForEdit["Дата_завершения"] = DateEnd;
            }
        }

        public void RowFromTableToModel(DataRow rowForEdit)
        {
            Discription = rowForEdit["Описание"].ToString();
            Quantity = int.Parse(rowForEdit["Кол-во"].ToString());

            if (!rowForEdit.IsNull("id_НП"))
            {
                int m = int.Parse(rowForEdit["id_НП"].ToString());
                SelectedNP = NP.DefaultView[NP.Rows.IndexOf(NP.Select("id_НП =" + m)[0])];
            }
            if (!rowForEdit.IsNull("Статус"))
            {
                int c = int.Parse(rowForEdit["Статус"].ToString());
                SelectedStatus = Status.DefaultView[Status.Rows.IndexOf(Status.Select("idСтатус_Заказа =" + c)[0])];
            }
            if (!rowForEdit.IsNull("Дата_начала"))
            {
                DateStart = DateTime.Parse(rowForEdit["Дата_начала"].ToString());
            }
            if (!rowForEdit.IsNull("Дата_завершения"))
            {
                DateEnd = DateTime.Parse(rowForEdit["Дата_завершения"].ToString());
            }
        }
        public void ComputeDataEnd()
        {
            if (DateStart == null)
            {
                MessageBox.Show("Введите дату начала");
                return;
            }
            if (SelectedNP == null)
            {
                MessageBox.Show("Введите НП");
                return;
            }
            if (Quantity <= 0)
            {
                MessageBox.Show("Введите кол-во");
                return;
            }
            string query = "compute_sum";
            int idNP = int.Parse(SelectedNP.Row["id_НП"].ToString());


            DBFunction dbFunc = new DBFunction();
            int count = Quantity;
            int sum = dbFunc.CallFuncCamputeSum(query, idNP, count);
            TimeSpan t = TimeSpan.FromMinutes(sum);
            DateEnd = DateStart + t;
        }

        public bool CheckDataForEdit()
        {
            if (SelectedNP == null)
            {
                MessageBox.Show("Введите НП");
                return false;
            }
            if (Quantity <= 0)
            {
                MessageBox.Show("Введите кол-во");
                return false;
            }
            if (SelectedStatus == null)
            {
                MessageBox.Show("Введите кол-во");
                return false;
            }
            return true;
        }

        public bool CheckDataForAdd()
        {
            if (SelectedNP == null)
            {
                MessageBox.Show("Введите НП");
                return false;
            }
            if (Quantity <= 0)
            {
                MessageBox.Show("Введите кол-во");
                return false;
            }
            if (SelectedStatus == null)
            {
                MessageBox.Show("Введите статус");
                return false;
            }
            return true;
        }

        public void Clear()
        {
            Discription = "";
            Quantity = 0;

            SelectedNP = null;
            SelectedStatus = null;
            DateStart = null;
            DateEnd = null;
        }
        #endregion
    }
}
