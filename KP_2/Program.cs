using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace KP_2
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // ≤н≥ц≥ал≥зац≥€ налаштувань програми
            ApplicationConfiguration.Initialize();

            // —творюЇмо форму лог≥ну
            LoginForm login = new LoginForm();

            // ѕоказуЇмо њњ ≥ чекаЇмо на результат
            if (login.ShowDialog() == DialogResult.OK)
            {
                // якщо в LoginForm встановлено DialogResult.OK Ч запускаЇмо головну форму
                Application.Run(new MainForm());
            }
            else
            {
                // якщо користувач закрив в≥кно або натиснув скасувати Ч виходимо
                Application.Exit();
            }
        }
    }
}