using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kurs.models
{
    interface IDB
    {
        
        void AddCommandTable(string SelectText);
        void AddCommandSelectTable(string SelectText);
        Task FillTableAsync();
        Task UpdateBDAsync();
        void UpdateBD();
        void FillTable();
        MySqlConnection GetConnect();

    }
}
