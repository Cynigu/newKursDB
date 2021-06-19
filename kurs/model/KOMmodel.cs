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
    class KOMmodel : ReactiveObject, ImodelTable
    {
        #region Constructors
        readonly int _idcp;

        public KOMmodel(int idCP)
        {
            DB _dBNP;
            _idcp = idCP;
            NP = new DataTable();
            string commandFillMK = "select id_НП from нп;";
            _dBNP = new DB(NP);
            _dBNP.AddCommandSelectTable(commandFillMK);
            _dBNP.FillTable();
        }
        #endregion

        #region Fields
        private DataTable np;

        private DataRowView selectedNP;

        private int quantity;

        #endregion

        #region Properties
        public DataTable NP
        {
            get { return np; }
            set { this.RaiseAndSetIfChanged(ref np, value); }
        }
        public DataRowView SelectedNP
        {
            get { return selectedNP; }
            set { this.RaiseAndSetIfChanged(ref selectedNP, value); }
        }

        public int Quantity
        {
            get { return quantity; }
            set { this.RaiseAndSetIfChanged(ref quantity, value); }
        }

        #endregion

        public bool CheckDataForAdd()
        {
            if (SelectedNP == null)
            {
                MessageBox.Show("Введите номер НП");
                return false;
            }
            if (Quantity == 0)
            {
                MessageBox.Show("Введите кол-во");
                return false;
            }
            return true;
        }

        public bool CheckDataForEdit()
        {
            if (SelectedNP == null)
            {
                MessageBox.Show("Введите номер НП");
                return false;
            }
            if (Quantity == 0)
            {
                MessageBox.Show("Введите кол-во");
                return false;
            }
            return true;
        }

        public void Clear()
        {
            SelectedNP = null;
            Quantity = 0;
        }

        public void RowFromModelToTable(DataRow rowForEdit)
        {
            rowForEdit["Спецификация"] = _idcp;
            rowForEdit["Кол-во"] = Quantity;
            if (SelectedNP != null)
            {
                rowForEdit["НП"] = SelectedNP.Row.Field<int>("id_НП");
            }
        }

        public void RowFromTableToModel(DataRow rowForEdit)
        {
            Quantity = int.Parse(rowForEdit["Кол-во"].ToString());
            if (!rowForEdit.IsNull("НП"))
            {
                int m = int.Parse(rowForEdit["НП"].ToString());
                SelectedNP = NP.DefaultView[NP.Rows.IndexOf(NP.Select("id_НП =" + m)[0])];
            }
        }
    }
}
