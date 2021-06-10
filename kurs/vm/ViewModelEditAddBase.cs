using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Базовая вм для работы с окнами редактирования
// Пока только для того чтобы указать от чего наследуемся (ReactiveObject) 
// и определить абстрактные методы которые точно дождны быть в дочерних классах

namespace kurs.vm
{
    public abstract class ViewModelEditAddBase: ReactiveObject
    {
        #region methods
        public abstract void ChangeRowInTable(DataRow newRow);

        public abstract void CopyRowInWindow(DataRow temp);
        #endregion
    }
}
