using kurs.model;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kurs.vm
{
    class ViewModelTableZapas : AbstractViewModelTableBaseWithEdit
    {
        public ViewModelTableZapas(string SelectedCommandText = "select * from запас", string tablename = "Запас") : base(SelectedCommandText, tablename)
        {
        }
        Zmodel zmodel;
        public Zmodel ZModel
        {
            get { return zmodel; }
            set { this.RaiseAndSetIfChanged(ref zmodel, value); }
        }
        public override void InizialistModel()
        {
            ZModel = new Zmodel();
            ModelTable = ZModel;
        }
    }
}
