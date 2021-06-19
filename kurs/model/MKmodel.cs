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
    class MKmodel : ReactiveObject, ImodelTable
    {
        #region Fields
        DateTime? datechanges;
        string status;
        #endregion

        #region Properties
        public DateTime? DateChanges 
        {
            get { return datechanges; }
            set { this.RaiseAndSetIfChanged(ref datechanges, value); }
        }

        public String Status
        {
            get { return status; }
            set { this.RaiseAndSetIfChanged(ref status, value); }
        }

        #endregion


        public bool CheckDataForAdd()
        {
            if (Status == "" || Status == null)
            {
                MessageBox.Show("Введите статус маршрутной карты");
                return false;
            }
            return true;
        }

        public bool CheckDataForEdit()
        {
            if (Status == "" || Status == null)
            {
                MessageBox.Show("Введите статус маршрутной карты");
                return false;
            }
            return true;
        }

        public void Clear()
        {
            Status = null;
            DateChanges = null;
        }

        public void RowFromModelToTable(DataRow rowForEdit)
        {
            rowForEdit["Статус"] = Status;
            DateChanges = DateTime.Now;
            rowForEdit["Дата_изменения"] = DateChanges;
        }

        public void RowFromTableToModel(DataRow rowForEdit)
        {
            DateChanges = rowForEdit.Field<DateTime>("Дата_изменения");
            Status = rowForEdit.Field<string>("Статус");
        }
    }
}
