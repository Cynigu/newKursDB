using kurs.models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace kurs.model
{
    public abstract class AbstractUOTmodel : ReactiveObject, ImodelTable
    {
        private readonly int _idPZ;
        private readonly int _qPZ;
        public AbstractUOTmodel(int idpz, string _commandFillNP, int qPZ)
        {
            _idPZ = idpz;
            _qPZ = qPZ;

            DB _dBNP;
            string commandFillNP = _commandFillNP;
            NP = new DataTable();
            //string commandFillNP = "select id_НП from нп; ";
            _dBNP = new DB(NP);
            _dBNP.AddCommandSelectTable(commandFillNP);
            _dBNP.FillTable();
        }

        #region Fields
        private DataTable np;
        private DataRowView selectedNP;
        private int qfact;
        private int qplan;
        #endregion

        #region Properties
        public DataTable NP
        {
            get { return np; }
            set { this.RaiseAndSetIfChanged(ref np, value); }
        }
        public DataRowView SelectedNP
        {
            get
            {
                return selectedNP;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref selectedNP, value);
            }
        }

        public int QFact
        {
            get
            {
                return qfact;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref qfact, value);
            }
        }

        public int QPlan
        {
            get
            {
                return qplan;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref qplan, value);
            }
        }
        #endregion

        public virtual bool CheckDataForAdd()
        {
            if (SelectedNP == null)
            {
                MessageBox.Show("Введите НП");
                return false;
            }

            return true;
        }

        public virtual bool CheckDataForEdit()
        {
            if (SelectedNP == null)
            {
                MessageBox.Show("Введите НП");
                return false;
            }

            return true;
        }

        public virtual void RowFromModelToTable(DataRow rowForEdit)
        {
            rowForEdit["id_ПЗ"] = _idPZ;
            if (SelectedNP != null)
            {
                rowForEdit["id_НП"] = SelectedNP.Row.Field<int>("id_НП");
                QPlan = ComputeQuantityPlan();
                rowForEdit["Кол-во (план)"] = QPlan;
            }

            rowForEdit["Кол-во (факт)"] = QFact;
            
        }

        public virtual void RowFromTableToModel(DataRow rowForEdit)
        {
            if (!rowForEdit.IsNull("id_НП"))
            {
                int m = int.Parse(rowForEdit["id_НП"].ToString());
                SelectedNP = NP.DefaultView[NP.Rows.IndexOf(NP.Select("id_НП =" + m)[0])];
            }
            if (!rowForEdit.IsNull("Кол-во (факт)"))
            {
                QFact = int.Parse(rowForEdit["Кол-во (факт)"].ToString());
            }
        }

        private int ComputeQuantityPlan()
        {
            int plan = 0;
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
                foreach (DataRow dr in KOM.Select("Спецификация =" + idCP[0][0]))
                {
                    if(dr.Field<int>("НП") == SelectedNP.Row.Field<int>("id_НП"))
                    {
                        plan += dr.Field<int>("Кол-во") * idCP[0][1];
                    }
                    int idnp = dr.Field<int>("НП");
                    if (!NP1.Select("id_НП =" + idnp)[0].IsNull("Спецификация"))
                    {
                        idCP.Add(new List<int>());
                        idCP[idCP.Count - 1].Add(NP1.Select("id_НП =" + idnp)[0].Field<int>("Спецификация"));
                        idCP[idCP.Count - 1].Add(idCP[0][1] * dr.Field<int>("Кол-во"));
                    }
                }
                idCP.RemoveAt(0);
            }

            return plan;
        }

        public void Clear()
        {
            QPlan = 0;
            SelectedNP = null;
            QFact = 0;
        }
    }
}
