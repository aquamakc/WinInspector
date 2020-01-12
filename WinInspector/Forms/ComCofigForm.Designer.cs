namespace WinInspector.Forms
{
    partial class ComCofigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cb_portName = new System.Windows.Forms.ComboBox();
            this.cb_baudRates = new System.Windows.Forms.ComboBox();
            this.cb_dataBits = new System.Windows.Forms.ComboBox();
            this.cb_parity = new System.Windows.Forms.ComboBox();
            this.cb_stopBits = new System.Windows.Forms.ComboBox();
            this.b_ok = new System.Windows.Forms.Button();
            this.b_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cb_portName
            // 
            this.cb_portName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_portName.FormattingEnabled = true;
            this.cb_portName.Location = new System.Drawing.Point(12, 12);
            this.cb_portName.Name = "cb_portName";
            this.cb_portName.Size = new System.Drawing.Size(163, 21);
            this.cb_portName.TabIndex = 0;
            // 
            // cb_baudRates
            // 
            this.cb_baudRates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_baudRates.FormattingEnabled = true;
            this.cb_baudRates.Location = new System.Drawing.Point(12, 39);
            this.cb_baudRates.Name = "cb_baudRates";
            this.cb_baudRates.Size = new System.Drawing.Size(163, 21);
            this.cb_baudRates.TabIndex = 1;
            // 
            // cb_dataBits
            // 
            this.cb_dataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_dataBits.FormattingEnabled = true;
            this.cb_dataBits.Location = new System.Drawing.Point(12, 66);
            this.cb_dataBits.Name = "cb_dataBits";
            this.cb_dataBits.Size = new System.Drawing.Size(163, 21);
            this.cb_dataBits.TabIndex = 2;
            // 
            // cb_parity
            // 
            this.cb_parity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_parity.FormattingEnabled = true;
            this.cb_parity.Location = new System.Drawing.Point(12, 93);
            this.cb_parity.Name = "cb_parity";
            this.cb_parity.Size = new System.Drawing.Size(163, 21);
            this.cb_parity.TabIndex = 3;
            // 
            // cb_stopBits
            // 
            this.cb_stopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_stopBits.FormattingEnabled = true;
            this.cb_stopBits.Location = new System.Drawing.Point(12, 120);
            this.cb_stopBits.Name = "cb_stopBits";
            this.cb_stopBits.Size = new System.Drawing.Size(163, 21);
            this.cb_stopBits.TabIndex = 4;
            // 
            // b_ok
            // 
            this.b_ok.Location = new System.Drawing.Point(12, 147);
            this.b_ok.Name = "b_ok";
            this.b_ok.Size = new System.Drawing.Size(75, 23);
            this.b_ok.TabIndex = 5;
            this.b_ok.Text = "OK";
            this.b_ok.UseVisualStyleBackColor = true;
            this.b_ok.Click += new System.EventHandler(this.b_ok_Click);
            // 
            // b_cancel
            // 
            this.b_cancel.Location = new System.Drawing.Point(100, 147);
            this.b_cancel.Name = "b_cancel";
            this.b_cancel.Size = new System.Drawing.Size(75, 23);
            this.b_cancel.TabIndex = 6;
            this.b_cancel.Text = "Cancel";
            this.b_cancel.UseVisualStyleBackColor = true;
            this.b_cancel.Click += new System.EventHandler(this.b_cancel_Click);
            // 
            // ComCofigForm
            // 
            this.AcceptButton = this.b_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.b_cancel;
            this.ClientSize = new System.Drawing.Size(187, 178);
            this.Controls.Add(this.b_cancel);
            this.Controls.Add(this.b_ok);
            this.Controls.Add(this.cb_stopBits);
            this.Controls.Add(this.cb_parity);
            this.Controls.Add(this.cb_dataBits);
            this.Controls.Add(this.cb_baudRates);
            this.Controls.Add(this.cb_portName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ComCofigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройка порта";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cb_portName;
        private System.Windows.Forms.ComboBox cb_baudRates;
        private System.Windows.Forms.ComboBox cb_dataBits;
        private System.Windows.Forms.ComboBox cb_parity;
        private System.Windows.Forms.ComboBox cb_stopBits;
        private System.Windows.Forms.Button b_ok;
        private System.Windows.Forms.Button b_cancel;
    }
}