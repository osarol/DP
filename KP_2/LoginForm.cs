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

            // Use parameterized query to fetch stored hash and role
            string sql = $"SELECT employee_id, full_name, password_hash, position_id FROM Employees WHERE login = @login LIMIT 1";
            try
            {
                using var conn = new Npgsql.NpgsqlConnection(db.GetConnectionString());
                using var cmd = new Npgsql.NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("login", login);
                conn.Open();
                using var rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    var id = rdr.IsDBNull(0) ? -1 : rdr.GetInt32(0);
                    var fullName = rdr.IsDBNull(1) ? string.Empty : rdr.GetString(1);
                    var stored = rdr.IsDBNull(2) ? string.Empty : rdr.GetString(2);
                    var positionId = rdr.IsDBNull(3) ? (int?)null : rdr.GetInt32(3);

                    if (!string.IsNullOrEmpty(stored) && AutoShowroomApp.PasswordHelper.VerifyPassword(password, stored))
                    {
                        // Populate session (KP_2.UserSession)
                        UserSession.IsAuthenticated = true;
                        UserSession.UserId = id;
                        UserSession.FullName = fullName;

                        // Grant Admin only to accounts whose login contains 'admin' (case-insensitive)
                        if (login.IndexOf("admin", StringComparison.OrdinalIgnoreCase) >= 0)
                            UserSession.Role = UserRole.Admin;
                        else
                            UserSession.Role = UserRole.Seller;

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка автентифікації: " + ex.Message);
                return;
            }
            MessageBox.Show("Невірний логін або пароль!");
        }
    }
}