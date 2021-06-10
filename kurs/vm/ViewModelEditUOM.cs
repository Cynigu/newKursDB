using kurs.commands;
using kurs.models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace kurs.vm
{
    class ViewModelEditUOM : ViewModelEditAddBase
    {
        #region Fields
        private bool isAddEnable;
        private DataTable op;
        private DataRowView selectedOP;
        private DateTime? dateStart;
        private DateTime? dateEnd;
        public DateTime? DateStart
        {
            get
            {
                return dateStart;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref dateStart, value);
            }
        }
        public DateTime? DateEnd
        {
            get { return dateEnd; }
            set { this.RaiseAndSetIfChanged(ref dateEnd, value); }
        }


        #endregion
        #region Properties
        public bool IsAddEnable
        {
            get { return isAddEnable; }
            set { this.RaiseAndSetIfChanged(ref isAddEnable, value); }
        }

        public DataTable OP
        {
            get { return op; }
            set { this.RaiseAndSetIfChanged(ref op, value); }
        }
        public DataRowView SelectedOP
        {
            get
            {
                return selectedOP;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref selectedOP, value);
            }
        }

        #endregion
        #region commands
        private RelayCommand _computeDataEndCommand;
        public ICommand ComputeDataEndCommand
        {
            get
            {
                return _computeDataEndCommand ??
                  (_computeDataEndCommand = new RelayCommand(obj =>
                  {
                      DateEnd = DateTime.Now;
                  }));
            }
        }
        private RelayCommand _computeDataStartCommand;
        public ICommand ComputeDataStartCommand
        {
            get
            {
                return _computeDataStartCommand ??
                  (_computeDataStartCommand = new RelayCommand(obj =>
                  {
                      DateStart = DateTime.Now;
                  }));
            }
        }

        #endregion
        #region Constructor
        public ViewModelEditUOM(int idNP)
        {
            DB _dBOP;
            string commandFillNP = "select id_Операция from операция where id_Маршрута = (select Маршрут from нп where " +
                "id_НП="+ idNP+") ";
            OP = new DataTable();
            //string commandFillNP = "select id_НП from нп; ";
            _dBOP = new DB(OP);
            _dBOP.AddCommandSelectTable(commandFillNP);
            _dBOP.FillTable();
            DateStart = null;
            DateEnd = null;
        }
        #endregion

        public override void ChangeRowInTable(DataRow newRow)
        {
            if (SelectedOP != null)
            {
                newRow["id_Операции"] = SelectedOP.Row[0];
            }
            if (DateStart != null)
            {
                newRow["Начало работы"] = DateStart;
            }
            if (DateEnd != null)
            {
                newRow["Окончание работы"] = DateEnd;
            }
        }

        public override void CopyRowInWindow(DataRow temp)
        {
            if (!temp.IsNull("id_Операции"))
            {
                int m = int.Parse(temp["id_Операции"].ToString());
                SelectedOP = OP.DefaultView[OP.Rows.IndexOf(OP.Select("id_Операция =" + m)[0])];
            }
            if (!temp.IsNull("Начало работы"))
            {
                DateStart = DateTime.Parse(temp["Начало работы"].ToString());
            }
            if (!temp.IsNull("Окончание работы"))
            {
                DateEnd = DateTime.Parse(temp["Окончание работы"].ToString());
            }
        }
    }
}
