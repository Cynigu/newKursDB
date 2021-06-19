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
    class CPmodel : ReactiveObject, ImodelTable
    {
        #region Fields

        private DataTable status;
        private readonly DB _dBStatus;

        private DataRowView selectedStatus;

        private DateTime? dateStart;

        #endregion

        #region Properties
        public DataTable Status
        {
            get { return status; }
            set { this.RaiseAndSetIfChanged(ref status, value); }
        }

        public DataRowView SelectedStatus
        {
            get { return selectedStatus; }
            set { this.RaiseAndSetIfChanged(ref selectedStatus, value); }
        }

        public DateTime? DateStart
        {
            get { return dateStart; }
            set { this.RaiseAndSetIfChanged(ref dateStart, value); }
        }

        #endregion

        #region Constructors
        public CPmodel()
        {
            Status = new DataTable();
            _dBStatus = new DB(Status);
            string commandFillCp = "select * from статус_спецификации;";
            _dBStatus.AddCommandSelectTable(commandFillCp);
            _dBStatus.FillTable();
            DateStart = null;
        }

        #endregion

        public bool CheckDataForAdd()
        {
            if (SelectedStatus == null)
            {
                MessageBox.Show("Введите статус");
                return false;
            }
            return true;
        }

        public bool CheckDataForEdit()
        {
            if (SelectedStatus == null)
            {
                MessageBox.Show("Введите статус");
                return false;
            }
            return true;
        }

        public void Clear()
        {
            SelectedStatus = null;
            DateStart = null;
        }

        public void RowFromModelToTable(DataRow rowForEdit)
        {
            DateStart = DateTime.Now;
            if (SelectedStatus != null)
            {
                rowForEdit["Статус"] = SelectedStatus.Row[0];
            }
            if (DateStart != null)
            {
                rowForEdit["Дата_изменения"] = DateStart;
            }
        }

        public void RowFromTableToModel(DataRow rowForEdit)
        {
            if (!rowForEdit.IsNull("Статус"))
            {
                int c = int.Parse(rowForEdit["Статус"].ToString());
                SelectedStatus = Status.DefaultView[Status.Rows.IndexOf(Status.Select("idстатус_спецификации =" + c)[0])];
            }
            if (!rowForEdit.IsNull("Дата_изменения"))
            {
                DateStart = DateTime.Parse(rowForEdit["Дата_изменения"].ToString());
            }
        }
    }
}
