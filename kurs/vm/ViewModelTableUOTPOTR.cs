using kurs.model;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kurs.vm
{
    class ViewModelTableUOTPOTR : AbstractViewModelTableUOT
    {
        public ViewModelTableUOTPOTR(string SelectedCommandText, int idPZ, int qPZ, string t = "select * from товарные_операции_потребление") : base(SelectedCommandText, idPZ, qPZ, t)
        {
            InizialistModel();
        }
        #region fields
        UOTPOTRmodel uotpotrmodel;
        #endregion
        #region Properties
        public UOTPOTRmodel UOTpotrModel
        {
            get { return uotpotrmodel; }
            set { this.RaiseAndSetIfChanged(ref uotpotrmodel, value); }
        }
        #endregion


        #region Methods

        public override void InizialistModel()
        {
            UOTpotrModel = new UOTPOTRmodel(_idPZ, _qPZ);
            ModelTable = UOTpotrModel;
        }
        #endregion
    }
}
