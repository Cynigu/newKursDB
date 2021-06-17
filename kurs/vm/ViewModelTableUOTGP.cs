using kurs.model;
using kurs.models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace kurs.vm
{
    class ViewModelTableUOTGP : AbstractViewModelTableUOT
    {

        public ViewModelTableUOTGP(string SelectedCommandText, int idPZ, int qPZ, string t = "select * from товарные_операции_выходгп") : base(SelectedCommandText, idPZ, qPZ, t)
        {
            InizialistModel();
        }
        #region fields
        UOTGPmodel uotgpmodel;
        #endregion
        #region Properties
        public UOTGPmodel UOTgpModel
        {
            get { return uotgpmodel; }
            set { this.RaiseAndSetIfChanged(ref uotgpmodel, value); }
        }
        #endregion


        #region Methods

        public override void InizialistModel()
        {
            UOTgpModel = new UOTGPmodel(_idPZ, _qPZ);
            ModelTable = UOTgpModel;
        }

        public override void GenerateTable()
        {

            foreach (DataRow dr in DT.Rows)
            {
                if (dr.RowState == DataRowState.Deleted)
                {
                    return;
                }
                if (dr.Field<int>("id_ПЗ") == _idPZ)
                {
                    dr.Delete();
                }
            }

            // Таблица нп
            DB _dBNP;
            string commandFillNP = "select * from нп";
            DataTable NP1 = new DataTable();
            _dBNP = new DB(NP1);
            _dBNP.AddCommandSelectTable(commandFillNP);
            _dBNP.FillTable();

            // Таблица компонентов
            DB _dBKOM;
            string commandFillKOM = "select * from компонент";
            DataTable KOM = new DataTable();
            _dBKOM = new DB(KOM);
            _dBKOM.AddCommandSelectTable(commandFillKOM);
            _dBKOM.FillTable();
            // Таблица пз
            DB _dBPZ;
            string commandFillPZ = "select * from пз where id_ПЗ =" + _idPZ;
            DataTable PZ = new DataTable();
            _dBPZ = new DB(PZ);
            _dBPZ.AddCommandSelectTable(commandFillPZ);
            _dBPZ.FillTable();

            int idBaseNP = PZ.Rows[0].Field<int>("id_НП");

            List<List<int>> idCP = new List<List<int>>();
            idCP.Add(new List<int>());
            idCP[0].Add(NP1.Select("id_НП = " + idBaseNP)[0].Field<int>("Спецификация"));
            idCP[0].Add(_qPZ);
            while (idCP.Count != 0)
            {
                foreach (DataRow drkom in KOM.Select("Спецификация =" + idCP[0][0]))
                {
                    int idnp = drkom.Field<int>("НП");
                    if (!NP1.Select("id_НП =" + idnp)[0].IsNull("Спецификация"))
                    {
                        idCP.Add(new List<int>());
                        idCP[idCP.Count - 1].Add(NP1.Select("id_НП =" + idnp)[0].Field<int>("Спецификация"));
                        idCP[idCP.Count - 1].Add(idCP[0][1] * drkom.Field<int>("Кол-во"));

                        DataRow dr = DT.NewRow();
                        dr["id_ПЗ"] = _idPZ;
                        dr["id_НП"] = drkom.Field<int>("НП");
                        dr["Кол-во (план)"] = drkom.Field<int>("Кол-во") * idCP[0][1];
                        DT.Rows.Add(dr);
                    }
                }
                idCP.RemoveAt(0);
            }
        }
        #endregion
    }
}
