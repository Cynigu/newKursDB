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
    class UOTGPmodel : AbstractUOTmodel
    {
        public UOTGPmodel(int idpz, int qPZ,
            string _commandFillNP = "select * from нп where Метод_возобновления =" +
            " (select idМетод_возобновления from метод_возобновления where название = \"Производство\");") : base(idpz, _commandFillNP, qPZ)
        {
        }

    }

}
