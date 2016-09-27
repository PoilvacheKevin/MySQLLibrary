using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace MySQLLibrary
{
    class SQLControl
    {

        private string connectionString;
        private MySqlConnection connection;

        /// <summary>
        /// Connect the program with the database.
        /// </summary>
        public SQLControl(string IP, string dataBase, string user, string password)
        {
            connectionString = "SERVER =" + IP + "; DATABASE =" + dataBase + "; UID =" + user + "; PASSWORD =" + password + ";";
            connection = new MySqlConnection(connectionString);
        }

        /// <summary>
        /// Add Data to the database.
        /// </summary>
        public void AddData(string table, string[] columnsNames, string[] values)
        {
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO " + table + "( ";

            for (int i = 0; i < columnsNames.Length - 1; i++)
            {
                cmd.CommandText += columnsNames[i] + ", ";
            }
            cmd.CommandText += columnsNames[columnsNames.Length - 1] + ") VALUES ( \"";

            for (int i = 0; i < values.Length - 1; i++)
            {
                cmd.CommandText += values[i] + "\" , \"";
            }
            cmd.CommandText += values[values.Length - 1] + "\" );";
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        /// <summary>
        /// Delete Data in the database.
        /// </summary>
        public void DeleteData(string table, string oneColumnName, string valueColumnName)
        {
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "DELETE FROM " + table + " WHERE " + oneColumnName + " = \"" + valueColumnName + "\"";
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        /// <summary>
        /// update selected data in the database.
        /// </summary>
        public void UpdateData(string table, string[] columnsNames, string[] newData, string oldData)
        {
            connection.Open();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "UPDATE " + table + " SET ";

            for (int i = 0; i < columnsNames.Length - 1; i++)
            {
                cmd.CommandText += (columnsNames[i] + " = \"" + newData[i] + "\", ");
            }
            cmd.CommandText += (columnsNames[columnsNames.Length - 1] + " = \"" + newData[newData.Length - 1] + "\"");
            cmd.CommandText += " WHERE " + columnsNames[0] + " = \"" + oldData + "\"";

            cmd.ExecuteNonQuery();
            connection.Close();
        }


        /// <summary>
        /// Return all rows from a table
        /// </summary>
        public List<string>[] ReceiveData(string table)
        {
            connection.Open();
            string query = "SELECT * FROM " + table;
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader reader = cmd.ExecuteReader();

            int number = reader.FieldCount;
            List<string>[] rows = new List<string>[number];
            for (int i = 0; i < number; i++)
            {
                rows[i] = new List<string>();
            }

            while (reader.Read())
            {
                for (int i = 0; i < number; i++)
                {
                    rows[i].Add(reader[i] + "");
                }
            }

            connection.Close();
            return rows;
        }

        /// <summary>
        /// return every row which selected.
        /// </summary>
        public List<string>[] ReceiveData(string table, string[] dataRows)
        {
            connection.Open();
            string query = "SELECT * FROM " + table;
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader reader = cmd.ExecuteReader();

            List<string>[] rows = new List<string>[dataRows.Length];
            for (int i = 0; i < dataRows.Length; i++)
            {
                rows[i] = new List<string>();
            }

            while (reader.Read())
            {
                for (int i = 0; i < dataRows.Length; i++)
                {
                    rows[i].Add(reader[dataRows[i]] + "");
                }
            }

            connection.Close();
            return rows;
        }

        /// <summary>
        /// return specifics rows and specifics columns selected.
        /// </summary>
        public List<string>[] ReceiveRow(string table, string columnSelect, string searched, string specificSelect = "*")
        {
            connection.Open();
            string query = "SELECT " + specificSelect + " FROM " + table + " WHERE " + columnSelect + " = \"" + searched + "\"";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader reader = cmd.ExecuteReader();

            int number = reader.FieldCount;
            List<string>[] rows = new List<string>[number];
            for (int i = 0; i < number; i++)
            {
                rows[i] = new List<string>();
            }

            while (reader.Read())
            {
                for (int i = 0; i < number; i++)
                {
                    rows[i].Add(reader[i] + "");
                }
            }

            connection.Close();
            return rows;
        }
    }
}
