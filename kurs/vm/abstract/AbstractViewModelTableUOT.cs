using kurs.commands;
using kurs.models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace kurs.vm
{
    public abstract class AbstractViewModelTableUOT : AbstractViewModelTableBaseWithEdit
    {
        public readonly int _idPZ;
        public readonly int _qPZ;
        public AbstractViewModelTableUOT(string SelectedCommandText, string t) : base(SelectedCommandText, t)
        {
        }

        public AbstractViewModelTableUOT(string SelectedCommandText, int idPZ, int qPZ, string t) : base(SelectedCommandText, t)
        {
            DT.DefaultView.RowFilter = "id_ПЗ =" + idPZ;
            _idPZ = idPZ;
            _qPZ = qPZ;
        }

        #region Commands
        // GenerateTableCommand

        private RelayCommand generateTableCommand;
        public ICommand GenerateTableCommand
        {
            get
            {
                return generateTableCommand ??
                  (generateTableCommand = new RelayCommand(obj =>
                  {
                      GenerateTable();
                  }));
            }
        }

        #endregion

        #region Methods
        public virtual void GenerateTable()
        {
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
            int idBaseCp = NP1.Select("id_НП = " + idBaseNP)[0].Field<int>("Спецификация");
            List<List<int>> idCP = new List<List<int>>();
            List<int> checkCP = new List<int>();
            checkCP.Add(idBaseCp);
            idCP.Add(new List<int>());
            idCP[0].Add(idBaseCp);
            idCP[0].Add(_qPZ);
            while (idCP.Count != 0)
            {
                foreach (DataRow drkom in KOM.Select("Спецификация =" + idCP[0][0]))
                {
                    DataRow dr = DT.NewRow();

                    dr["id_ПЗ"] = _idPZ;
                    dr["id_НП"] = drkom.Field<int>("НП");
                    dr["Кол-во (план)"] = drkom.Field<int>("Кол-во") * idCP[0][1];

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
                    }
                    DT.Rows.Add(dr);
                }
                idCP.RemoveAt(0);
            }
        }
        #endregion
    }
}
