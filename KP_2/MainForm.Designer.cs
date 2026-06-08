namespace KP_2
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            menuStrip = new MenuStrip();
            файлToolStripMenuItem = new ToolStripMenuItem();
            відкритиБДToolStripMenuItem = new ToolStripMenuItem();
            СтворитиБДToolStripMenuItem = new ToolStripMenuItem();
            зберегтиБДToolStripMenuItem = new ToolStripMenuItem();
            клієнтToolStripMenuItem = new ToolStripMenuItem();
            даніАвтомобіліToolStripMenuItem = new ToolStripMenuItem();
            даніКлієнтиToolStripMenuItem = new ToolStripMenuItem();
            даніМаркиToolStripMenuItem = new ToolStripMenuItem();
            даніПосадиToolStripMenuItem = new ToolStripMenuItem();
            даніПоставкиToolStripMenuItem = new ToolStripMenuItem();
            ДаніПостачальникиToolStripMenuItem = new ToolStripMenuItem();
            даніОстанніПродажіToolStripMenuItem = new ToolStripMenuItem();
            даніСпівробітникиToolStripMenuItem = new ToolStripMenuItem();
            даніТестдрайвиToolStripMenuItem = new ToolStripMenuItem();
            даніТипиКузоваToolStripMenuItem = new ToolStripMenuItem();
            додатиToolStripMenuItem = new ToolStripMenuItem();
            автомобільToolStripMenuItem = new ToolStripMenuItem();
            брендToolStripMenuItem = new ToolStripMenuItem();
            клієнтаToolStripMenuItem = new ToolStripMenuItem();
            постачальникаToolStripMenuItem = new ToolStripMenuItem();
            поставкуToolStripMenuItem = new ToolStripMenuItem();
            продажToolStripMenuItem = new ToolStripMenuItem();
            посадуToolStripMenuItem = new ToolStripMenuItem();
            працівникаToolStripMenuItem = new ToolStripMenuItem();
            тестдрайвToolStripMenuItem = new ToolStripMenuItem();
            типКузоваToolStripMenuItem = new ToolStripMenuItem();
            зведеніДаніToolStripMenuItem = new ToolStripMenuItem();
            звітАвтомобіліToolStripMenuItem = new ToolStripMenuItem();
            журналПродажівToolStripMenuItem = new ToolStripMenuItem();
            аналітикаБрендівToolStripMenuItem = new ToolStripMenuItem();
            редагуванняToolStripMenuItem = new ToolStripMenuItem();
            видалитиЗаписToolStripMenuItem = new ToolStripMenuItem();
            пошукToolStripMenuItem = new ToolStripMenuItem();
            dgvMain = new DataGridView();
            редагуватиЗаписToolStripMenuItem = new ToolStripMenuItem();
            menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMain).BeginInit();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { файлToolStripMenuItem, клієнтToolStripMenuItem, додатиToolStripMenuItem, зведеніДаніToolStripMenuItem, редагуванняToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(984, 24);
            menuStrip.TabIndex = 3;
            menuStrip.Text = "menuStrip2";
            // 
            // файлToolStripMenuItem
            // 
            файлToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { відкритиБДToolStripMenuItem, СтворитиБДToolStripMenuItem, зберегтиБДToolStripMenuItem });
            файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            файлToolStripMenuItem.Size = new Size(48, 20);
            файлToolStripMenuItem.Text = "Файл";
            // 
            // відкритиБДToolStripMenuItem
            // 
            відкритиБДToolStripMenuItem.Name = "відкритиБДToolStripMenuItem";
            відкритиБДToolStripMenuItem.Size = new Size(180, 22);
            відкритиБДToolStripMenuItem.Text = "Відкрити БД";
            відкритиБДToolStripMenuItem.Click += відкритиБДToolStripMenuItem_Click;
            // 
            // СтворитиБДToolStripMenuItem
            // 
            СтворитиБДToolStripMenuItem.Name = "СтворитиБДToolStripMenuItem";
            СтворитиБДToolStripMenuItem.Size = new Size(180, 22);
            СтворитиБДToolStripMenuItem.Text = "Створити БД";
            СтворитиБДToolStripMenuItem.Click += СтворитиБДToolStripMenuItem_Click;
            // 
            // зберегтиБДToolStripMenuItem
            // 
            зберегтиБДToolStripMenuItem.Name = "зберегтиБДToolStripMenuItem";
            зберегтиБДToolStripMenuItem.Size = new Size(180, 22);
            зберегтиБДToolStripMenuItem.Text = "Зберегти БД";
            зберегтиБДToolStripMenuItem.Click += зберегтиБДToolStripMenuItem_Click;
            // 
            // клієнтToolStripMenuItem
            // 
            клієнтToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { даніАвтомобіліToolStripMenuItem, даніКлієнтиToolStripMenuItem, даніМаркиToolStripMenuItem, даніПосадиToolStripMenuItem, даніПоставкиToolStripMenuItem, ДаніПостачальникиToolStripMenuItem, даніОстанніПродажіToolStripMenuItem, даніСпівробітникиToolStripMenuItem, даніТестдрайвиToolStripMenuItem, даніТипиКузоваToolStripMenuItem });
            клієнтToolStripMenuItem.Name = "клієнтToolStripMenuItem";
            клієнтToolStripMenuItem.Size = new Size(43, 20);
            клієнтToolStripMenuItem.Text = "Дані";
            // 
            // даніАвтомобіліToolStripMenuItem
            // 
            даніАвтомобіліToolStripMenuItem.Name = "даніАвтомобіліToolStripMenuItem";
            даніАвтомобіліToolStripMenuItem.Size = new Size(160, 22);
            даніАвтомобіліToolStripMenuItem.Text = "Автомобілі";
            даніАвтомобіліToolStripMenuItem.Click += даніАвтомобіліToolStripMenuItem_Click;
            // 
            // даніКлієнтиToolStripMenuItem
            // 
            даніКлієнтиToolStripMenuItem.Name = "даніКлієнтиToolStripMenuItem";
            даніКлієнтиToolStripMenuItem.Size = new Size(160, 22);
            даніКлієнтиToolStripMenuItem.Text = "Клієнти";
            даніКлієнтиToolStripMenuItem.Click += даніКлієнтиToolStripMenuItem_Click;
            // 
            // даніМаркиToolStripMenuItem
            // 
            даніМаркиToolStripMenuItem.Name = "даніМаркиToolStripMenuItem";
            даніМаркиToolStripMenuItem.Size = new Size(160, 22);
            даніМаркиToolStripMenuItem.Text = "Марки";
            даніМаркиToolStripMenuItem.Click += даніМаркиToolStripMenuItem_Click;
            // 
            // даніПосадиToolStripMenuItem
            // 
            даніПосадиToolStripMenuItem.Name = "даніПосадиToolStripMenuItem";
            даніПосадиToolStripMenuItem.Size = new Size(160, 22);
            даніПосадиToolStripMenuItem.Text = "Посади";
            даніПосадиToolStripMenuItem.Click += даніПосадиToolStripMenuItem_Click;
            // 
            // даніПоставкиToolStripMenuItem
            // 
            даніПоставкиToolStripMenuItem.Name = "даніПоставкиToolStripMenuItem";
            даніПоставкиToolStripMenuItem.Size = new Size(160, 22);
            даніПоставкиToolStripMenuItem.Text = "Поставки";
            даніПоставкиToolStripMenuItem.Click += даніПоставкиToolStripMenuItem_Click;
            // 
            // ДаніПостачальникиToolStripMenuItem
            // 
            ДаніПостачальникиToolStripMenuItem.Name = "ДаніПостачальникиToolStripMenuItem";
            ДаніПостачальникиToolStripMenuItem.Size = new Size(160, 22);
            ДаніПостачальникиToolStripMenuItem.Text = "Постачальники";
            ДаніПостачальникиToolStripMenuItem.Click += ДаніПостачальникиToolStripMenuItem_Click;
            // 
            // даніОстанніПродажіToolStripMenuItem
            // 
            даніОстанніПродажіToolStripMenuItem.Name = "даніОстанніПродажіToolStripMenuItem";
            даніОстанніПродажіToolStripMenuItem.Size = new Size(160, 22);
            даніОстанніПродажіToolStripMenuItem.Text = "Продажі";
            даніОстанніПродажіToolStripMenuItem.Click += даніОстанніПродажіToolStripMenuItem_Click;
            // 
            // даніСпівробітникиToolStripMenuItem
            // 
            даніСпівробітникиToolStripMenuItem.Name = "даніСпівробітникиToolStripMenuItem";
            даніСпівробітникиToolStripMenuItem.Size = new Size(160, 22);
            даніСпівробітникиToolStripMenuItem.Text = "Співробітники";
            даніСпівробітникиToolStripMenuItem.Click += даніСпівробітникиToolStripMenuItem_Click;
            // 
            // даніТестдрайвиToolStripMenuItem
            // 
            даніТестдрайвиToolStripMenuItem.Name = "даніТестдрайвиToolStripMenuItem";
            даніТестдрайвиToolStripMenuItem.Size = new Size(160, 22);
            даніТестдрайвиToolStripMenuItem.Text = "Тестдрайви";
            даніТестдрайвиToolStripMenuItem.Click += даніТестдрайвиToolStripMenuItem_Click;
            // 
            // даніТипиКузоваToolStripMenuItem
            // 
            даніТипиКузоваToolStripMenuItem.Name = "даніТипиКузоваToolStripMenuItem";
            даніТипиКузоваToolStripMenuItem.Size = new Size(160, 22);
            даніТипиКузоваToolStripMenuItem.Text = "Типи кузова";
            даніТипиКузоваToolStripMenuItem.Click += даніТипиКузоваToolStripMenuItem_Click;
            // 
            // додатиToolStripMenuItem
            // 
            додатиToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { автомобільToolStripMenuItem, брендToolStripMenuItem, клієнтаToolStripMenuItem, постачальникаToolStripMenuItem, поставкуToolStripMenuItem, продажToolStripMenuItem, посадуToolStripMenuItem, працівникаToolStripMenuItem, тестдрайвToolStripMenuItem, типКузоваToolStripMenuItem });
            додатиToolStripMenuItem.Name = "додатиToolStripMenuItem";
            додатиToolStripMenuItem.Size = new Size(58, 20);
            додатиToolStripMenuItem.Text = "Додати";
            // 
            // автомобільToolStripMenuItem
            // 
            автомобільToolStripMenuItem.Name = "автомобільToolStripMenuItem";
            автомобільToolStripMenuItem.Size = new Size(159, 22);
            автомобільToolStripMenuItem.Text = "Автомобіль";
            автомобільToolStripMenuItem.Click += додатиАвтомобільToolStripMenuItem_Click;
            // 
            // брендToolStripMenuItem
            // 
            брендToolStripMenuItem.Name = "брендToolStripMenuItem";
            брендToolStripMenuItem.Size = new Size(159, 22);
            брендToolStripMenuItem.Text = "Марку";
            брендToolStripMenuItem.Click += додатиБрендToolStripMenuItem_Click;
            // 
            // клієнтаToolStripMenuItem
            // 
            клієнтаToolStripMenuItem.Name = "клієнтаToolStripMenuItem";
            клієнтаToolStripMenuItem.Size = new Size(159, 22);
            клієнтаToolStripMenuItem.Text = "Клієнта";
            клієнтаToolStripMenuItem.Click += клієнтаToolStripMenuItem_Click;
            // 
            // постачальникаToolStripMenuItem
            // 
            постачальникаToolStripMenuItem.Name = "постачальникаToolStripMenuItem";
            постачальникаToolStripMenuItem.Size = new Size(159, 22);
            постачальникаToolStripMenuItem.Text = "Постачальника";
            постачальникаToolStripMenuItem.Click += додатиПостачальникаToolStripMenuItem_Click;
            // 
            // поставкуToolStripMenuItem
            // 
            поставкуToolStripMenuItem.Name = "поставкуToolStripMenuItem";
            поставкуToolStripMenuItem.Size = new Size(159, 22);
            поставкуToolStripMenuItem.Text = "Поставку";
            поставкуToolStripMenuItem.Click += поставкуToolStripMenuItem_Click;
            // 
            // продажToolStripMenuItem
            // 
            продажToolStripMenuItem.Name = "продажToolStripMenuItem";
            продажToolStripMenuItem.Size = new Size(159, 22);
            продажToolStripMenuItem.Text = "Продаж";
            продажToolStripMenuItem.Click += продажToolStripMenuItem_Click;
            // 
            // посадуToolStripMenuItem
            // 
            посадуToolStripMenuItem.Name = "посадуToolStripMenuItem";
            посадуToolStripMenuItem.Size = new Size(159, 22);
            посадуToolStripMenuItem.Text = "Посаду";
            посадуToolStripMenuItem.Click += посадуToolStripMenuItem_Click;
            // 
            // працівникаToolStripMenuItem
            // 
            працівникаToolStripMenuItem.Name = "працівникаToolStripMenuItem";
            працівникаToolStripMenuItem.Size = new Size(159, 22);
            працівникаToolStripMenuItem.Text = "Працівника";
            працівникаToolStripMenuItem.Click += працівникаToolStripMenuItem_Click;
            // 
            // тестдрайвToolStripMenuItem
            // 
            тестдрайвToolStripMenuItem.Name = "тестдрайвToolStripMenuItem";
            тестдрайвToolStripMenuItem.Size = new Size(159, 22);
            тестдрайвToolStripMenuItem.Text = "Тестдрайв";
            тестдрайвToolStripMenuItem.Click += тестдрайвToolStripMenuItem_Click;
            // 
            // типКузоваToolStripMenuItem
            // 
            типКузоваToolStripMenuItem.Name = "типКузоваToolStripMenuItem";
            типКузоваToolStripMenuItem.Size = new Size(159, 22);
            типКузоваToolStripMenuItem.Text = "Тип кузова";
            типКузоваToolStripMenuItem.Click += типКузоваToolStripMenuItem_Click;
            // 
            // зведеніДаніToolStripMenuItem
            // 
            зведеніДаніToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { звітАвтомобіліToolStripMenuItem, журналПродажівToolStripMenuItem, аналітикаБрендівToolStripMenuItem });
            зведеніДаніToolStripMenuItem.Name = "зведеніДаніToolStripMenuItem";
            зведеніДаніToolStripMenuItem.Size = new Size(85, 20);
            зведеніДаніToolStripMenuItem.Text = "Зведені дані";
            // 
            // звітАвтомобіліToolStripMenuItem
            // 
            звітАвтомобіліToolStripMenuItem.Name = "звітАвтомобіліToolStripMenuItem";
            звітАвтомобіліToolStripMenuItem.Size = new Size(174, 22);
            звітАвтомобіліToolStripMenuItem.Text = "Звіт автомобілі";
            звітАвтомобіліToolStripMenuItem.Click += звітАвтомобіліToolStripMenuItem_Click;
            // 
            // журналПродажівToolStripMenuItem
            // 
            журналПродажівToolStripMenuItem.Name = "журналПродажівToolStripMenuItem";
            журналПродажівToolStripMenuItem.Size = new Size(174, 22);
            журналПродажівToolStripMenuItem.Text = "Журнал продажів";
            журналПродажівToolStripMenuItem.Click += журналПродажівToolStripMenuItem_Click;
            // 
            // аналітикаБрендівToolStripMenuItem
            // 
            аналітикаБрендівToolStripMenuItem.Name = "аналітикаБрендівToolStripMenuItem";
            аналітикаБрендівToolStripMenuItem.Size = new Size(174, 22);
            аналітикаБрендівToolStripMenuItem.Text = "Аналітика брендів";
            аналітикаБрендівToolStripMenuItem.Click += аналітикаБрендівToolStripMenuItem_Click;
            // 
            // редагуванняToolStripMenuItem
            // 
            редагуванняToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { видалитиЗаписToolStripMenuItem, пошукToolStripMenuItem, редагуватиЗаписToolStripMenuItem });
            редагуванняToolStripMenuItem.Name = "редагуванняToolStripMenuItem";
            редагуванняToolStripMenuItem.Size = new Size(87, 20);
            редагуванняToolStripMenuItem.Text = "Редагування";
            // 
            // видалитиЗаписToolStripMenuItem
            // 
            видалитиЗаписToolStripMenuItem.Name = "видалитиЗаписToolStripMenuItem";
            видалитиЗаписToolStripMenuItem.Size = new Size(180, 22);
            видалитиЗаписToolStripMenuItem.Text = "Видалити запис";
            видалитиЗаписToolStripMenuItem.Click += видалитиЗаписToolStripMenuItem_Click;
            // 
            // пошукToolStripMenuItem
            // 
            пошукToolStripMenuItem.Name = "пошукToolStripMenuItem";
            пошукToolStripMenuItem.Size = new Size(180, 22);
            пошукToolStripMenuItem.Text = "Пошук";
            пошукToolStripMenuItem.Click += пошукToolStripMenuItem_Click;
            // 
            // dgvMain
            // 
            dgvMain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMain.Dock = DockStyle.Fill;
            dgvMain.Location = new Point(0, 24);
            dgvMain.Name = "dgvMain";
            dgvMain.ReadOnly = true;
            dgvMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMain.Size = new Size(984, 637);
            dgvMain.TabIndex = 4;
            // 
            // редагуватиЗаписToolStripMenuItem
            // 
            редагуватиЗаписToolStripMenuItem.Name = "редагуватиЗаписToolStripMenuItem";
            редагуватиЗаписToolStripMenuItem.Size = new Size(180, 22);
            редагуватиЗаписToolStripMenuItem.Text = "Редагувати запис";
            редагуватиЗаписToolStripMenuItem.Click += редагуватиЗаписToolStripMenuItem_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 661);
            Controls.Add(dgvMain);
            Controls.Add(menuStrip);
            Name = "MainForm";
            Text = "Автосалон - Головне меню";
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMain).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MenuStrip menuStrip1;
        private MenuStrip menuStrip;
        private ToolStripMenuItem файлToolStripMenuItem;
        private ToolStripMenuItem відкритиБДToolStripMenuItem;
        private ToolStripMenuItem клієнтToolStripMenuItem;
        private ToolStripMenuItem СтворитиБДToolStripMenuItem;
        private ToolStripMenuItem даніОстанніПродажіToolStripMenuItem;
        private ToolStripMenuItem даніАвтомобіліToolStripMenuItem;
        private ToolStripMenuItem даніКлієнтиToolStripMenuItem;
        private ToolStripMenuItem даніСпівробітникиToolStripMenuItem;
        private ToolStripMenuItem додатиToolStripMenuItem;
        private ToolStripMenuItem автомобільToolStripMenuItem;
        private ToolStripMenuItem клієнтаToolStripMenuItem;
        private ToolStripMenuItem брендToolStripMenuItem;
        private ToolStripMenuItem постачальникаToolStripMenuItem;
        private DataGridView dgvMain;
        private ToolStripMenuItem даніПоставкиToolStripMenuItem;
        private ToolStripMenuItem редагуванняToolStripMenuItem;
        private ToolStripMenuItem видалитиЗаписToolStripMenuItem;
        private ToolStripMenuItem пошукToolStripMenuItem;
        private ToolStripMenuItem ДаніПостачальникиToolStripMenuItem;
        private ToolStripMenuItem даніТестдрайвиToolStripMenuItem;
        private ToolStripMenuItem даніМаркиToolStripMenuItem;
        private ToolStripMenuItem даніПосадиToolStripMenuItem;
        private ToolStripMenuItem даніТипиКузоваToolStripMenuItem;
        private ToolStripMenuItem зведеніДаніToolStripMenuItem;
        private ToolStripMenuItem звітАвтомобіліToolStripMenuItem;
        private ToolStripMenuItem журналПродажівToolStripMenuItem;
        private ToolStripMenuItem аналітикаБрендівToolStripMenuItem;
        private ToolStripMenuItem поставкуToolStripMenuItem;
        private ToolStripMenuItem продажToolStripMenuItem;
        private ToolStripMenuItem посадуToolStripMenuItem;
        private ToolStripMenuItem працівникаToolStripMenuItem;
        private ToolStripMenuItem тестдрайвToolStripMenuItem;
        private ToolStripMenuItem типКузоваToolStripMenuItem;
        private ToolStripMenuItem зберегтиБДToolStripMenuItem;
        private ToolStripMenuItem редагуватиЗаписToolStripMenuItem;
    }
}