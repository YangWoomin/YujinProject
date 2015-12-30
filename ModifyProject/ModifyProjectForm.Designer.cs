namespace ModifyProject
{
    partial class ModifyProjectForm
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
            this.classTreeView = new System.Windows.Forms.TreeView();
            this.namespaceTreeView = new System.Windows.Forms.TreeView();
            this.typeCombo = new System.Windows.Forms.ComboBox();
            this.workAtText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.projectText = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.categoryCombo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.changeBtn = new System.Windows.Forms.Button();
            this.deleteBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.createBtn = new System.Windows.Forms.Button();
            this.nameText = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // classTreeView
            // 
            this.classTreeView.Location = new System.Drawing.Point(256, 74);
            this.classTreeView.Name = "classTreeView";
            this.classTreeView.Size = new System.Drawing.Size(200, 207);
            this.classTreeView.TabIndex = 4;
            this.classTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.classTreeView_AfterSelect);
            // 
            // namespaceTreeView
            // 
            this.namespaceTreeView.Location = new System.Drawing.Point(31, 74);
            this.namespaceTreeView.Name = "namespaceTreeView";
            this.namespaceTreeView.Size = new System.Drawing.Size(195, 207);
            this.namespaceTreeView.TabIndex = 0;
            this.namespaceTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.namespaceTreeView_AfterSelect);
            // 
            // typeCombo
            // 
            this.typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeCombo.FormattingEnabled = true;
            this.typeCombo.Location = new System.Drawing.Point(199, 65);
            this.typeCombo.Name = "typeCombo";
            this.typeCombo.Size = new System.Drawing.Size(66, 20);
            this.typeCombo.TabIndex = 8;
            // 
            // workAtText
            // 
            this.workAtText.Location = new System.Drawing.Point(306, 26);
            this.workAtText.Name = "workAtText";
            this.workAtText.Size = new System.Drawing.Size(150, 21);
            this.workAtText.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(254, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "Work at";
            // 
            // projectText
            // 
            this.projectText.Location = new System.Drawing.Point(126, 26);
            this.projectText.Name = "projectText";
            this.projectText.Size = new System.Drawing.Size(100, 21);
            this.projectText.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(254, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "Class";
            // 
            // categoryCombo
            // 
            this.categoryCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.categoryCombo.FormattingEnabled = true;
            this.categoryCombo.Location = new System.Drawing.Point(91, 26);
            this.categoryCombo.Name = "categoryCombo";
            this.categoryCombo.Size = new System.Drawing.Size(121, 20);
            this.categoryCombo.TabIndex = 5;
            this.categoryCombo.SelectedIndexChanged += new System.EventHandler(this.categoryCombo_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "Namespace";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Category";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.projectText);
            this.groupBox2.Controls.Add(this.classTreeView);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.namespaceTreeView);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(17, 11);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(484, 308);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "View";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current Project";
            // 
            // changeBtn
            // 
            this.changeBtn.Location = new System.Drawing.Point(334, 64);
            this.changeBtn.Name = "changeBtn";
            this.changeBtn.Size = new System.Drawing.Size(57, 23);
            this.changeBtn.TabIndex = 4;
            this.changeBtn.Text = "Modify";
            this.changeBtn.UseVisualStyleBackColor = true;
            this.changeBtn.Click += new System.EventHandler(this.changeBtn_Click);
            // 
            // deleteBtn
            // 
            this.deleteBtn.Location = new System.Drawing.Point(397, 64);
            this.deleteBtn.Name = "deleteBtn";
            this.deleteBtn.Size = new System.Drawing.Size(57, 23);
            this.deleteBtn.TabIndex = 3;
            this.deleteBtn.Text = "Delete";
            this.deleteBtn.UseVisualStyleBackColor = true;
            this.deleteBtn.Click += new System.EventHandler(this.deleteBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.typeCombo);
            this.groupBox1.Controls.Add(this.workAtText);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.categoryCombo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.changeBtn);
            this.groupBox1.Controls.Add(this.deleteBtn);
            this.groupBox1.Controls.Add(this.nameLabel);
            this.groupBox1.Controls.Add(this.createBtn);
            this.groupBox1.Controls.Add(this.nameText);
            this.groupBox1.Location = new System.Drawing.Point(17, 325);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(484, 106);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Workspace";
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(46, 69);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(39, 12);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "Name";
            // 
            // createBtn
            // 
            this.createBtn.Location = new System.Drawing.Point(271, 64);
            this.createBtn.Name = "createBtn";
            this.createBtn.Size = new System.Drawing.Size(57, 23);
            this.createBtn.TabIndex = 2;
            this.createBtn.Text = "Create";
            this.createBtn.UseVisualStyleBackColor = true;
            this.createBtn.Click += new System.EventHandler(this.createBtn_Click);
            // 
            // nameText
            // 
            this.nameText.Location = new System.Drawing.Point(91, 65);
            this.nameText.Name = "nameText";
            this.nameText.Size = new System.Drawing.Size(102, 21);
            this.nameText.TabIndex = 1;
            // 
            // ModifyProjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 443);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ModifyProjectForm";
            this.Text = "Form1";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView classTreeView;
        private System.Windows.Forms.TreeView namespaceTreeView;
        private System.Windows.Forms.ComboBox typeCombo;
        private System.Windows.Forms.TextBox workAtText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox projectText;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox categoryCombo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button changeBtn;
        private System.Windows.Forms.Button deleteBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Button createBtn;
        private System.Windows.Forms.TextBox nameText;
    }
}

