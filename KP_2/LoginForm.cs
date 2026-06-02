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

            // Use parameterized query to fetch stored hash
            string sql = $"SELECT password_hash FROM Employees WHERE login = @login LIMIT 1";
            try
            {
                using var conn = new Npgsql.NpgsqlConnection(db.GetConnectionString());
                using var cmd = new Npgsql.NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("login", login);
                conn.Open();
                var stored = cmd.ExecuteScalar() as string;
                if (!string.IsNullOrEmpty(stored) && AutoShowroomApp.PasswordHelper.VerifyPassword(password, stored))
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка автентифікації: " + ex.Message);
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
            //MessageBox.Show("Невірний логін або пароль!");
        }
    }
}