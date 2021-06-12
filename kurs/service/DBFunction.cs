using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kurs.service
{
    class DBFunction
    {
        private readonly MySqlConnection _connection;
        public DBFunction(string connect = "SERVER = localhost;Database=гардероб;UID=root; Password=root;  convert zero datetime=True; ")
        {
            _connection = new MySqlConnection(connect);

        }
        public int CallFuncCamputeSum(string text, int id, int count)
        {
            int x = 0;
            MySqlCommand command = new MySqlCommand(text, _connection);
            command.CommandType = CommandType.StoredProcedure;
            MySqlParameter p1 = new MySqlParameter("id", MySqlDbType.Int32);
            p1.Direction = ParameterDirection.Input;
            MySqlParameter p2 = new MySqlParameter("count", MySqlDbType.Int32);
            p2.Direction = ParameterDirection.Input;
            MySqlParameter p3 = new MySqlParameter("result", MySqlDbType.Int32);
            p3.Direction = ParameterDirection.ReturnValue;
            p1.Value = id;
            p2.Value = count;
            command.Parameters.Add(p1);
            command.Parameters.Add(p2);
            command.Parameters.Add(p3);
            _connection.Open();
            command.ExecuteNonQuery();
            _connection.Close();
            if (p3.Value != DBNull.Value)
                x = (int)p3.Value;
            return x;
        }
    }
}
