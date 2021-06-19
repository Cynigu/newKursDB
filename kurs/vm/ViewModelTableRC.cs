using kurs.model;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kurs.vm
{
    class ViewModelTableRC : AbstractViewModelTableBaseWithEdit
    {
        public ViewModelTableRC(string SelectedCommandText, string t = "Рабочие центры") : base(SelectedCommandText, t)
        {
        }

        #region fields
        RCmodel rcmodel;
        #endregion
        #region Properties
        public RCmodel RCModel
        {
            get { return rcmodel; }
            set { this.RaiseAndSetIfChanged(ref rcmodel, value); }
        }
        #endregion
        public override void InizialistModel()
        {
            RCModel = new RCmodel();
            ModelTable = RCModel;
        }
    }
}
