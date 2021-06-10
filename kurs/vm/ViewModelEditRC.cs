using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kurs.vm
{
    class ViewModelEditRC : ReactiveObject
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

        #region methods
        public void ChangeRowInTable(DataRow newRow)
        {
            newRow["Описание"] = Description;
            newRow["Ежедневная_мощность"] = Power;
        }

        public void CopyRowInWindow(DataRow temp)
        {

            Description = temp["Описание"].ToString();

            Power = int.Parse(temp["Ежедневная_мощность"].ToString());
            
        }
        #endregion
    }
}
