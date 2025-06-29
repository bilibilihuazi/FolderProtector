namespace FolderProtector
{
    partial class Password_Window
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
            this.button_Forget = new AntdUI.Button();
            this.button_OK = new AntdUI.Button();
            this.input_Password = new AntdUI.Input();
            this.label2 = new AntdUI.Label();
            this.label1 = new AntdUI.Label();
            this.SuspendLayout();
            // 
            // button_Forget
            // 
            this.button_Forget.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Forget.HandCursor = System.Windows.Forms.Cursors.Default;
            this.button_Forget.Location = new System.Drawing.Point(140, 132);
            this.button_Forget.Name = "button_Forget";
            this.button_Forget.Size = new System.Drawing.Size(220, 39);
            this.button_Forget.TabIndex = 9;
            this.button_Forget.Text = "忘记密钥";
            this.button_Forget.Type = AntdUI.TTypeMini.Primary;
            this.button_Forget.Visible = false;
            this.button_Forget.Click += new System.EventHandler(this.button_Forget_Click);
            // 
            // button_OK
            // 
            this.button_OK.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_OK.HandCursor = System.Windows.Forms.Cursors.Default;
            this.button_OK.Location = new System.Drawing.Point(140, 177);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(220, 58);
            this.button_OK.TabIndex = 8;
            this.button_OK.Text = "确定";
            this.button_OK.Type = AntdUI.TTypeMini.Success;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // input_Password
            // 
            this.input_Password.Location = new System.Drawing.Point(12, 88);
            this.input_Password.Name = "input_Password";
            this.input_Password.Size = new System.Drawing.Size(481, 38);
            this.input_Password.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(12, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(481, 21);
            this.label2.TabIndex = 6;
            this.label2.Text = "为保证您的文件夹安全，请您设置安全密钥，以保证只有您可以打开此工具";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(481, 43);
            this.label1.TabIndex = 5;
            this.label1.Text = "欢迎！";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Password_Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 247);
            this.Controls.Add(this.button_Forget);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.input_Password);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.EnableHitTest = false;
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Password_Window";
            this.Resizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Password_Window_FormClosing);
            this.Load += new System.EventHandler(this.Password_Window_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.Button button_Forget;
        private AntdUI.Button button_OK;
        private AntdUI.Input input_Password;
        private AntdUI.Label label2;
        private AntdUI.Label label1;
    }
}