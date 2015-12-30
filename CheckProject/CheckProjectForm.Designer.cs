namespace CheckProject
{
    partial class CheckProjectForm
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
            this.curProjText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.deleteBtn = new System.Windows.Forms.Button();
            this.changeBtn = new System.Windows.Forms.Button();
            this.createBtn = new System.Windows.Forms.Button();
            this.projectList = new System.Windows.Forms.ListBox();
            this.applyBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // curProjText
            // 
            this.curProjText.Location = new System.Drawing.Point(112, 31);
            this.curProjText.Name = "curProjText";
            this.curProjText.Size = new System.Drawing.Size(100, 21);
            this.curProjText.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "Current Project";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.curProjText);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.deleteBtn);
            this.groupBox1.Controls.Add(this.changeBtn);
            this.groupBox1.Controls.Add(this.createBtn);
            this.groupBox1.Controls.Add(this.projectList);
            this.groupBox1.Location = new System.Drawing.Point(16, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(284, 238);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Project List";
            // 
            // deleteBtn
            // 
            this.deleteBtn.Location = new System.Drawing.Point(191, 199);
            this.deleteBtn.Name = "deleteBtn";
            this.deleteBtn.Size = new System.Drawing.Size(75, 23);
            this.deleteBtn.TabIndex = 6;
            this.deleteBtn.Text = "Delete";
            this.deleteBtn.UseVisualStyleBackColor = true;
            this.deleteBtn.Click += new System.EventHandler(this.deleteBtn_Click);
            // 
            // changeBtn
            // 
            this.changeBtn.Location = new System.Drawing.Point(105, 199);
            this.changeBtn.Name = "changeBtn";
            this.changeBtn.Size = new System.Drawing.Size(75, 23);
            this.changeBtn.TabIndex = 5;
            this.changeBtn.Text = "Modify";
            this.changeBtn.UseVisualStyleBackColor = true;
            this.changeBtn.Click += new System.EventHandler(this.changeBtn_Click);
            // 
            // createBtn
            // 
            this.createBtn.Location = new System.Drawing.Point(19, 199);
            this.createBtn.Name = "createBtn";
            this.createBtn.Size = new System.Drawing.Size(75, 23);
            this.createBtn.TabIndex = 4;
            this.createBtn.Text = "Create";
            this.createBtn.UseVisualStyleBackColor = true;
            this.createBtn.Click += new System.EventHandler(this.createBtn_Click);
            // 
            // projectList
            // 
            this.projectList.FormattingEnabled = true;
            this.projectList.ItemHeight = 12;
            this.projectList.Location = new System.Drawing.Point(19, 60);
            this.projectList.Name = "projectList";
            this.projectList.Size = new System.Drawing.Size(247, 124);
            this.projectList.TabIndex = 0;
            // 
            // applyBtn
            // 
            this.applyBtn.Location = new System.Drawing.Point(207, 265);
            this.applyBtn.Name = "applyBtn";
            this.applyBtn.Size = new System.Drawing.Size(75, 23);
            this.applyBtn.TabIndex = 5;
            this.applyBtn.Text = "Apply";
            this.applyBtn.UseVisualStyleBackColor = true;
            this.applyBtn.Click += new System.EventHandler(this.applyBtn_Click);
            // 
            // CheckProjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 299);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.applyBtn);
            this.Name = "CheckProjectForm";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox curProjText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button deleteBtn;
        private System.Windows.Forms.Button changeBtn;
        private System.Windows.Forms.Button createBtn;
        private System.Windows.Forms.ListBox projectList;
        private System.Windows.Forms.Button applyBtn;
    }
}

