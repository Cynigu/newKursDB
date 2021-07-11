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
            sum += ComputeSumPeriodOj();
            TimeSpan t = TimeSpan.FromMinutes(sum);
            DateEnd = DateStart + t;
        }
        public int ComputeSumPeriodOj()
        {
            DataTable DT = new DataTable();
            DT.Columns.Add("id_НП").DataType = Type.GetType("System.Int32");
            DT.Columns.Add("Кол-во (план)").DataType = Type.GetType("System.Int32"); 

            // Таблица нп
            DB _dBNP;
            string commandFillNP = "select * from нп";
            DataTable NP1 = new DataTable();
            _dBNP = new DB(NP1);
            _dBNP.AddCommandSelectTable(commandFillNP);
            _dBNP.FillTable();
            // Таблица компонентов
            DB _dBKOM;
            string commandFillKOM = "select * from компонент";
            DataTable KOM = new DataTable();
            _dBKOM = new DB(KOM);
            _dBKOM.AddCommandSelectTable(commandFillKOM);
            _dBKOM.FillTable();

            int idBaseNP = SelectedNP.Row.Field<int>("id_НП");
            int idBaseCp = NP1.Select("id_НП = " + idBaseNP)[0].Field<int>("Спецификация");
            List<List<int>> idCP = new List<List<int>>();
            List<int> checkCP = new List<int>();
            checkCP.Add(idBaseCp);

            idCP.Add(new List<int>());
            idCP[0].Add(idBaseCp);
            idCP[0].Add(Quantity);
            while (idCP.Count != 0)
            {
                foreach (DataRow drkom in KOM.Select("Спецификация =" + idCP[0][0]))
                {
                    DataRow dr = DT.NewRow();

                    dr["id_НП"] = drkom.Field<int>("НП");
                    dr["Кол-во (план)"] = drkom.Field<int>("Кол-во") * idCP[0][1];

                    int idnp = drkom.Field<int>("НП");
                    if (!NP1.Select("id_НП =" + idnp)[0].IsNull("Спецификация"))
                    {
                        int idcp = NP1.Select("id_НП =" + idnp)[0].Field<int>("Спецификация");
                        if (checkCP.Contains(idcp))
                        {
                            MessageBox.Show("НП данного заказа имеет некорректную спецификацию");
                            return 0;
                        }
                        else checkCP.Add(idcp);
                        idCP.Add(new List<int>());
                        idCP[idCP.Count - 1].Add(idcp);
                        idCP[idCP.Count - 1].Add(idCP[0][1] * drkom.Field<int>("Кол-во"));
                    }
                    string sel = "id_НП =" + idnp;
                    if (DT.Select(sel).Length > 0)
                    {
                        DT.Select(sel)[0]["Кол-во (план)"] = DT.Select(sel)[0].Field<int>("Кол-во (план)")
                            + dr.Field<int>("Кол-во (план)");
                    }
                    else DT.Rows.Add(dr);
                }
                idCP.RemoveAt(0);
            }

            // Таблица запасов
            DB _dBZ;
            string commandFillZ = "select * from запас";
            DataTable Z = new DataTable();
            _dBZ = new DB(Z);
            _dBZ.AddCommandSelectTable(commandFillZ);
            _dBZ.FillTable();

            int sum = 0;
            for (int i =0; i < DT.Rows.Count; i++)
            {
                int np = DT.Rows[i].Field<int>("id_НП");
                string sel = "id_НП =" + np + " and Метод_возобновления = 1";

                if (DT.Rows[i].Field<int>("Кол-во (план)") > Z.Select("НП =" + np.ToString())[0].Field<int>("Кол-во")
                    && NP1.Select(sel).Length > 0)
                {
                    sum += NP1.Select("id_НП =" + np)[0].Field<int>("Период_ожидания");
                }
            }
            return sum;
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
