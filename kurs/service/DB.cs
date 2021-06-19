
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*Основная работа с БД
 Требуется только соединить сервер*/

namespace kurs.models
{
    public class DB: IDB
    {
        #region Readonly
        private readonly MySqlConnection _connection;
        private readonly MySqlDataAdapter _adapter;
        #endregion
        private DataTable _DT;

        #region Constructors
        //string connect = "SERVER = localhost;Database=kursdb;UID=root; Password=root;  convert zero datetime=True; "
        public DB(DataTable DT, string connect = "SERVER = localhost;Database=garderob;UID=root; Password=root;  convert zero datetime=True; ")
        {
            _connection = new MySqlConnection(connect);
            _adapter = new MySqlDataAdapter();
            _DT = DT;
        }

        #endregion


        #region Methods

        // Добавление таблицы с которой будем работать и инициализация основных запросов к ней
        public void AddCommandTable(string SelectText)
        {
            MySqlCommand myCommand = new MySqlCommand(SelectText, _connection);
            MySqlCommandBuilder commandB = new MySqlCommandBuilder(_adapter);
            _adapter.SelectCommand = myCommand;
            _adapter.DeleteCommand = commandB.GetDeleteCommand();
            _adapter.UpdateCommand = commandB.GetUpdateCommand();
            _adapter.InsertCommand = commandB.GetInsertCommand();
        }

        // Добавление таблицы и инициализация запроса select
        public void AddCommandSelectTable(string SelectText)
        {
            MySqlCommand myCommand = new MySqlCommand(SelectText, _connection);
            MySqlCommandBuilder commandB = new MySqlCommandBuilder(_adapter);
            _adapter.SelectCommand = myCommand;
        }

        
        // заполнить таблицу асинхронно
        public async Task FillTableAsync()
        {
            if (_DT == null) throw new ArgumentNullException();
            await  _adapter.FillAsync(_DT);
        }

        // обновить бд в сообвествии с изменениями в таблице асинхронно
        public async Task UpdateBDAsync()
        {
            if (_DT == null) throw new ArgumentNullException();
            await _adapter.UpdateAsync(_DT);
        }

        // обновить бд в сообвествии с изменениями в таблице
        public void UpdateBD()
        {
            if (_DT == null) throw new ArgumentNullException();
            _adapter.Update(_DT);
        }


        // заполнить таблицу
        public void FillTable()
        {
            if (_DT == null) throw new ArgumentNullException();
            _adapter.Fill(_DT);
        }

        public MySqlConnection GetConnect()
        {
            return _connection;
        }

        #endregion
    }
}
