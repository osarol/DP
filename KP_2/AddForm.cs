using AutoShowroomApp;
using System;
using System.Globalization;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using static AutoShowroomApp.DatabaseHelper;

namespace KP_2
{
    public partial class AddForm : Form
    {
        private string _tableName;
        private List<FieldConfig> _fields;
        private Dictionary<string, Control> _controls = new Dictionary<string, Control>();
        private Dictionary<string, string>? _initialValues;
        private bool _isEdit = false;
        private string _primaryKey = null;
        private string _primaryKeyValue = null;

        public AddForm(string title, string tableName, List<FieldConfig> fields, Dictionary<string, string>? initialValues = null, string primaryKey = null, string primaryKeyValue = null)
        {
            InitializeComponent();
            this.Text = title;
            this._tableName = tableName;
            this._fields = fields;
            this._initialValues = initialValues;
            this._primaryKey = primaryKey;
            this._primaryKeyValue = primaryKeyValue;
            this._isEdit = initialValues != null && !string.IsNullOrEmpty(primaryKey) && !string.IsNullOrEmpty(primaryKeyValue);
            GenerateUI();
        }

        private void GenerateUI()
        {
            DatabaseHelper db = new DatabaseHelper();

            // Fetch column types for the target table to detect date/timestamp columns
            var columnTypes = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            try
            {
                var dtCols = db.ExecuteQuery($"SELECT column_name, data_type FROM information_schema.columns WHERE table_schema='public' AND table_name = '{_tableName}'");
                foreach (DataRow r in dtCols.Rows)
                {
                    columnTypes[r["column_name"].ToString()] = r["data_type"].ToString();
                }
            }
            catch { }

            foreach (var field in _fields)
            {
                Label lbl = new Label { Text = field.DisplayName, AutoSize = true };
                flowLayoutPanel.Controls.Add(lbl);
                // If field looks like foreign key (ends with _id) and no DataSourceSql provided, try to resolve a data source
                DataTable cmbData = null;
                string builtSql = field.DataSourceSql;
                if (string.IsNullOrEmpty(builtSql) && field.ColumnName.EndsWith("_id", StringComparison.OrdinalIgnoreCase))
                {
                    var baseName = field.ColumnName.Substring(0, field.ColumnName.Length - 3); // remove _id

                    string ToPascal(string s)
                    {
                        var parts = s.Split(new[] { '_', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < parts.Length; i++)
                            parts[i] = parts[i].Length > 0 ? char.ToUpper(parts[i][0]) + parts[i].Substring(1) : parts[i];
                        return string.Join("", parts);
                    }

                    var pascal = ToPascal(baseName);

                    // Explicit known mapping for this schema
                    var fkMap = new Dictionary<string, (string table, string idCol, string displayExpr)>(StringComparer.OrdinalIgnoreCase)
                    {
                        ["brand"] = ("Brands", "brand_id", "name"),
                        ["body_type"] = ("BodyTypes", "body_type_id", "name"),
                        ["supplier"] = ("Suppliers", "supplier_id", "company_name"),
                        ["supply"] = ("Suppliers", "supplier_id", "company_name"),
                        ["customer"] = ("Customers", "customer_id", "last_name || ' ' || first_name"),
                        ["position"] = ("Positions", "position_id", "title"),
                        ["employee"] = ("Employees", "employee_id", "full_name"),
                        ["car"] = ("Cars", "car_id", "model || ' (VIN: ' || vin_code || ')'"),
                    };

                    if (fkMap.TryGetValue(baseName, out var map))
                    {
                        var testSql = $"SELECT {map.idCol} as id, {map.displayExpr} as display_name FROM {map.table} ORDER BY display_name";
                        try { cmbData = db.ExecuteQuery(testSql); builtSql = testSql; }
                        catch { cmbData = null; builtSql = null; }
                    }
                    else
                    {
                        var candidates = new[] { pascal + "s", pascal, baseName + "s", baseName };
                        foreach (var cand in candidates)
                        {
                            try
                            {
                                var testSql = $"SELECT {baseName}_id as id, COALESCE(name, full_name, company_name, model, title) as display_name FROM {cand} ORDER BY display_name";
                                var testDt = db.ExecuteQuery(testSql);
                                if (testDt != null && testDt.Rows.Count > 0)
                                {
                                    cmbData = testDt;
                                    builtSql = testSql;
                                    break;
                                }
                            }
                            catch { }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(field.DataSourceSql)) builtSql = field.DataSourceSql;

                if (!string.IsNullOrEmpty(builtSql) || cmbData != null)
                {
                    ComboBox cmb = new ComboBox { Width = 250, DropDownStyle = ComboBoxStyle.DropDownList };
                    DataTable dt = cmbData ?? db.ExecuteQuery(builtSql);
                    cmb.DataSource = dt;
                    cmb.DisplayMember = "display_name";
                    cmb.ValueMember = "id";
                    flowLayoutPanel.Controls.Add(cmb);
                    _controls.Add(field.ColumnName, cmb);

                    if (_isEdit && _initialValues != null && _initialValues.ContainsKey(field.ColumnName))
                    {
                        var val = _initialValues[field.ColumnName];
                        try
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                var id = dt.Rows[i]["id"].ToString();
                                if (id == val)
                                {
                                    cmb.SelectedIndex = i;
                                    break;
                                }
                            }
                        }
                        catch { }
                    }
                }
                else
                {
                    // Use DateTimePicker for date/timestamp columns
                    string dtype = null;
                    columnTypes.TryGetValue(field.ColumnName, out dtype);
                    if (!string.IsNullOrEmpty(dtype) && (dtype.Contains("timestamp") || dtype.Contains("date")))
                    {
                        DateTimePicker dtp = new DateTimePicker { Width = 250, Format = DateTimePickerFormat.Custom, CustomFormat = "yyyy-MM-dd HH:mm:ss" };
                        flowLayoutPanel.Controls.Add(dtp);
                        _controls.Add(field.ColumnName, dtp);

                        if (_isEdit && _initialValues != null && _initialValues.ContainsKey(field.ColumnName))
                        {
                            if (DateTime.TryParse(_initialValues[field.ColumnName], out var parsed))
                                dtp.Value = parsed;
                        }
                    }
                    else
                    {
                        TextBox txt = new TextBox { Width = 250 };
                        // If this is password field, mask input and do not prefill hashed password
                        if (string.Equals(field.ColumnName, "password_hash", StringComparison.OrdinalIgnoreCase))
                        {
                            txt.UseSystemPasswordChar = true;
                            // do not prefill the hashed password when editing
                        }
                        else
                        {
                            if (_isEdit && _initialValues != null && _initialValues.ContainsKey(field.ColumnName))
                            {
                                txt.Text = _initialValues[field.ColumnName];
                            }
                        }

                        flowLayoutPanel.Controls.Add(txt);
                        _controls.Add(field.ColumnName, txt);
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validate VIN for Cars table: must be exactly 17 characters
            if (string.Equals(_tableName, "Cars", StringComparison.OrdinalIgnoreCase))
            {
                if (_controls.TryGetValue("vin_code", out var vinCtrl))
                {
                    string vinVal = "";
                    if (vinCtrl is TextBox vinTxt) vinVal = vinTxt.Text?.Trim() ?? "";
                    else if (vinCtrl is ComboBox vinCmb) vinVal = vinCmb.SelectedValue?.ToString() ?? "";

                    if (vinVal.Length != 17)
                    {
                        MessageBox.Show("VIN-код має містити рівно 17 символів.", "Помилка валідації", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            List<string> columns = new List<string>();
            List<string> values = new List<string>();
            var updateSets = new List<string>();

            DatabaseHelper db = new DatabaseHelper();

            foreach (var entry in _controls)
            {
                string col = entry.Key;

                // Handle password specially
                if (string.Equals(col, "password_hash", StringComparison.OrdinalIgnoreCase))
                {
                    if (entry.Value is TextBox pwdTxt)
                    {
                        string raw = pwdTxt.Text ?? string.Empty;
                        if (_isEdit)
                        {
                            // If empty on edit -> do not update password
                            if (string.IsNullOrEmpty(raw))
                            {
                                continue;
                            }
                        }

                        // If non-empty -> hash
                        string hashed = AutoShowroomApp.PasswordHelper.HashPassword(raw);
                        // For insert and update include hashed value
                        columns.Add(col);
                        values.Add($"'{hashed}'");
                        updateSets.Add($"{col} = '{hashed}'");
                        continue;
                    }
                }

                if (entry.Value is ComboBox cmb)
                {
                    var val = cmb.SelectedValue?.ToString();
                    columns.Add(col);
                    values.Add(val != null ? val : "NULL");
                    updateSets.Add($"{col} = {(val != null ? val : "NULL")}");
                }
                else if (entry.Value is DateTimePicker dtp)
                {
                    var literal = $"'{dtp.Value.ToString("yyyy-MM-dd HH:mm:ss")}'";
                    columns.Add(col);
                    values.Add(literal);
                    updateSets.Add($"{col} = {literal}");
                }
                else if (entry.Value is TextBox txt)
                {
                    var literal = $"'{txt.Text}'";
                    columns.Add(col);
                    values.Add(literal);
                    updateSets.Add($"{col} = {literal}");
                }
                else
                {
                    var literal = $"'{entry.Value.Text}'";
                    columns.Add(col);
                    values.Add(literal);
                    updateSets.Add($"{col} = {literal}");
                }
            }

            // Domain validations
            if (string.Equals(_tableName, "Supplies", StringComparison.OrdinalIgnoreCase))
            {
                // purchase_cost must be > 0
                if (_controls.TryGetValue("purchase_cost", out var pcCtrl))
                {
                    string pcVal = null;
                    if (pcCtrl is TextBox t) pcVal = t.Text;
                    else if (pcCtrl is ComboBox c) pcVal = c.SelectedValue?.ToString();
                    if (!string.IsNullOrEmpty(pcVal) && decimal.TryParse(pcVal, NumberStyles.Any, CultureInfo.InvariantCulture, out var pcDec))
                    {
                        if (pcDec <= 0)
                        {
                            MessageBox.Show("Вартість закупівлі має бути більше 0.", "Помилка валідації", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
            }

            if (string.Equals(_tableName, "Sales", StringComparison.OrdinalIgnoreCase))
            {
                // final_price must be > 0
                if (_controls.TryGetValue("final_price", out var fpCtrl))
                {
                    string fpVal = null;
                    if (fpCtrl is TextBox t) fpVal = t.Text;
                    else if (fpCtrl is ComboBox c) fpVal = c.SelectedValue?.ToString();
                    if (!string.IsNullOrEmpty(fpVal) && decimal.TryParse(fpVal, NumberStyles.Any, CultureInfo.InvariantCulture, out var fpDec))
                    {
                        if (fpDec <= 0)
                        {
                            MessageBox.Show("Фінальна вартість має бути більше 0.", "Помилка валідації", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }

                // sale_date cannot be earlier than latest supply_date for that car
                if (_controls.TryGetValue("sale_date", out var sdCtrl) && _controls.TryGetValue("car_id", out var carCtrl))
                {
                    DateTime saleDate;
                    if (sdCtrl is DateTimePicker sdtp) saleDate = sdtp.Value;
                    else if (sdCtrl is TextBox sdtxt && DateTime.TryParse(sdtxt.Text, out var tmp)) saleDate = tmp;
                    else saleDate = DateTime.MinValue;

                    string carId = null;
                    if (carCtrl is ComboBox cc) carId = cc.SelectedValue?.ToString();
                    else if (carCtrl is TextBox ct) carId = ct.Text;

                    if (!string.IsNullOrEmpty(carId) && saleDate != DateTime.MinValue)
                    {
                        try
                        {
                            if (!int.TryParse(carId, out var carIdInt))
                            {
                                MessageBox.Show("Невірний ідентифікатор автомобіля.", "Помилка валідації", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            var db2 = new DatabaseHelper();
                            using var conn = new NpgsqlConnection(db2.GetConnectionString());
                            conn.Open();
                            using var cmd = new NpgsqlCommand("SELECT MAX(supply_date) FROM Supplies WHERE car_id = @car", conn);
                            cmd.Parameters.AddWithValue("car", carIdInt);
                            var res = cmd.ExecuteScalar();

                            if (res == null || res == DBNull.Value)
                            {
                                MessageBox.Show("Для обраного автомобіля немає записів про поставку. Неможливо оформити продаж.", "Помилка валідації", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            DateTime lastSupply;
                            if (res is DateTime dt)
                                lastSupply = dt;
                            else if (res is DateOnly d)
                                lastSupply = d.ToDateTime(System.TimeOnly.MinValue);
                            else
                                lastSupply = Convert.ToDateTime(res);

                            // Require sale date to be strictly after supply date
                            if (saleDate <= lastSupply)
                            {
                                MessageBox.Show("Дата продажу має бути пізніше дати останньої поставки цього автомобіля.", "Помилка валідації", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            // If DB check fails, show message
                            MessageBox.Show("Не вдалося перевірити дату поставки: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
            }

            if (_isEdit)
            {
                if (updateSets.Count == 0)
                {
                    MessageBox.Show("Немає змін для збереження.");
                    return;
                }
                string sql = $"UPDATE {_tableName} SET {string.Join(", ", updateSets)} WHERE {_primaryKey} = {_primaryKeyValue}";
                db.ExecuteNonQuery(sql);

                MessageBox.Show("Дані успішно оновлено!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                string sql = $"INSERT INTO {_tableName} ({string.Join(", ", columns)}) VALUES ({string.Join(", ", values)})";
                db.ExecuteNonQuery(sql);

                MessageBox.Show("Дані успішно додано!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}