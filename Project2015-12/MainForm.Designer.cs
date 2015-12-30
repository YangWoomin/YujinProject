namespace Project2015_12
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.curProjText = new System.Windows.Forms.TextBox();
            this.modProjBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkProjBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Modify Project";
            // 
            // curProjText
            // 
            this.curProjText.Location = new System.Drawing.Point(133, 24);
            this.curProjText.Name = "curProjText";
            this.curProjText.Size = new System.Drawing.Size(100, 21);
            this.curProjText.TabIndex = 5;
            // 
            // modProjBtn
            // 
            this.modProjBtn.Location = new System.Drawing.Point(151, 21);
            this.modProjBtn.Name = "modProjBtn";
            this.modProjBtn.Size = new System.Drawing.Size(75, 23);
            this.modProjBtn.TabIndex = 2;
            this.modProjBtn.Text = "Go";
            this.modProjBtn.UseVisualStyleBackColor = true;
            this.modProjBtn.Click += new System.EventHandler(this.modProjBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "Current Project";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.modProjBtn);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(8, 116);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(241, 58);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // chkProjBtn
            // 
            this.chkProjBtn.Location = new System.Drawing.Point(151, 22);
            this.chkProjBtn.Name = "chkProjBtn";
            this.chkProjBtn.Size = new System.Drawing.Size(75, 23);
            this.chkProjBtn.TabIndex = 1;
            this.chkProjBtn.Text = "Go";
            this.chkProjBtn.UseVisualStyleBackColor = true;
            this.chkProjBtn.Click += new System.EventHandler(this.selProjBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Check Project";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.curProjText);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Location = new System.Drawing.Point(14, 14);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(257, 233);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkProjBtn);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(241, 58);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.groupBox3);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox curProjText;
        private System.Windows.Forms.Button modProjBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button chkProjBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

