using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kurs.vm
{
    class ViewModelTableSklad : AbstractViewModelTableBase
    {
        public ViewModelTableSklad(string SelectedCommandText, string tablename = "Склады") : base(SelectedCommandText, tablename)
        {
        }
    }
}
