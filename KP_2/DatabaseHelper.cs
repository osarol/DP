using System;
using System.Data;
using Npgsql;
using System.Windows.Forms;

namespace AutoShowroomApp
{
    public class DatabaseHelper
    {

        private string connectionString = "Host=localhost;Username=postgres;Password=postgres;Database=postgres";

        /// <summary>
        /// Метод для отримання даних з бази у вигляді таблиці (DataTable)
        /// </summary>
        public DataTable ExecuteQuery(string sql)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            dataTable.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при виконанні запиту: " + ex.Message);
            }
            return dataTable;
        }

        /// <summary>
        /// Метод для виконання команд без повернення таблиці (INSERT, UPDATE, DELETE)
        /// </summary>
        public void ExecuteNonQuery(string sql)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при зміні даних: " + ex.Message);
            }
        }
        public class FieldConfig
        {
            public string ColumnName { get; set; } 
            public string DisplayName { get; set; } 
            public string DataSourceSql { get; set; } 
        }
    }
}