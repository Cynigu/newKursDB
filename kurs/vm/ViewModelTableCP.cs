using kurs.model;
using kurs.view;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kurs.vm
{
    class ViewModelTableCP : AbstractViewModelTableBaseWithEditAndOpen
    {
        public ViewModelTableCP(string SelectedCommandText, string tablename = "Спецификация") : base(SelectedCommandText, tablename)
        {
        }
        CPmodel cpmodel;
        public CPmodel CPModel
        {
            get { return cpmodel; }
            set { this.RaiseAndSetIfChanged(ref cpmodel, value); }
        }
        public override void InizialistModel()
        {
            CPModel = new CPmodel();
            ModelTable = CPModel;
        }

        public override void Open()
        {
            if (SelectedRow == null) return;

            int idcp = SelectedRow.Row.Field<int>("id_Спецификация");
            string select = "select * from компонент where Спецификация =" + idcp + ";";
            string t = "Спецификация №" + idcp;
            ViewModelTableKOM vm = new ViewModelTableKOM(select, idcp, t);
            WindowKOM kom = new WindowKOM() { DataContext = vm };
            kom.Show();
            if (vm.EditData != null)
            {
                SelectedRow.Row["Дата_изменения"] = vm.EditData;
                Fill();
            }
        }
    }
}
