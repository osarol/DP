using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Npgsql;
using System.Windows.Forms;

namespace AutoShowroomApp
{
    public class DatabaseHelper
    {
        // Назва активної бази даних, яку використовує за замовчуванням конструктор без параметрів
        public static string ActiveDatabase { get; private set; } = null;

        private static readonly string AppSettingsDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AutoShowroomApp");
        private static readonly string AppSettingsFile = Path.Combine(AppSettingsDir, "appsettings.txt");

        static DatabaseHelper()
        {
            try
            {
                ActiveDatabase = LoadActiveDatabaseFromStorage();
            }
            catch
            {
                ActiveDatabase = null;
            }
        }

        // Експортує структуру (з DefaultSchemaSql) і опціонально дані з таблиць у .sql файл
        public bool ExportDatabaseToSql(string dbName, string filePath, bool includeData)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                // Додаємо вбудований скрипт структури
                sb.AppendLine("-- Schema export");
                sb.AppendLine(DefaultSchemaSql);
                sb.AppendLine();

                if (includeData)
                {
                    using var conn = new NpgsqlConnection(BuildConnectionString(dbName));
                    conn.Open();

                    // Отримати список таблиць у схемі public
                    using var tblCmd = new NpgsqlCommand("SELECT table_name FROM information_schema.tables WHERE table_schema = 'public' AND table_type='BASE TABLE'", conn);
                    using var reader = tblCmd.ExecuteReader();
                    var tables = new List<string>();
                    while (reader.Read())
                    {
                        tables.Add(reader.GetString(0));
                    }
                    reader.Close();

                    foreach (var table in tables)
                    {
                        sb.AppendLine($"-- Data for table {table}");

                        // Get columns
                        using var colCmd = new NpgsqlCommand($"SELECT column_name, data_type FROM information_schema.columns WHERE table_schema='public' AND table_name = @t ORDER BY ordinal_position", conn);
                        colCmd.Parameters.AddWithValue("t", table);
                        using var colReader = colCmd.ExecuteReader();
                        var columns = new List<(string name, string type)>();
                        while (colReader.Read())
                        {
                            columns.Add((colReader.GetString(0), colReader.GetString(1)));
                        }
                        colReader.Close();

                        // Read data
                        using var dataCmd = new NpgsqlCommand($"SELECT * FROM \"{table}\"", conn);
                        using var dataReader = dataCmd.ExecuteReader();
                        while (dataReader.Read())
                        {
                            var cols = new List<string>();
                            var vals = new List<string>();
                            for (int i = 0; i < dataReader.FieldCount; i++)
                            {
                                cols.Add("\"" + dataReader.GetName(i) + "\"");
                                var val = dataReader.GetValue(i);
                                vals.Add(FormatValueForSql(val, dataReader.GetFieldType(i)));
                            }

                            sb.AppendLine($"INSERT INTO \"{table}\" ({string.Join(", ", cols)}) VALUES ({string.Join(", ", vals)});\n");
                        }
                        dataReader.Close();
                    }
                }

                File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при експорті БД: " + ex.Message);
                return false;
            }
        }

        private string FormatValueForSql(object val, Type fieldType)
        {
            if (val == null || val == DBNull.Value) return "NULL";

            if (fieldType == typeof(string) || fieldType == typeof(char) || fieldType == typeof(Guid))
            {
                string s = val.ToString().Replace("'", "''");
                return $"'{s}'";
            }
            if (fieldType == typeof(bool))
            {
                return ((bool)val) ? "TRUE" : "FALSE";
            }
            if (fieldType == typeof(DateTime))
            {
                var dt = (DateTime)val;
                return $"'{dt.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}'";
            }
            // Numeric types
            if (fieldType.IsPrimitive || fieldType == typeof(decimal) || fieldType == typeof(double) || fieldType == typeof(float))
            {
                if (val is IFormattable f)
                    return f.ToString(null, CultureInfo.InvariantCulture);
                return val.ToString();
            }

            // Fallback
            string sf = val.ToString().Replace("'", "''");
            return $"'{sf}'";
        }


        public static void SetActiveDatabase(string dbName)
        {
            ActiveDatabase = dbName;
            try
            {
                SaveActiveDatabaseToStorage(dbName);
            }
            catch
            {
                // Не критично, просто не збереглось
            }
        }

        private static string LoadActiveDatabaseFromStorage()
        {
            try
            {
                if (!File.Exists(AppSettingsFile)) return null;
                var text = File.ReadAllText(AppSettingsFile).Trim();
                return string.IsNullOrWhiteSpace(text) ? null : text;
            }
            catch
            {
                return null;
            }
        }

        private static void SaveActiveDatabaseToStorage(string dbName)
        {
            Directory.CreateDirectory(AppSettingsDir);
            File.WriteAllText(AppSettingsFile, dbName ?? string.Empty);
        }

        // Вбудований SQL-скрипт для створення структури БД
        public const string DefaultSchemaSql = @"-- 1. Марки авто (для зручного вибору в UI)
CREATE TABLE Brands (
    brand_id SERIAL PRIMARY KEY,
    name VARCHAR(50) NOT NULL UNIQUE,
    country VARCHAR(50)
);

-- 2. Типи кузова (Седан, Позашляховик і т.д.)
CREATE TABLE BodyTypes (
    body_type_id SERIAL PRIMARY KEY,
    name VARCHAR(30) NOT NULL UNIQUE
);

-- 3. Автомобілі
CREATE TABLE Cars (
    car_id SERIAL PRIMARY KEY,
    brand_id INTEGER REFERENCES Brands(brand_id),
    body_type_id INTEGER REFERENCES BodyTypes(body_type_id),
    model VARCHAR(50) NOT NULL,
    vin_code VARCHAR(17) UNIQUE NOT NULL,
    year_produced INTEGER,
    price NUMERIC(12, 2) NOT NULL,
    color VARCHAR(30),
    engine_volume DECIMAL(3,1), -- Наприклад, 2.0
    status VARCHAR(20) DEFAULT 'В наявності'
);

-- 4. Постачальники (звідки привозять авто)
CREATE TABLE Suppliers (
    supplier_id SERIAL PRIMARY KEY,
    company_name VARCHAR(100) NOT NULL,
    contact_person VARCHAR(100),
    phone VARCHAR(20)
);

-- 5. Поставки (журнал надходження авто)
CREATE TABLE Supplies (
    supply_id SERIAL PRIMARY KEY,
    car_id INTEGER REFERENCES Cars(car_id),
    supplier_id INTEGER REFERENCES Suppliers(supplier_id),
    supply_date DATE DEFAULT CURRENT_DATE,
    purchase_cost NUMERIC(12, 2)
);

-- 6. Клієнти
CREATE TABLE Customers (
    customer_id SERIAL PRIMARY KEY,
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    phone VARCHAR(20) NOT NULL,
    email VARCHAR(100),
    address TEXT
);

-- 7. Посади (для розмежування прав у майбутньому)
CREATE TABLE Positions (
    position_id SERIAL PRIMARY KEY,
    title VARCHAR(50) NOT NULL,
    salary_base NUMERIC(10, 2)
);

-- 8. Співробітники
CREATE TABLE Employees (
    employee_id SERIAL PRIMARY KEY,
    position_id INTEGER REFERENCES Positions(position_id),
    full_name VARCHAR(100) NOT NULL,
    hire_date DATE DEFAULT CURRENT_DATE,
    login VARCHAR(50) UNIQUE,
    password_hash VARCHAR(255)
);

-- 9. Тест-драйви (важлива частина автоматизації)
CREATE TABLE TestDrives (
    test_drive_id SERIAL PRIMARY KEY,
    car_id INTEGER REFERENCES Cars(car_id),
    customer_id INTEGER REFERENCES Customers(customer_id),
    employee_id INTEGER REFERENCES Employees(employee_id),
    scheduled_at TIMESTAMP NOT NULL,
    notes TEXT
);

-- 10. Продажі (Фінальна таблиця)
CREATE TABLE Sales (
    sale_id SERIAL PRIMARY KEY,
    car_id INTEGER REFERENCES Cars(car_id) ON DELETE CASCADE,
    customer_id INTEGER REFERENCES Customers(customer_id),
    employee_id INTEGER REFERENCES Employees(employee_id),
    sale_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    final_price NUMERIC(12, 2) NOT NULL,
    payment_method VARCHAR(30)
);
";

        // Виконати SQL-скрипт, переданий як рядок
        public bool ExecuteScriptContent(string dbName, string scriptContent)
        {
            try
            {
                using var conn = new NpgsqlConnection(BuildConnectionString(dbName));
                conn.Open();

                // Розбиваємо по ';' і виконуємо кожну команду окремо
                var parts = scriptContent.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var part in parts)
                {
                    var stmt = part.Trim();
                    if (string.IsNullOrWhiteSpace(stmt)) continue;
                    using var cmd = new NpgsqlCommand(stmt, conn);
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при виконанні скрипта: " + ex.Message);
                return false;
            }
        }

        private string _host = "localhost";
        private string _username = "postgres";
        private string _password = "postgres";
        private string connectionString;

        public DatabaseHelper()
        {
            // Якщо користувач раніше відкрив/обрав БД — використовуємо її, інакше підключаємось до postgres
            string db = ActiveDatabase ?? "postgres";
            connectionString = BuildConnectionString(db);
        }

        // Повернути рядок підключення, що використовується поточним екземпляром
        public string GetConnectionString()
        {
            return connectionString;
        }

        public DatabaseHelper(string host, string username, string password, string database = "postgres")
        {
            _host = host;
            _username = username;
            _password = password;
            connectionString = BuildConnectionString(database);
        }

        private string BuildConnectionString(string database)
        {
            return $"Host={_host};Username={_username};Password={_password};Database={database}";
        }

        public bool TestConnection(string database = null)
        {
            try
            {
                string cs = database == null ? connectionString : BuildConnectionString(database);
                using var conn = new NpgsqlConnection(cs);
                conn.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DatabaseExists(string dbName)
        {
            try
            {
                using var conn = new NpgsqlConnection(BuildConnectionString("postgres"));
                conn.Open();
                using var cmd = new NpgsqlCommand("SELECT 1 FROM pg_database WHERE datname = @name", conn);
                cmd.Parameters.AddWithValue("name", dbName);
                var scalar = cmd.ExecuteScalar();
                return scalar != null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при перевірці БД: " + ex.Message);
                return false;
            }
        }

        public bool CreateDatabase(string dbName)
        {
            try
            {
                using var conn = new NpgsqlConnection(BuildConnectionString("postgres"));
                conn.Open();

                using (var check = new NpgsqlCommand("SELECT 1 FROM pg_database WHERE datname = @name", conn))
                {
                    check.Parameters.AddWithValue("name", dbName);
                    if (check.ExecuteScalar() != null)
                        return true;
                }

                using var cmd = new NpgsqlCommand($"CREATE DATABASE \"{dbName}\"", conn);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при створенні БД: " + ex.Message);
                return false;
            }
        }

        public bool SetDatabase(string dbName)
        {
            if (!DatabaseExists(dbName))
            {
                MessageBox.Show("База даних не знайдена: " + dbName);
                return false;
            }

            connectionString = BuildConnectionString(dbName);
            return TestConnection(dbName);
        }

        public bool ExecuteScript(string dbName, string scriptFilePath)
        {
            try
            {
                if (!File.Exists(scriptFilePath))
                {
                    MessageBox.Show("Файл скрипту не знайдено: " + scriptFilePath);
                    return false;
                }

                string script = File.ReadAllText(scriptFilePath);

                using var conn = new NpgsqlConnection(BuildConnectionString(dbName));
                conn.Open();
                using var cmd = new NpgsqlCommand(script, conn);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при виконанні скрипта: " + ex.Message);
                return false;
            }
        }

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