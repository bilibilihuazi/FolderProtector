namespace FolderProtector
{
    partial class Forget_Window
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
            this.label1 = new AntdUI.Label();
            this.input = new AntdUI.Input();
            this.button_Done = new AntdUI.Button();
            this.button_Cancel = new AntdUI.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 44);
            this.label1.TabIndex = 0;
            this.label1.Text = "请输入您的密钥恢复码";
            // 
            // input
            // 
            this.input.HandCursor = System.Windows.Forms.Cursors.Default;
            this.input.Location = new System.Drawing.Point(12, 62);
            this.input.Name = "input";
            this.input.Size = new System.Drawing.Size(540, 44);
            this.input.TabIndex = 1;
            // 
            // button_Done
            // 
            this.button_Done.HandCursor = System.Windows.Forms.Cursors.Default;
            this.button_Done.Location = new System.Drawing.Point(425, 112);
            this.button_Done.Name = "button_Done";
            this.button_Done.Size = new System.Drawing.Size(127, 33);
            this.button_Done.TabIndex = 2;
            this.button_Done.Text = "下一步";
            this.button_Done.Type = AntdUI.TTypeMini.Success;
            this.button_Done.Click += new System.EventHandler(this.button_Done_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.HandCursor = System.Windows.Forms.Cursors.Default;
            this.button_Cancel.Location = new System.Drawing.Point(292, 112);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(127, 33);
            this.button_Cancel.TabIndex = 3;
            this.button_Cancel.Text = "取消";
            this.button_Cancel.Type = AntdUI.TTypeMini.Error;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // Forget_Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 157);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_Done);
            this.Controls.Add(this.input);
            this.Controls.Add(this.label1);
            this.EnableHitTest = false;
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Forget_Window";
            this.Resizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Forget_Window";
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.Label label1;
        private AntdUI.Input input;
        private AntdUI.Button button_Done;
        private AntdUI.Button button_Cancel;
    }
}