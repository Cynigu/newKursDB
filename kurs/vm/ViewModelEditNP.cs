using kurs.models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// вм для работы с окном редактирования для таблицы НП

namespace kurs.vm
{
    public class ViewModelEditNP : ViewModelEditAddBase
    {
        #region Fields
        private DataTable mk;
        private DataTable cp;
        private DataTable methodVozobnovlen;
        private DataTable methodSpisania;

        private readonly DB _dBMK;
        private readonly DB _dBCP;
        private readonly DB _dBMetVozob;
        private readonly DB _dBSpisania;

        private DataRowView selectedMK;
        private DataRowView  selectedCp;
        private DataRowView selectedMethodWriteOff;
        private DataRowView selectedRenewals;
        private string name;
        private string unitM;
        private int point;
        private int quantity;
        private int period;

        private bool isEnableMKCP;
        private bool isEnablePoint;

        #endregion

        #region Properties
        public DataTable MK 
        {
            get { return mk; }
            set { this.RaiseAndSetIfChanged(ref mk, value); }
        }
        public DataTable CP
        {
            get { return cp; }
            set { this.RaiseAndSetIfChanged(ref cp, value); }
        }

        public DataTable MethodVozobnovlen
        {
            get { return methodVozobnovlen; }
            set {
                this.RaiseAndSetIfChanged(ref methodVozobnovlen, value);}
        }
        public DataTable MethodSpisania
        {
            get { return methodSpisania; }
            set { this.RaiseAndSetIfChanged(ref methodSpisania, value); }
        }
        public DataRowView SelectedMK
        {
            get { return selectedMK; }
            set { this.RaiseAndSetIfChanged(ref selectedMK, value); }
        }
        public DataRowView SelectedCp
        {
            get { return selectedCp; }
            set { this.RaiseAndSetIfChanged(ref selectedCp, value); }
        }
        public DataRowView SelectedMethodWriteOff
        {
            get { return selectedMethodWriteOff; }
            set { this.RaiseAndSetIfChanged(ref selectedMethodWriteOff, value); }
        }
        public DataRowView SelectedRenewals
        {
            get { return selectedRenewals; }
            set {
                
                this.RaiseAndSetIfChanged(ref selectedRenewals, value);
                if (SelectedRenewals.Row["название"].ToString() == "Покупка")
                {
                    IsEnableMKCP = false;
                    IsEnablePoint = true;
                }
                else
                {
                    IsEnableMKCP = true;
                    IsEnablePoint = false;
                }
            }
        }
        public string Name
        {
            get { return name; }
            set { this.RaiseAndSetIfChanged(ref name, value); }
        }
        public string UnitM
        {
            get { return unitM; }
            set { this.RaiseAndSetIfChanged(ref unitM, value); }
        }
        public int Point
        {
            get { return point; }
            set { this.RaiseAndSetIfChanged(ref point, value); }
        }
        public int Quantity
        {
            get { return quantity; }
            set { this.RaiseAndSetIfChanged(ref quantity, value); }
        }
        public int Period
        {
            get { return period; }
            set { this.RaiseAndSetIfChanged(ref period, value); }
        }

        public bool IsEnableMKCP 
        {
            get { return isEnableMKCP; }
            set { this.RaiseAndSetIfChanged(ref isEnableMKCP, value); }
        }

        public bool IsEnablePoint
        {
            get { return isEnablePoint; }
            set { this.RaiseAndSetIfChanged(ref isEnablePoint, value); }
        }

        #endregion

        #region Constructors
        public ViewModelEditNP()
        {
            MK = new DataTable();
            string commandFillMK = "select id_МК from мк;";
            _dBMK = new DB(MK);
            _dBMK.AddCommandSelectTable(commandFillMK);
            _dBMK.FillTable();

            CP = new DataTable();
            string commandFillCp = "select id_Спецификация from спецификация;";

            _dBCP = new DB(CP);
            _dBCP.AddCommandSelectTable(commandFillCp);
            _dBCP.FillTable();

            MethodSpisania = new DataTable();
            string commandFillSpisania = "select * from метод_списания";
            _dBSpisania = new DB(MethodSpisania);
            _dBSpisania.AddCommandSelectTable(commandFillSpisania);
            _dBSpisania.FillTable();

            MethodVozobnovlen = new DataTable();
            string commandFillVozobnov = "select * from метод_возобновления";
            _dBMetVozob = new DB(MethodVozobnovlen);
            _dBMetVozob.AddCommandSelectTable(commandFillVozobnov);
            _dBMetVozob.FillTable();

            IsEnableMKCP = true;
        }

        #endregion
        #region methods
        public override void ChangeRowInTable(DataRow newRow)
        {
            if (SelectedMethodWriteOff != null) {
                newRow["Метод_списания"] = SelectedMethodWriteOff[0];
            }
            newRow["Наименование"] = Name;
            newRow["Ед_изм"] = UnitM;
            if (SelectedRenewals != null) {
                newRow["Метод_возобновления"] = SelectedRenewals[0];
            }
            newRow["Точка_доказа"] = Point;
            newRow["Кол-во_для_доказа"] = Quantity;
            newRow["Период_ожидания"] = Period;
            if (SelectedMK != null)
            {
                newRow["Маршрут"] = SelectedMK.Row[0];
            }
            if (SelectedCp != null)
            {
                newRow["Спецификация"] = SelectedCp.Row[0];
            }
        }

        public override void CopyRowInWindow(DataRow temp)
        {
            Name = temp["Наименование"].ToString();
            UnitM = temp["Ед_изм"].ToString();
            Point = int.Parse(temp["Точка_доказа"].ToString());
            Quantity = int.Parse(temp["Кол-во_для_доказа"].ToString());
            Period = int.Parse(temp["Период_ожидания"].ToString());
            if (!temp.IsNull("Метод_возобновления"))
            {
                int m = int.Parse(temp["Метод_возобновления"].ToString());
                SelectedRenewals = MethodVozobnovlen.DefaultView[
                    MethodVozobnovlen.Rows.IndexOf(
                        MethodVozobnovlen.Select("idМетод_возобновления =" + m)[0])];
            }
            if (!temp.IsNull("Метод_списания"))
            {
                int m = int.Parse(temp["Метод_списания"].ToString());
                SelectedMethodWriteOff = MethodSpisania.DefaultView[
                    MethodSpisania.Rows.IndexOf(
                        MethodSpisania.Select("idМетод_списания =" + m)[0])];
            }
            if (!temp.IsNull("Маршрут"))
            {
                int m = int.Parse(temp["Маршрут"].ToString());
                SelectedMK = MK.DefaultView[MK.Rows.IndexOf(MK.Select("id_МК =" + m)[0])];
            }
            if (!temp.IsNull("Спецификация"))
            {
                int c = int.Parse(temp["Спецификация"].ToString());
                SelectedCp = CP.DefaultView[CP.Rows.IndexOf(CP.Select("id_Спецификация =" + c)[0])];
            }
        }
        #endregion
    }
}
