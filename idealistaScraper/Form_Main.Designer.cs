namespace idealista
{
    partial class Form_Main
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
            this.textBox_DBHost = new System.Windows.Forms.TextBox();
            this.textBox_DBPort = new System.Windows.Forms.TextBox();
            this.textBox_DBUser = new System.Windows.Forms.TextBox();
            this.textBox_DBPassword = new System.Windows.Forms.TextBox();
            this.button_SaveSetting = new System.Windows.Forms.Button();
            this.textBox_DBDatabase = new System.Windows.Forms.TextBox();
            this.button_Start = new System.Windows.Forms.Button();
            this.textBox_RootPath = new System.Windows.Forms.TextBox();
            this.checkBox_ForceUpdate = new System.Windows.Forms.CheckBox();
            this.textBox_Cookie = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox_DBHost
            // 
            this.textBox_DBHost.Location = new System.Drawing.Point(13, 13);
            this.textBox_DBHost.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_DBHost.Name = "textBox_DBHost";
            this.textBox_DBHost.Size = new System.Drawing.Size(107, 27);
            this.textBox_DBHost.TabIndex = 0;
            this.textBox_DBHost.Text = "localhost";
            this.textBox_DBHost.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_DBPort
            // 
            this.textBox_DBPort.Location = new System.Drawing.Point(128, 13);
            this.textBox_DBPort.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_DBPort.Name = "textBox_DBPort";
            this.textBox_DBPort.Size = new System.Drawing.Size(59, 27);
            this.textBox_DBPort.TabIndex = 1;
            this.textBox_DBPort.Text = "3306";
            this.textBox_DBPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_DBUser
            // 
            this.textBox_DBUser.Location = new System.Drawing.Point(195, 13);
            this.textBox_DBUser.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_DBUser.Name = "textBox_DBUser";
            this.textBox_DBUser.Size = new System.Drawing.Size(107, 27);
            this.textBox_DBUser.TabIndex = 2;
            this.textBox_DBUser.Text = "root";
            this.textBox_DBUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_DBPassword
            // 
            this.textBox_DBPassword.Location = new System.Drawing.Point(310, 13);
            this.textBox_DBPassword.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_DBPassword.Name = "textBox_DBPassword";
            this.textBox_DBPassword.PasswordChar = '●';
            this.textBox_DBPassword.Size = new System.Drawing.Size(107, 27);
            this.textBox_DBPassword.TabIndex = 3;
            this.textBox_DBPassword.Text = "root";
            this.textBox_DBPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button_SaveSetting
            // 
            this.button_SaveSetting.Location = new System.Drawing.Point(539, 13);
            this.button_SaveSetting.Name = "button_SaveSetting";
            this.button_SaveSetting.Size = new System.Drawing.Size(115, 27);
            this.button_SaveSetting.TabIndex = 4;
            this.button_SaveSetting.Text = "Save Setting";
            this.button_SaveSetting.UseVisualStyleBackColor = true;
            this.button_SaveSetting.Click += new System.EventHandler(this.button_SaveSetting_Click);
            // 
            // textBox_DBDatabase
            // 
            this.textBox_DBDatabase.Location = new System.Drawing.Point(425, 13);
            this.textBox_DBDatabase.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_DBDatabase.Name = "textBox_DBDatabase";
            this.textBox_DBDatabase.Size = new System.Drawing.Size(107, 27);
            this.textBox_DBDatabase.TabIndex = 5;
            this.textBox_DBDatabase.Text = "ilog";
            this.textBox_DBDatabase.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button_Start
            // 
            this.button_Start.Location = new System.Drawing.Point(310, 47);
            this.button_Start.Name = "button_Start";
            this.button_Start.Size = new System.Drawing.Size(344, 60);
            this.button_Start.TabIndex = 6;
            this.button_Start.Text = "Start";
            this.button_Start.UseVisualStyleBackColor = true;
            this.button_Start.Click += new System.EventHandler(this.button_Start_Click);
            // 
            // textBox_RootPath
            // 
            this.textBox_RootPath.Location = new System.Drawing.Point(12, 80);
            this.textBox_RootPath.Name = "textBox_RootPath";
            this.textBox_RootPath.Size = new System.Drawing.Size(290, 27);
            this.textBox_RootPath.TabIndex = 7;
            // 
            // checkBox_ForceUpdate
            // 
            this.checkBox_ForceUpdate.AutoSize = true;
            this.checkBox_ForceUpdate.Location = new System.Drawing.Point(12, 51);
            this.checkBox_ForceUpdate.Name = "checkBox_ForceUpdate";
            this.checkBox_ForceUpdate.Size = new System.Drawing.Size(114, 23);
            this.checkBox_ForceUpdate.TabIndex = 8;
            this.checkBox_ForceUpdate.Text = "Force Update";
            this.checkBox_ForceUpdate.UseVisualStyleBackColor = true;
            // 
            // textBox_Cookie
            // 
            this.textBox_Cookie.Location = new System.Drawing.Point(15, 113);
            this.textBox_Cookie.Multiline = true;
            this.textBox_Cookie.Name = "textBox_Cookie";
            this.textBox_Cookie.Size = new System.Drawing.Size(639, 69);
            this.textBox_Cookie.TabIndex = 9;
            this.textBox_Cookie.Text = "\r\n";
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 192);
            this.Controls.Add(this.textBox_Cookie);
            this.Controls.Add(this.checkBox_ForceUpdate);
            this.Controls.Add(this.textBox_RootPath);
            this.Controls.Add(this.button_Start);
            this.Controls.Add(this.textBox_DBDatabase);
            this.Controls.Add(this.button_SaveSetting);
            this.Controls.Add(this.textBox_DBPassword);
            this.Controls.Add(this.textBox_DBUser);
            this.Controls.Add(this.textBox_DBPort);
            this.Controls.Add(this.textBox_DBHost);
            this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Form_Main";
            this.Text = "Scraper";
            this.Load += new System.EventHandler(this.Form_Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_DBHost;
        private System.Windows.Forms.TextBox textBox_DBPort;
        private System.Windows.Forms.TextBox textBox_DBUser;
        private System.Windows.Forms.TextBox textBox_DBPassword;
        private System.Windows.Forms.Button button_SaveSetting;
        private System.Windows.Forms.TextBox textBox_DBDatabase;
        private System.Windows.Forms.Button button_Start;
        private System.Windows.Forms.TextBox textBox_RootPath;
        private System.Windows.Forms.CheckBox checkBox_ForceUpdate;
        private System.Windows.Forms.TextBox textBox_Cookie;
    }
}

