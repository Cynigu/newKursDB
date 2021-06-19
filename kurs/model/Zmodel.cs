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
    class Zmodel : ReactiveObject, ImodelTable
    {
        #region Fields
        private DataTable sklad;
        private DataTable np;
        private DataRowView selectedSklad;
        private DataRowView selectedNP;
        private int quantity;
        #endregion
        #region Properties
        public DataTable Sklad
        {
            get { return sklad; }
            set { this.RaiseAndSetIfChanged(ref sklad, value); }
        }
        public DataTable NP
        {
            get { return np; }
            set { this.RaiseAndSetIfChanged(ref np, value); }
        }
        public DataRowView SelectedSklad
        {
            get { return selectedSklad; }
            set { this.RaiseAndSetIfChanged(ref selectedSklad, value); }
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

        public Zmodel()
        {
            Sklad = new DataTable();
            string commandFillSklad = "select * from склад;";
            DB _dBSklad = new DB(Sklad);
            _dBSklad.AddCommandSelectTable(commandFillSklad);
            _dBSklad.FillTable();

            NP = new DataTable();
            string commandFillNP = "select * from нп;";
            DB _dBNP = new DB(NP);
            _dBNP.AddCommandSelectTable(commandFillNP);
            _dBNP.FillTable();
        }
        public bool CheckDataForAdd()
        {
            if (SelectedNP == null)
            {
                MessageBox.Show("Введите номер номенклатурной позиции");
                return false;
            }
            if (SelectedSklad == null)
            {
                MessageBox.Show("Введите номер склада");
                return false;
            }
            return true;
        }

        public bool CheckDataForEdit()
        {
            if (SelectedNP == null)
            {
                MessageBox.Show("Введите номер номенклатурной позиции");
                return false;
            }
            if (SelectedSklad == null)
            {
                MessageBox.Show("Введите номер склада");
                return false;
            }
            return true;
        }

        public void Clear()
        {
            SelectedNP = null;
            SelectedSklad = null;
            Quantity = 0;
        }

        public void RowFromModelToTable(DataRow rowForEdit)
        {
            if (SelectedSklad != null) {
                rowForEdit["Склад"] = SelectedSklad.Row.Field<int>("id_Склад");
            }
            if(SelectedNP != null) rowForEdit["НП"] = SelectedNP.Row.Field<int>("id_НП");
            rowForEdit["Кол-во"] = Quantity;
        }

        public void RowFromTableToModel(DataRow rowForEdit)
        {
            if(!rowForEdit.IsNull("Склад")) 
                SelectedSklad = Sklad.DefaultView[Sklad.Rows.IndexOf(Sklad.Select("`id_Склад` =" + rowForEdit.Field<int>("Склад"))[0])];
            if (!rowForEdit.IsNull("Склад"))
                SelectedNP = NP.DefaultView[NP.Rows.IndexOf(NP.Select("id_НП =" + rowForEdit.Field<int>("НП"))[0])];
            Quantity = rowForEdit.Field<int>("Кол-во");
        }
    }
}
