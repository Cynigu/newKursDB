using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kurs.model
{
    public interface ImodelTable
    {
        void RowFromModelToTable(DataRow rowForEdit);
        void RowFromTableToModel(DataRow rowForEdit);

        bool CheckDataForEdit();
        bool CheckDataForAdd();
        void Clear();
    }
}
