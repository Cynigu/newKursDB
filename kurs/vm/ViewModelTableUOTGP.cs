using Autofac;
using kurs.model;
using kurs.models;
using kurs.service;
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
            var b = Container.ContainerUOTGP();
            var con = b.Build();
            UOTgpModel = con.Resolve<UOTGPmodel>(new NamedParameter("p1", _idPZ), new NamedParameter("p2", _qPZ));

            ModelTable = UOTgpModel;
        }

        public override void GenerateTable()
        {
            var b = Container.ContainerUOTGenerate();
            var con = b.Build();

            
            DT.AcceptChanges();
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
            IDB _dBNP;
            string commandFillNP = "select * from нп";
            DataTable NP1 = new DataTable();
            _dBNP = con.Resolve<IDB>(new NamedParameter("p1", NP1));
            _dBNP.AddCommandSelectTable(commandFillNP);
            _dBNP.FillTable();
            
            // Таблица компонентов
            IDB _dBKOM;
            string commandFillKOM = "select * from компонент";
            DataTable KOM = new DataTable();
            _dBKOM = con.Resolve<IDB>(new NamedParameter("p1", KOM));
            _dBKOM.AddCommandSelectTable(commandFillKOM);
            _dBKOM.FillTable();
            // Таблица пз
            IDB _dBPZ;
            string commandFillPZ = "select * from пз where id_ПЗ =" + _idPZ;
            DataTable PZ = new DataTable();
            _dBPZ = con.Resolve<IDB>(new NamedParameter("p1", PZ));
            _dBPZ.AddCommandSelectTable(commandFillPZ);
            _dBPZ.FillTable();

            int idBaseNP = PZ.Rows[0].Field<int>("id_НП");
            int idBaseCp = NP1.Select("id_НП = " + idBaseNP)[0].Field<int>("Спецификация");
            List<List<int>> idCP = new List<List<int>>();
            List<int> checkCP = new List<int>();
            checkCP.Add(idBaseCp);
            idCP.Add(new List<int>());
            idCP[0].Add(idBaseCp);
            idCP[0].Add(_qPZ);

            DataRow dr1 = DT.NewRow();
            dr1["id_ПЗ"] = _idPZ;
            dr1["id_НП"] = idBaseNP;
            dr1["Кол-во (план)"] = _qPZ;
            DT.Rows.Add(dr1);

            while (idCP.Count != 0)
            {
                foreach (DataRow drkom in KOM.Select("Спецификация =" + idCP[0][0]))
                {
                    int idnp = drkom.Field<int>("НП");
                    if (!NP1.Select("id_НП =" + idnp)[0].IsNull("Спецификация"))
                    {
                        int idcp = NP1.Select("id_НП =" + idnp)[0].Field<int>("Спецификация");
                        if (checkCP.Contains(idcp))
                        {
                            MessageBox.Show("НП данного заказа имеет некорректную спецификацию, из-за чего нельзя сгенерировать таблицу автоматически");
                            return;
                        }
                        else checkCP.Add(idcp);

                        idCP.Add(new List<int>());
                        idCP[idCP.Count - 1].Add(idcp);
                        idCP[idCP.Count - 1].Add(idCP[0][1] * drkom.Field<int>("Кол-во"));

                        DataRow dr = DT.NewRow();
                        dr["id_ПЗ"] = _idPZ;
                        dr["id_НП"] = drkom.Field<int>("НП");
                        dr["Кол-во (план)"] = drkom.Field<int>("Кол-во") * idCP[0][1];
                        string sel = "id_НП =" + idnp + "and id_ПЗ =" + _idPZ;
                        if (DT.Select(sel).Length > 0)
                        {
                            DT.Select(sel)[0]["Кол-во (план)"] = DT.Select(sel)[0].Field<int>("Кол-во (план)")
                                + dr.Field<int>("Кол-во (план)");
                        }
                        else DT.Rows.Add(dr);
                    }
                }
                idCP.RemoveAt(0);
            }
        }
        #endregion
    }
}
