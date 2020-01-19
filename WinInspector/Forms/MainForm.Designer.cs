namespace WinInspector.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.bPortConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.bPortOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.bPortClose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.bDevConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.bReadParams = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.lPortName = new System.Windows.Forms.ToolStripLabel();
            this.lPortStat = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.l_readedTime = new System.Windows.Forms.ToolStripLabel();
            this.l_voltaage = new System.Windows.Forms.Label();
            this.l_current = new System.Windows.Forms.Label();
            this.l_power = new System.Windows.Forms.Label();
            this.l_frequency = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bPortConfig,
            this.bPortOpen,
            this.bPortClose});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(47, 20);
            this.toolStripMenuItem1.Text = "Порт";
            // 
            // bPortConfig
            // 
            this.bPortConfig.Name = "bPortConfig";
            this.bPortConfig.Size = new System.Drawing.Size(133, 22);
            this.bPortConfig.Text = "Настройка";
            this.bPortConfig.Click += new System.EventHandler(this.bPortConfig_Click);
            // 
            // bPortOpen
            // 
            this.bPortOpen.Name = "bPortOpen";
            this.bPortOpen.Size = new System.Drawing.Size(133, 22);
            this.bPortOpen.Text = "Открыть";
            this.bPortOpen.Click += new System.EventHandler(this.bPortOpen_Click);
            // 
            // bPortClose
            // 
            this.bPortClose.Enabled = false;
            this.bPortClose.Name = "bPortClose";
            this.bPortClose.Size = new System.Drawing.Size(133, 22);
            this.bPortClose.Text = "Закрыть";
            this.bPortClose.Click += new System.EventHandler(this.bPortClose_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bDevConfig,
            this.bReadParams});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(82, 20);
            this.toolStripMenuItem2.Text = "Устройство";
            // 
            // bDevConfig
            // 
            this.bDevConfig.Enabled = false;
            this.bDevConfig.Name = "bDevConfig";
            this.bDevConfig.Size = new System.Drawing.Size(176, 22);
            this.bDevConfig.Text = "Настройки";
            // 
            // bReadParams
            // 
            this.bReadParams.Enabled = false;
            this.bReadParams.Name = "bReadParams";
            this.bReadParams.Size = new System.Drawing.Size(176, 22);
            this.bReadParams.Text = "Читать параметры";
            this.bReadParams.Click += new System.EventHandler(this.bReadParams_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lPortName,
            this.lPortStat,
            this.toolStripSeparator1,
            this.l_readedTime});
            this.toolStrip1.Location = new System.Drawing.Point(0, 425);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // lPortName
            // 
            this.lPortName.Name = "lPortName";
            this.lPortName.Size = new System.Drawing.Size(35, 22);
            this.lPortName.Text = "COM";
            // 
            // lPortStat
            // 
            this.lPortStat.Name = "lPortStat";
            this.lPortStat.Size = new System.Drawing.Size(47, 22);
            this.lPortStat.Text = "Закрыт";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // l_readedTime
            // 
            this.l_readedTime.Name = "l_readedTime";
            this.l_readedTime.Size = new System.Drawing.Size(110, 22);
            this.l_readedTime.Text = "01/01/1960 01:01:01";
            // 
            // l_voltaage
            // 
            this.l_voltaage.AutoSize = true;
            this.l_voltaage.Location = new System.Drawing.Point(100, 81);
            this.l_voltaage.Name = "l_voltaage";
            this.l_voltaage.Size = new System.Drawing.Size(35, 13);
            this.l_voltaage.TabIndex = 2;
            this.l_voltaage.Text = "label1";
            // 
            // l_current
            // 
            this.l_current.AutoSize = true;
            this.l_current.Location = new System.Drawing.Point(100, 129);
            this.l_current.Name = "l_current";
            this.l_current.Size = new System.Drawing.Size(35, 13);
            this.l_current.TabIndex = 3;
            this.l_current.Text = "label2";
            // 
            // l_power
            // 
            this.l_power.AutoSize = true;
            this.l_power.Location = new System.Drawing.Point(100, 179);
            this.l_power.Name = "l_power";
            this.l_power.Size = new System.Drawing.Size(35, 13);
            this.l_power.TabIndex = 4;
            this.l_power.Text = "label3";
            // 
            // l_frequency
            // 
            this.l_frequency.AutoSize = true;
            this.l_frequency.Location = new System.Drawing.Point(100, 219);
            this.l_frequency.Name = "l_frequency";
            this.l_frequency.Size = new System.Drawing.Size(35, 13);
            this.l_frequency.TabIndex = 5;
            this.l_frequency.Text = "label4";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(312, 129);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.l_frequency);
            this.Controls.Add(this.l_power);
            this.Controls.Add(this.l_current);
            this.Controls.Add(this.l_voltaage);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Инспектор";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem bPortConfig;
        private System.Windows.Forms.ToolStripMenuItem bPortOpen;
        private System.Windows.Forms.ToolStripMenuItem bPortClose;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem bReadParams;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel lPortName;
        private System.Windows.Forms.ToolStripLabel lPortStat;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel l_readedTime;
        private System.Windows.Forms.ToolStripMenuItem bDevConfig;
        private System.Windows.Forms.Label l_voltaage;
        private System.Windows.Forms.Label l_current;
        private System.Windows.Forms.Label l_power;
        private System.Windows.Forms.Label l_frequency;
        private System.Windows.Forms.Button button1;
    }
}

