namespace FolderProtector
{
    partial class Main_Window
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main_Window));
            this.pageHeader = new AntdUI.PageHeader();
            this.button_FolderPathBrowser = new AntdUI.Button();
            this.label_FolderPath = new AntdUI.Label();
            this.input_FolderPath = new AntdUI.Input();
            this.label_Hide = new AntdUI.Label();
            this.switch_Hide = new AntdUI.Switch();
            this.switch_Lock = new AntdUI.Switch();
            this.label_Lock = new AntdUI.Label();
            this.button_Set = new AntdUI.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // pageHeader
            // 
            this.pageHeader.HandCursor = System.Windows.Forms.Cursors.Default;
            this.pageHeader.Location = new System.Drawing.Point(0, 0);
            this.pageHeader.MaximizeBox = false;
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.ShowButton = true;
            this.pageHeader.ShowIcon = true;
            this.pageHeader.Size = new System.Drawing.Size(483, 30);
            this.pageHeader.TabIndex = 0;
            this.pageHeader.Text = "文件夹保护器";
            // 
            // button_FolderPathBrowser
            // 
            this.button_FolderPathBrowser.HandCursor = System.Windows.Forms.Cursors.Default;
            this.button_FolderPathBrowser.Location = new System.Drawing.Point(372, 36);
            this.button_FolderPathBrowser.Name = "button_FolderPathBrowser";
            this.button_FolderPathBrowser.Size = new System.Drawing.Size(99, 30);
            this.button_FolderPathBrowser.TabIndex = 1;
            this.button_FolderPathBrowser.Text = "浏览...";
            this.button_FolderPathBrowser.Click += new System.EventHandler(this.button_FolderPathBrowser_Click);
            // 
            // label_FolderPath
            // 
            this.label_FolderPath.HandCursor = System.Windows.Forms.Cursors.Default;
            this.label_FolderPath.Location = new System.Drawing.Point(12, 36);
            this.label_FolderPath.Name = "label_FolderPath";
            this.label_FolderPath.Size = new System.Drawing.Size(70, 30);
            this.label_FolderPath.TabIndex = 2;
            this.label_FolderPath.Text = "文件夹路径:";
            // 
            // input_FolderPath
            // 
            this.input_FolderPath.Location = new System.Drawing.Point(88, 36);
            this.input_FolderPath.Name = "input_FolderPath";
            this.input_FolderPath.Size = new System.Drawing.Size(278, 30);
            this.input_FolderPath.TabIndex = 3;
            this.input_FolderPath.TextChanged += new System.EventHandler(this.input_FolderPath_TextChanged);
            this.input_FolderPath.Enter += new System.EventHandler(this.input_FolderPath_Enter);
            this.input_FolderPath.Leave += new System.EventHandler(this.input_FolderPath_Leave);
            // 
            // label_Hide
            // 
            this.label_Hide.Location = new System.Drawing.Point(63, 72);
            this.label_Hide.Name = "label_Hide";
            this.label_Hide.Size = new System.Drawing.Size(75, 23);
            this.label_Hide.TabIndex = 4;
            this.label_Hide.Text = "隐藏文件夹";
            // 
            // switch_Hide
            // 
            this.switch_Hide.HandCursor = System.Windows.Forms.Cursors.Default;
            this.switch_Hide.Location = new System.Drawing.Point(12, 72);
            this.switch_Hide.Name = "switch_Hide";
            this.switch_Hide.Size = new System.Drawing.Size(45, 23);
            this.switch_Hide.TabIndex = 5;
            this.switch_Hide.Text = "switch1";
            // 
            // switch_Lock
            // 
            this.switch_Lock.HandCursor = System.Windows.Forms.Cursors.Default;
            this.switch_Lock.Location = new System.Drawing.Point(12, 101);
            this.switch_Lock.Name = "switch_Lock";
            this.switch_Lock.Size = new System.Drawing.Size(45, 23);
            this.switch_Lock.TabIndex = 9;
            // 
            // label_Lock
            // 
            this.label_Lock.Location = new System.Drawing.Point(63, 101);
            this.label_Lock.Name = "label_Lock";
            this.label_Lock.Size = new System.Drawing.Size(113, 23);
            this.label_Lock.TabIndex = 8;
            this.label_Lock.Text = "锁定文件夹";
            // 
            // button_Set
            // 
            this.button_Set.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Set.HandCursor = System.Windows.Forms.Cursors.Default;
            this.button_Set.Location = new System.Drawing.Point(317, 72);
            this.button_Set.Name = "button_Set";
            this.button_Set.Size = new System.Drawing.Size(154, 54);
            this.button_Set.TabIndex = 10;
            this.button_Set.Text = "应用";
            this.button_Set.Type = AntdUI.TTypeMini.Success;
            this.button_Set.Click += new System.EventHandler(this.button_Set_Click);
            // 
            // Main_Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 131);
            this.Controls.Add(this.button_Set);
            this.Controls.Add(this.switch_Lock);
            this.Controls.Add(this.label_Lock);
            this.Controls.Add(this.switch_Hide);
            this.Controls.Add(this.label_Hide);
            this.Controls.Add(this.input_FolderPath);
            this.Controls.Add(this.label_FolderPath);
            this.Controls.Add(this.button_FolderPathBrowser);
            this.Controls.Add(this.pageHeader);
            this.EnableHitTest = false;
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Main_Window";
            this.Resizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main_Window";
            this.Load += new System.EventHandler(this.Main_Window_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.PageHeader pageHeader;
        private AntdUI.Button button_FolderPathBrowser;
        private AntdUI.Label label_FolderPath;
        private AntdUI.Input input_FolderPath;
        private AntdUI.Label label_Hide;
        private AntdUI.Switch switch_Hide;
        private AntdUI.Switch switch_Lock;
        private AntdUI.Label label_Lock;
        private AntdUI.Button button_Set;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}

