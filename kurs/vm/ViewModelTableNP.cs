using kurs.model;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kurs.vm
{
    class ViewModelTableNP : AbstractViewModelTableBaseWithEdit
    {
        public ViewModelTableNP(string SelectedCommandText, string tablename = "Номенклатурные позиции") : base(SelectedCommandText, tablename)
        {
        }

        #region fields
        NPmodel npmodel;
        #endregion
        #region Properties
        public NPmodel NPModel
        {
            get { return npmodel; }
            set { this.RaiseAndSetIfChanged(ref npmodel, value); }
        }
        #endregion
        public override void InizialistModel()
        {
            NPModel = new NPmodel();
            ModelTable = NPModel;
        }
    }
}
