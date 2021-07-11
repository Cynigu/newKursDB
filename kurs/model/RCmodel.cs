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
    class RCmodel : ReactiveObject, ImodelTable
    {
        #region Fields
        private string description;
        private int power;
        #endregion
        #region Properties
        public string Description
        {
            get { return description; }
            set { this.RaiseAndSetIfChanged(ref description, value); }
        }
        public int Power
        {
            get { return power; }
            set { this.RaiseAndSetIfChanged(ref power, value); }
        }
        #endregion

        #region Methods
        public bool CheckDataForAdd()
        {
            if (Description == null || Description == "")
            {
                MessageBox.Show("Введите описание");
                return false;
            }

            if (Power <= 0)
            {
                MessageBox.Show("Некорректно введена мощность");
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

            if (Power <= 0)
            {
                MessageBox.Show("Некорректно введена мощность");
                return false;
            }
            return true;
        }

        public void Clear()
        {
            Description = "";
            Power = 0;
        }

        public void RowFromModelToTable(DataRow rowForEdit)
        {
            rowForEdit["Описание"] = Description;
            rowForEdit["Ежедневная_мощность"] = Power;
        }

        public void RowFromTableToModel(DataRow rowForEdit)
        {
            Description = rowForEdit["Описание"].ToString();

            Power = int.Parse(rowForEdit["Ежедневная_мощность"].ToString());
        }
        #endregion
    }
}
