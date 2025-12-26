using AutoShowroomApp;
using System.Data;
using static AutoShowroomApp.DatabaseHelper;
using Microsoft.VisualBasic;

namespace KP_2
{
    public partial class MainForm : Form
    {
        private string currentTableName = "";
        private string currentPrimaryKey = "";
        private Action currentRefreshAction = null;
        public MainForm()
        {
            InitializeComponent();
            даніАвтомобіліToolStripMenuItem_Click(null, null);
        }
        private void додатиБрендToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fields = new List<FieldConfig>
        {
        new FieldConfig { ColumnName = "name", DisplayName = "Назва бренду" },
        new FieldConfig { ColumnName = "country", DisplayName = "Країна походження" }
    };

            AddForm form = new AddForm("Додати новий бренд", "Brands", fields);
            if (form.ShowDialog() == DialogResult.OK)
            {
                currentRefreshAction?.Invoke();
            }
        }

        private void додатиПостачальникаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fields = new List<FieldConfig>
    {
        new FieldConfig { ColumnName = "company_name", DisplayName = "Назва компанії" },
        new FieldConfig { ColumnName = "contact_person", DisplayName = "Контактна особа" },
        new FieldConfig { ColumnName = "phone", DisplayName = "Номер телефону" }
    };

            AddForm form = new AddForm("Реєстрація постачальника", "Suppliers", fields);
            if (form.ShowDialog() == DialogResult.OK)
            {
                currentRefreshAction?.Invoke();
            }
        }

        private void клієнтаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fields = new List<FieldConfig>
    {
        new FieldConfig { ColumnName = "first_name", DisplayName = "Ім'я" },
        new FieldConfig { ColumnName = "last_name", DisplayName = "Прізвище" },
        new FieldConfig { ColumnName = "phone", DisplayName = "Телефон" },
        new FieldConfig { ColumnName = "email", DisplayName = "E-mail" },
        new FieldConfig { ColumnName = "address", DisplayName = "Адреса проживання" }
    };

            AddForm form = new AddForm("Картка нового клієнта", "Customers", fields);
            if (form.ShowDialog() == DialogResult.OK)
            {
                currentRefreshAction?.Invoke();
            }
        }
        private void додатиАвтомобільToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fields = new List<FieldConfig>
    {
        new FieldConfig {
            ColumnName = "brand_id",
            DisplayName = "Оберіть марку",
            DataSourceSql = "SELECT brand_id as id, name as display_name FROM Brands ORDER BY name"
        },
        new FieldConfig {
            ColumnName = "body_type_id",
            DisplayName = "Тип кузова",
            DataSourceSql = "SELECT body_type_id as id, name as display_name FROM BodyTypes"
        },
        new FieldConfig { ColumnName = "model", DisplayName = "Модель автомобіля" },
        new FieldConfig { ColumnName = "vin_code", DisplayName = "VIN-код (17 символів)" },
        new FieldConfig { ColumnName = "year_produced", DisplayName = "Рік випуску" },
        new FieldConfig { ColumnName = "price", DisplayName = "Ціна ($)" },
        new FieldConfig { ColumnName = "engine_volume", DisplayName = "Об'єм двигуна (л)" },
        new FieldConfig { ColumnName = "color", DisplayName = "Колір" }
    };

            AddForm form = new AddForm("Прийом автомобіля на склад", "Cars", fields);
            if (form.ShowDialog() == DialogResult.OK)
            {
                currentRefreshAction?.Invoke();
            }
        }

        private void поставкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fields = new List<FieldConfig>
    {
        new FieldConfig {
            ColumnName = "car_id",
            DisplayName = "Автомобіль",
            DataSourceSql = "SELECT car_id as id, model as display_name FROM Cars WHERE status = 'Очікується' OR status = 'В наявності'"
        },
        new FieldConfig {
            ColumnName = "supplier_id",
            DisplayName = "Постачальник",
            DataSourceSql = "SELECT supplier_id as id, company_name as display_name FROM Suppliers"
        },
        new FieldConfig { ColumnName = "supply_date", DisplayName = "Дата поставки" }, 
        new FieldConfig { ColumnName = "purchase_cost", DisplayName = "Вартість закупівлі ($)" }
    };

            AddForm form = new AddForm("Реєстрація нової поставки", "Supplies", fields);
            if (form.ShowDialog() == DialogResult.OK) currentRefreshAction?.Invoke();
        }

        private void продажToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fields = new List<FieldConfig>
    {
        new FieldConfig {
            ColumnName = "car_id",
            DisplayName = "Оберіть автомобіль",
            DataSourceSql = "SELECT car_id as id, model || ' (VIN: ' || vin_code || ')' as display_name " +
                            "FROM Cars WHERE status = 'В наявності' ORDER BY model"
        },
        
        new FieldConfig {
            ColumnName = "customer_id",
            DisplayName = "Клієнт (Покупець)",
            DataSourceSql = "SELECT customer_id as id, last_name || ' ' || first_name as display_name " +
                            "FROM Customers ORDER BY last_name"
        },
        
        new FieldConfig {
            ColumnName = "employee_id",
            DisplayName = "Менеджер з продажу",
            DataSourceSql = "SELECT employee_id as id, full_name as display_name FROM Employees"
        },
        
        new FieldConfig {
            ColumnName = "sale_date",
            DisplayName = "Дата угоди (РРРР-ММ-ДД)"
        },
        
        new FieldConfig {
            ColumnName = "final_price",
            DisplayName = "Фінальна вартість ($)"
        },
        
        new FieldConfig {
            ColumnName = "payment_method",
            DisplayName = "Спосіб оплати (Готівка/Карта/Кредит)"
        }
    };


            AddForm form = new AddForm("Оформлення нової угоди купівлі-продажу", "Sales", fields);


            if (form.ShowDialog() == DialogResult.OK)
            {

                currentRefreshAction?.Invoke();

                MessageBox.Show(
                    "Продаж успішно зареєстровано!\nСтатус автомобіля автоматично змінено на 'Продано'.",
                    "Успішна операція",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }

        private void посадуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fields = new List<FieldConfig>
    {
        new FieldConfig { ColumnName = "title", DisplayName = "Назва посади" },
        new FieldConfig { ColumnName = "salary_base", DisplayName = "Базова ставка ($)" }
    };

            AddForm form = new AddForm("Додати нову посаду", "Positions", fields);
            if (form.ShowDialog() == DialogResult.OK) currentRefreshAction?.Invoke();
        }

        private void працівникаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fields = new List<FieldConfig>
    {
        new FieldConfig { ColumnName = "full_name", DisplayName = "ПІБ працівника" },
        new FieldConfig {
            ColumnName = "position_id",
            DisplayName = "Посада",
            DataSourceSql = "SELECT position_id as id, title as display_name FROM Positions"
        },
        new FieldConfig { ColumnName = "hire_date", DisplayName = "Дата прийому (РРРР-ММ-ДД)" },
        new FieldConfig { ColumnName = "login", DisplayName = "Логін для входу" },
        new FieldConfig { ColumnName = "password_hash", DisplayName = "Пароль (тимчасово)" }
    };

            AddForm form = new AddForm("Реєстрація нового співробітника", "Employees", fields);
            if (form.ShowDialog() == DialogResult.OK) currentRefreshAction?.Invoke();
        }

        private void тестдрайвToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fields = new List<FieldConfig>
    {
        new FieldConfig {
            ColumnName = "customer_id",
            DisplayName = "Клієнт",
            DataSourceSql = "SELECT customer_id as id, last_name || ' ' || first_name as display_name FROM Customers"
        },
        new FieldConfig {
            ColumnName = "car_id",
            DisplayName = "Автомобіль",
            DataSourceSql = "SELECT car_id as id, model || ' (VIN: ' || vin_code || ')' as display_name FROM Cars WHERE status = 'В наявності'"
        },
        new FieldConfig {
            ColumnName = "employee_id",
            DisplayName = "Відповідальний менеджер",
            DataSourceSql = "SELECT employee_id as id, full_name as display_name FROM Employees"
        },
        new FieldConfig { ColumnName = "scheduled_at", DisplayName = "Дата та час (YYYY-MM-DD HH:MM)" },
        new FieldConfig { ColumnName = "notes", DisplayName = "Примітки" }
    };

            AddForm form = new AddForm("Запис на тест-драйв", "TestDrives", fields);
            if (form.ShowDialog() == DialogResult.OK) currentRefreshAction?.Invoke();
        }

        private void типКузоваToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fields = new List<FieldConfig>
    {
        new FieldConfig { ColumnName = "name", DisplayName = "Назва типу кузова (напр. Седан)" }
    };

            AddForm form = new AddForm("Додати тип кузова", "BodyTypes", fields);
            if (form.ShowDialog() == DialogResult.OK) currentRefreshAction?.Invoke();
        }

        private void RefreshData(string sql)
        {
            try
            {
                DatabaseHelper db = new DatabaseHelper();
                DataTable dt = db.ExecuteQuery(sql);
                dgvMain.DataSource = dt;

                dgvMain.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvMain.AllowUserToAddRows = false; 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка завантаження даних: " + ex.Message);
            }
        }

        private void даніТестдрайвиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTableName = "TestDrives";
            currentPrimaryKey = "test_drive_id";
            currentRefreshAction = () => даніТестдрайвиToolStripMenuItem_Click(null, null);

            string sql = @"SELECT test_drive_id as ""ID"", customer_id as ""ID Клієнта"", 
                           car_id as ""ID Авто"", employee_id as ""ID Працівника"",
                           scheduled_at as ""Дата та час"", 
                           notes as ""Примітки"" 
                    FROM TestDrives ORDER BY scheduled_at DESC";
            RefreshData(sql);
        }

        private void ДаніПостачальникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTableName = "Suppliers";
            currentPrimaryKey = "supplier_id";
            currentRefreshAction = () => ДаніПостачальникиToolStripMenuItem_Click(null, null);

            string sql = @"SELECT supplier_id as ""ID"", company_name as ""Компанія"", 
                           contact_person as ""Контакт"", phone as ""Телефон"" 
                    FROM Suppliers";
            RefreshData(sql);
        }

        private void даніПосадиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTableName = "Positions";
            currentPrimaryKey = "position_id";
            currentRefreshAction = () => даніПосадиToolStripMenuItem_Click(null, null);

            string sql = @"SELECT position_id as ""ID"", title as ""Посада"", 
                           salary_base as ""Оклад"" 
                    FROM Positions";
            RefreshData(sql);
        }

        private void даніМаркиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTableName = "Brands";
            currentPrimaryKey = "brand_id";
            currentRefreshAction = () => даніМаркиToolStripMenuItem_Click(null, null);

            string sql = @"SELECT brand_id as ""ID"", name as ""Бренд"", 
                           country as ""Країна"" 
                    FROM Brands ORDER BY name";
            RefreshData(sql);
        }

        private void даніСпівробітникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTableName = "Employees";
            currentPrimaryKey = "employee_id";
            currentRefreshAction = () => даніСпівробітникиToolStripMenuItem_Click(null, null);

            string sql = @"SELECT employee_id as ""ID"", full_name as ""ПІБ"", 
                           position_id as ""ID Посади"", hire_date as ""Дата найму"" 
                    FROM Employees";
            RefreshData(sql);
        }

        private void даніКлієнтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTableName = "Customers";
            currentPrimaryKey = "customer_id";
            currentRefreshAction = () => даніКлієнтиToolStripMenuItem_Click(null, null);

            string sql = @"SELECT customer_id as ""ID"", first_name as ""Ім'я"", 
                           last_name as ""Прізвище"", phone as ""Телефон"" 
                    FROM Customers";
            RefreshData(sql);
        }

        private void даніАвтомобіліToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTableName = "Cars";
            currentPrimaryKey = "car_id";
            currentRefreshAction = () => даніАвтомобіліToolStripMenuItem_Click(null, null);

            string sql = @"SELECT car_id as ""ID"", brand_id as ""ID Бренду"", 
                           model as ""Модель"", price as ""Ціна"", 
                           status as ""Статус"" 
                    FROM Cars";
            RefreshData(sql);
        }

        private void даніОстанніПродажіToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTableName = "Sales";
            currentPrimaryKey = "sale_id";
            currentRefreshAction = () => даніОстанніПродажіToolStripMenuItem_Click(null, null);

            string sql = @"SELECT sale_id as ""ID"", sale_date as ""Дата"", 
                           car_id as ""ID Авто"", customer_id as ""ID Клієнта"", 
                           final_price as ""Сума"" 
                    FROM Sales ORDER BY sale_date DESC";
            RefreshData(sql);
        }

        private void даніПоставкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTableName = "Supplies";
            currentPrimaryKey = "supply_id";
            currentRefreshAction = () => даніПоставкиToolStripMenuItem_Click(null, null);

            string sql = @"SELECT supply_id as ""ID"", car_id as ""ID Авто"", 
                           supplier_id as ""ID Постачальника"", supply_date as ""Дата"" 
                    FROM Supplies";
            RefreshData(sql);
        }
        private void даніТипиКузоваToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTableName = "BodyTypes";
            currentPrimaryKey = "body_type_id";
            currentRefreshAction = () => даніТипиКузоваToolStripMenuItem_Click(null, null);

            string sql = @"SELECT body_type_id as ""ID"", 
                           name as ""Назва типу кузова"" 
                    FROM BodyTypes 
                    ORDER BY name";

            RefreshData(sql);
        }

        private void видалитиЗаписToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvMain.CurrentRow == null)
            {
                MessageBox.Show("Виберіть рядок у таблиці!");
                return;
            }

            string idValue = dgvMain.CurrentRow.Cells[0].Value.ToString();

            if (string.IsNullOrEmpty(currentTableName))
            {
                MessageBox.Show("Спочатку виберіть розділ (наприклад, Автомобілі) у меню.");
                return;
            }

            if (MessageBox.Show($"Видалити запис ID {idValue} з таблиці {currentTableName}?", "Увага",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    DatabaseHelper db = new DatabaseHelper();
                    string sql = $"DELETE FROM {currentTableName} WHERE {currentPrimaryKey} = {idValue}";
                    db.ExecuteNonQuery(sql);

                    MessageBox.Show("Видалено успішно!");
                    currentRefreshAction?.Invoke();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка: " + ex.Message);
                }
            }
        }

        private void пошукToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvMain.DataSource == null || dgvMain.Rows.Count == 0)
            {
                MessageBox.Show("Спочатку відкрийте будь-яку таблицю (Автомобілі, Клієнти тощо).");
                return;
            }

            string searchTerm = Interaction.InputBox("Введіть текст для пошуку:", "Пошук", "");

            if (string.IsNullOrWhiteSpace(searchTerm)) return;

            bool found = false;
            dgvMain.ClearSelection(); 

            foreach (DataGridViewRow row in dgvMain.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null && cell.Value.ToString().Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    {
                        row.Selected = true; 
                        dgvMain.FirstDisplayedScrollingRowIndex = row.Index; 
                        found = true;
                        break;
                    }
                }
            }

            if (!found)
            {
                MessageBox.Show($"За запитом '{searchTerm}' нічого не знайдено.");
            }
        }

        private void звітАвтомобіліToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTableName = ""; 
            currentRefreshAction = () => звітАвтомобіліToolStripMenuItem_Click(null, null);

            string sql = @"SELECT b.name as ""Марка"", c.model as ""Модель"", 
                           bt.name as ""Кузов"", c.year_produced as ""Рік"", 
                           c.price as ""Ціна ($)"", c.color as ""Колір""
                    FROM Cars c
                    JOIN Brands b ON c.brand_id = b.brand_id
                    JOIN BodyTypes bt ON c.body_type_id = bt.body_type_id
                    WHERE c.status = 'В наявності'
                    ORDER BY b.name, c.model";
            RefreshData(sql);
        }

        private void журналПродажівToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTableName = "";
            currentRefreshAction = () => журналПродажівToolStripMenuItem_Click(null, null);

            string sql = @"SELECT s.sale_date as ""Дата"", 
                           cust.last_name || ' ' || cust.first_name as ""Клієнт"",
                           b.name || ' ' || c.model as ""Автомобіль"",
                           s.final_price as ""Сума угоди"",
                           e.full_name as ""Менеджер""
                    FROM Sales s
                    JOIN Customers cust ON s.customer_id = cust.customer_id
                    JOIN Cars c ON s.car_id = c.car_id
                    JOIN Brands b ON c.brand_id = b.brand_id
                    JOIN Employees e ON s.employee_id = e.employee_id
                    ORDER BY s.sale_date DESC";
            RefreshData(sql);
        }

        private void аналітикаБрендівToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTableName = "";
            currentRefreshAction = () => аналітикаБрендівToolStripMenuItem_Click(null, null);

            string sql = @"SELECT b.name as ""Бренд"", 
                           COUNT(c.car_id) as ""Кількість авто"", 
                           SUM(c.price) as ""Загальна вартість ($)""
                    FROM Brands b
                    LEFT JOIN Cars c ON b.brand_id = c.brand_id
                    GROUP BY b.name
                    ORDER BY COUNT(c.car_id) DESC";
            RefreshData(sql);
        }
    }
}