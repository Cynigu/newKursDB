using kurs.model;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kurs.vm
{
    class ViewModelTableOP : AbstractViewModelTableBaseWithEdit
    {
        private OPmodel opmodel;
        public OPmodel OPModel 
        {
            get
            {
                return opmodel;
            }
            set { this.RaiseAndSetIfChanged(ref opmodel, value); }
        }

        private readonly int _idmk;
        public ViewModelTableOP(string SelectedCommandText, int idMK, string tablename = "Операции") : base(SelectedCommandText, tablename)
        {
            _idmk = idMK;
            InizialistModel();
        }

        public override void InizialistModel()
        {
            OPModel = new OPmodel(_idmk);
            ModelTable = OPModel;
        }
    }
}
