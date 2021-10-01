
namespace GenshinAutoFish
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timerCapture = new System.Windows.Forms.Timer(this.components);
            this.nudFrameRate = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.rtbConsole = new System.Windows.Forms.RichTextBox();
            this.chkTopMost = new System.Windows.Forms.CheckBox();
            this.btnSwitch = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.chkDisplayDetectForm = new System.Windows.Forms.CheckBox();
            this.chkAutoPullUp = new System.Windows.Forms.CheckBox();
            this.btnResetArea = new System.Windows.Forms.Button();
            this.chkAlwaysHideArea = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFrameRate)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(12, 117);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(492, 75);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // timerCapture
            // 
            this.timerCapture.Tick += new System.EventHandler(this.timerCapture_Tick);
            // 
            // nudFrameRate
            // 
            this.nudFrameRate.Location = new System.Drawing.Point(98, 21);
            this.nudFrameRate.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nudFrameRate.Name = "nudFrameRate";
            this.nudFrameRate.Size = new System.Drawing.Size(90, 28);
            this.nudFrameRate.TabIndex = 3;
            this.nudFrameRate.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "刷新帧率";
            // 
            // rtbConsole
            // 
            this.rtbConsole.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbConsole.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbConsole.Location = new System.Drawing.Point(12, 198);
            this.rtbConsole.Name = "rtbConsole";
            this.rtbConsole.Size = new System.Drawing.Size(492, 246);
            this.rtbConsole.TabIndex = 7;
            this.rtbConsole.Text = "";
            // 
            // chkTopMost
            // 
            this.chkTopMost.AutoSize = true;
            this.chkTopMost.Location = new System.Drawing.Point(124, 61);
            this.chkTopMost.Name = "chkTopMost";
            this.chkTopMost.Size = new System.Drawing.Size(106, 22);
            this.chkTopMost.TabIndex = 9;
            this.chkTopMost.Text = "置顶界面";
            this.chkTopMost.UseVisualStyleBackColor = true;
            this.chkTopMost.CheckedChanged += new System.EventHandler(this.chkTopMost_CheckedChanged);
            // 
            // btnSwitch
            // 
            this.btnSwitch.Location = new System.Drawing.Point(13, 459);
            this.btnSwitch.Name = "btnSwitch";
            this.btnSwitch.Size = new System.Drawing.Size(226, 45);
            this.btnSwitch.TabIndex = 10;
            this.btnSwitch.Text = "启动自动钓鱼(F11)";
            this.btnSwitch.UseVisualStyleBackColor = true;
            this.btnSwitch.Click += new System.EventHandler(this.btnSwitch_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(221, 23);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(269, 18);
            this.linkLabel1.TabIndex = 11;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "开源地址-遇到问题也在这边提问";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // chkDisplayDetectForm
            // 
            this.chkDisplayDetectForm.AutoSize = true;
            this.chkDisplayDetectForm.Location = new System.Drawing.Point(236, 61);
            this.chkDisplayDetectForm.Name = "chkDisplayDetectForm";
            this.chkDisplayDetectForm.Size = new System.Drawing.Size(205, 22);
            this.chkDisplayDetectForm.TabIndex = 12;
            this.chkDisplayDetectForm.Text = "显示识别窗口(Debug)";
            this.chkDisplayDetectForm.UseVisualStyleBackColor = true;
            // 
            // chkAutoPullUp
            // 
            this.chkAutoPullUp.AutoSize = true;
            this.chkAutoPullUp.Checked = true;
            this.chkAutoPullUp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoPullUp.Location = new System.Drawing.Point(12, 61);
            this.chkAutoPullUp.Name = "chkAutoPullUp";
            this.chkAutoPullUp.Size = new System.Drawing.Size(106, 22);
            this.chkAutoPullUp.TabIndex = 13;
            this.chkAutoPullUp.Text = "自动提杆";
            this.chkAutoPullUp.UseVisualStyleBackColor = true;
            // 
            // btnResetArea
            // 
            this.btnResetArea.Location = new System.Drawing.Point(245, 459);
            this.btnResetArea.Name = "btnResetArea";
            this.btnResetArea.Size = new System.Drawing.Size(167, 45);
            this.btnResetArea.TabIndex = 14;
            this.btnResetArea.Text = "重置识别框位置";
            this.btnResetArea.UseVisualStyleBackColor = true;
            this.btnResetArea.Click += new System.EventHandler(this.btnResetArea_Click);
            // 
            // chkAlwaysHideArea
            // 
            this.chkAlwaysHideArea.AutoSize = true;
            this.chkAlwaysHideArea.Location = new System.Drawing.Point(12, 89);
            this.chkAlwaysHideArea.Name = "chkAlwaysHideArea";
            this.chkAlwaysHideArea.Size = new System.Drawing.Size(160, 22);
            this.chkAlwaysHideArea.TabIndex = 15;
            this.chkAlwaysHideArea.Text = "一直隐藏识别框";
            this.chkAlwaysHideArea.UseVisualStyleBackColor = true;
            this.chkAlwaysHideArea.CheckedChanged += new System.EventHandler(this.chkAlwaysHideArea_CheckedChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 515);
            this.Controls.Add(this.chkAlwaysHideArea);
            this.Controls.Add(this.btnResetArea);
            this.Controls.Add(this.chkAutoPullUp);
            this.Controls.Add(this.chkDisplayDetectForm);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.btnSwitch);
            this.Controls.Add(this.chkTopMost);
            this.Controls.Add(this.rtbConsole);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudFrameRate);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "原神自动钓鱼机";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFrameRate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timerCapture;
        private System.Windows.Forms.NumericUpDown nudFrameRate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtbConsole;
        private System.Windows.Forms.CheckBox chkTopMost;
        private System.Windows.Forms.Button btnSwitch;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.CheckBox chkDisplayDetectForm;
        private System.Windows.Forms.CheckBox chkAutoPullUp;
        private System.Windows.Forms.Button btnResetArea;
        private System.Windows.Forms.CheckBox chkAlwaysHideArea;
    }
}

