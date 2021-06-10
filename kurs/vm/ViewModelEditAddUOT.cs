using kurs.models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace kurs.vm
{
    class ViewModelEditAddUOT : ViewModelEditAddBase
    {
        #region Fields
        private bool isAddEnable;
        private DataTable np;
        private DataTable zapas;
        private DataRowView selectedNP;
        private int quantity;
        private string strID;
        #endregion
        #region Properties
        public bool IsAddEnable 
        {
            get { return isAddEnable; }
            set { this.RaiseAndSetIfChanged(ref isAddEnable, value); }
        }

        public string StrId
        {
            get { return strID; }
            set { this.RaiseAndSetIfChanged(ref strID, value); }
        }
        public DataTable NP
        {
            get { return np; }
            set { this.RaiseAndSetIfChanged(ref np, value); }
        }
        public DataRowView SelectedNP
        {
            get
            {
                return selectedNP;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref selectedNP, value);
            }
        }
        public int Quantity
        {
            get { 
                return quantity; }
            set {
                
                this.RaiseAndSetIfChanged(ref quantity, value); 
            }
        }
        #endregion

        #region Constructors

        public ViewModelEditAddUOT(string commandFillNP, string id)
        {
            DB _dBNP;
            DB _dBZapas;
            zapas = new DataTable();
            NP = new DataTable();
            string commandFillZapas = "select * from запас; ";
            StrId = id;
            _dBNP = new DB(NP);
            _dBZapas = new DB(zapas);
            _dBZapas.AddCommandSelectTable(commandFillZapas);
            _dBZapas.FillTable();
            _dBNP.AddCommandSelectTable(commandFillNP);
            _dBNP.FillTable();
        }
        #endregion
        public bool ChangeRowInTableSpisanie(DataRow newRow)
        {
            if (SelectedNP != null)
            {
                newRow["id_НП"] = SelectedNP.Row[0];
            }
            int maxQ = zapas.Select("НП=" + SelectedNP.Row[0].ToString())[0].Field<int>("Кол-во");
            if (maxQ < Quantity)
            {
                return false;
            }
            newRow["Кол-во"] = Quantity;
            return true;
        }
        public override void ChangeRowInTable(DataRow newRow)
        {
            if (SelectedNP != null)
            {
                newRow["id_НП"] = SelectedNP.Row[0];
            }
            newRow["Кол-во"] = Quantity;
        }

        public override void CopyRowInWindow(DataRow temp)
        {
            Quantity = int.Parse(temp["Кол-во"].ToString());
            if (!temp.IsNull("id_НП"))
            {
                int m = int.Parse(temp["id_НП"].ToString());
                SelectedNP = NP.DefaultView[NP.Rows.IndexOf(NP.Select("НП =" + m)[0])];
            }
        }
    }
}
