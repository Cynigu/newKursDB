using kurs.models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace kurs.model
{
    class UOTPOTRmodel: AbstractUOTmodel
    {
        public UOTPOTRmodel(int idpz, int qPZ,
            string _commandFillNP = "select * from нп;") : base(idpz, _commandFillNP, qPZ)
        {
        }
        public override bool CheckDataForAdd()
        {
            DB _dBzapas;
            string commandFillNP = "select * from запас";
            DataTable zapas = new DataTable();
            _dBzapas = new DB(zapas);
            _dBzapas.AddCommandSelectTable(commandFillNP);
            _dBzapas.FillTable();
            int maxQ = 0;
            if (!zapas.Select("НП=" + SelectedNP.Row.Field<int>("id_НП").ToString())[0].IsNull("Кол-во")) 
                maxQ = zapas.Select("НП=" + SelectedNP.Row.Field<int>("id_НП").ToString())[0].Field<int>("Кол-во");
            if (maxQ < QFact)
            {
                MessageBox.Show("На складе нет столько ресурсов сколько вы указали!!");
                return false;
            }
            return base.CheckDataForAdd();
        }

        public override bool CheckDataForEdit()
        {
            DB _dBzapas;
            string commandFillNP = "select * from запас";
            DataTable zapas = new DataTable();
            _dBzapas = new DB(zapas);
            _dBzapas.AddCommandSelectTable(commandFillNP);
            _dBzapas.FillTable();

            int maxQ = zapas.Select("НП=" + SelectedNP.Row.Field<int>("id_НП").ToString())[0].Field<int>("Кол-во");
            if (maxQ < QFact)
            {
                MessageBox.Show("На складе нет столько ресурсов сколько вы указали!!");
                return false;
            }
            return base.CheckDataForEdit();
        }
    }
}
