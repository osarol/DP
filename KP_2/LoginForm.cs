using System;
using System.Data;
using System.Windows.Forms;
using AutoShowroomApp;

namespace KP_2
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Будь ласка, введіть логін та пароль");
                return;
            }

            DatabaseHelper db = new DatabaseHelper();

            string sql = $"SELECT * FROM Employees WHERE login = '{login}' AND password_hash = '{password}'";

            DataTable result = db.ExecuteQuery(sql);

            if (result.Rows.Count > 0)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Невірний логін або пароль!");
            }
        }
    }
}