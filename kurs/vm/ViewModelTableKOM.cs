using kurs.model;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kurs.vm
{
    class ViewModelTableKOM : AbstractViewModelTableBaseWithEdit
    {
        readonly int _idcp;
        public ViewModelTableKOM(string SelectedCommandText, int idcp, string tablename) : base(SelectedCommandText, tablename)
        {
            _idcp = idcp;
            InizialistModel();
        }

        KOMmodel kommodel;
        public KOMmodel KOMModel
        {
            get { return kommodel; }
            set { this.RaiseAndSetIfChanged(ref kommodel, value); }
        }
        public override void InizialistModel()
        {
            KOMModel = new KOMmodel(_idcp);
            ModelTable = KOMModel;
        }
    }
}
