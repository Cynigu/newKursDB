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
    class ViewModelTableMK : AbstractViewModelTableBaseWithEditAndOpen
    {
        public ViewModelTableMK(string SelectedCommandText, string tablename = "Маршрутная карта") : base(SelectedCommandText, tablename)
        {
        }

        MKmodel mkmodel;
        public MKmodel MKModel
        {
            get { return mkmodel; }
            set { this.RaiseAndSetIfChanged(ref mkmodel, value); }
        }

        public override void InizialistModel()
        {
            MKModel = new MKmodel();
            ModelTable = MKModel;
        }

        public override void Open()
        {
            // ----- !!! Можно обработать при нажатии кнопки сохранить в операциях-------
            if (SelectedRow == null) return;
            // ----- !!! Можно обработать при нажатии кнопки сохранить в операциях-------
            int idmk = SelectedRow.Row.Field<int>("id_МК");
            string select = "select * from операция where id_Маршрута =" + idmk + ";";
            string t = "Маршрутная карта №" + idmk;
            ViewModelTableOP vm = new ViewModelTableOP(select, idmk, t);
            WindowOP OP = new WindowOP() { DataContext = vm };
            OP.Show();
            if (vm.EditData != null)
            {
                SelectedRow.Row["Дата_изменения"] = vm.EditData;
                Fill();
            }
        }
    }
}
