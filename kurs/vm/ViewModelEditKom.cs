using kurs.models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kurs.vm
{
    class ViewModelEditKom : ViewModelEditAddBase
    {
        #region Fields
        private DataTable np;


        private readonly DB _dBNP;

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

        public DateTime EditData { get; set; }

        #endregion

        #region Constructors
        public ViewModelEditKom()
        {

            

            NP = new DataTable();
            string commandFillMK = "select id_НП from нп;";
            _dBNP = new DB(NP);
            _dBNP.AddCommandSelectTable(commandFillMK);
            _dBNP.FillTable();

        }

        #endregion

        #region methods
        public override void ChangeRowInTable(DataRow rowForEdit)
        {
            EditData = DateTime.Now;
            rowForEdit["Кол-во"] = Quantity;
            if (SelectedNP != null)
            {
                rowForEdit["НП"] = SelectedNP.Row[0];
            }
        }

        public override void CopyRowInWindow(DataRow rowForEdit)
        {
            Quantity = int.Parse(rowForEdit["Кол-во"].ToString());

            if (!rowForEdit.IsNull("НП"))
            {
                int m = int.Parse(rowForEdit["НП"].ToString());
                SelectedNP = NP.DefaultView[NP.Rows.IndexOf(NP.Select("id_НП =" + m)[0])];
            }
        }
        #endregion
    }
}
