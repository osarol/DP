using AutoShowroomApp;
using System;
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

        public AddForm(string title, string tableName, List<FieldConfig> fields)
        {
            InitializeComponent();
            this.Text = title;
            this._tableName = tableName;
            this._fields = fields;
            GenerateUI();
        }

        private void GenerateUI()
        {
            DatabaseHelper db = new DatabaseHelper();

            foreach (var field in _fields)
            {
                Label lbl = new Label { Text = field.DisplayName, AutoSize = true };
                flowLayoutPanel.Controls.Add(lbl);

                if (!string.IsNullOrEmpty(field.DataSourceSql))
                {
                    ComboBox cmb = new ComboBox { Width = 250, DropDownStyle = ComboBoxStyle.DropDownList };
                    DataTable dt = db.ExecuteQuery(field.DataSourceSql);
                    cmb.DataSource = dt;
                    cmb.DisplayMember = "display_name";
                    cmb.ValueMember = "id";
                    flowLayoutPanel.Controls.Add(cmb);
                    _controls.Add(field.ColumnName, cmb);
                }
                else
                {
                    TextBox txt = new TextBox { Width = 250 };
                    flowLayoutPanel.Controls.Add(txt);
                    _controls.Add(field.ColumnName, txt);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<string> columns = new List<string>();
            List<string> values = new List<string>();

            foreach (var entry in _controls)
            {
                columns.Add(entry.Key);
                if (entry.Value is ComboBox cmb)
                {
                    values.Add(cmb.SelectedValue.ToString());
                }
                else
                {
                    values.Add($"'{entry.Value.Text}'");
                }
            }

            string sql = $"INSERT INTO {_tableName} ({string.Join(", ", columns)}) VALUES ({string.Join(", ", values)})";

            DatabaseHelper db = new DatabaseHelper();
            db.ExecuteNonQuery(sql);

            MessageBox.Show("Дані успішно додано!");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}