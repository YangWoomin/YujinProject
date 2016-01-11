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
            this.typeSelCombo = new System.Windows.Forms.ComboBox();
            this.workAtText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.projectText = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.categoryCombo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DoBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.workCombo = new System.Windows.Forms.ComboBox();
            this.typeCombo = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.nameText = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // classTreeView
            // 
            this.classTreeView.Location = new System.Drawing.Point(282, 74);
            this.classTreeView.Name = "classTreeView";
            this.classTreeView.Size = new System.Drawing.Size(222, 207);
            this.classTreeView.TabIndex = 4;
            this.classTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.classTreeView_AfterSelect);
            this.classTreeView.Enter += new System.EventHandler(this.classTreeView_Enter);
            this.classTreeView.Leave += new System.EventHandler(this.classTreeView_Leave);
            // 
            // namespaceTreeView
            // 
            this.namespaceTreeView.Location = new System.Drawing.Point(21, 74);
            this.namespaceTreeView.Name = "namespaceTreeView";
            this.namespaceTreeView.Size = new System.Drawing.Size(222, 207);
            this.namespaceTreeView.TabIndex = 0;
            this.namespaceTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.namespaceTreeView_AfterSelect);
            this.namespaceTreeView.Enter += new System.EventHandler(this.namespaceTreeView_Enter);
            this.namespaceTreeView.Leave += new System.EventHandler(this.namespaceTreeView_Leave);
            // 
            // typeSelCombo
            // 
            this.typeSelCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeSelCombo.FormattingEnabled = true;
            this.typeSelCombo.Location = new System.Drawing.Point(326, 65);
            this.typeSelCombo.Name = "typeSelCombo";
            this.typeSelCombo.Size = new System.Drawing.Size(77, 20);
            this.typeSelCombo.TabIndex = 8;
            this.typeSelCombo.SelectionChangeCommitted += new System.EventHandler(this.typeSelCombo_SelectionChangeCommitted);
            // 
            // workAtText
            // 
            this.workAtText.Location = new System.Drawing.Point(307, 30);
            this.workAtText.Name = "workAtText";
            this.workAtText.Size = new System.Drawing.Size(179, 21);
            this.workAtText.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(255, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "Work at";
            // 
            // projectText
            // 
            this.projectText.Location = new System.Drawing.Point(116, 25);
            this.projectText.Name = "projectText";
            this.projectText.Size = new System.Drawing.Size(100, 21);
            this.projectText.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(280, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "Class";
            // 
            // categoryCombo
            // 
            this.categoryCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.categoryCombo.FormattingEnabled = true;
            this.categoryCombo.Location = new System.Drawing.Point(100, 30);
            this.categoryCombo.Name = "categoryCombo";
            this.categoryCombo.Size = new System.Drawing.Size(121, 20);
            this.categoryCombo.TabIndex = 5;
            this.categoryCombo.SelectedIndexChanged += new System.EventHandler(this.categoryCombo_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "Namespace";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 34);
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
            this.groupBox2.Location = new System.Drawing.Point(12, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(524, 308);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "View";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current Project";
            // 
            // DoBtn
            // 
            this.DoBtn.Location = new System.Drawing.Point(409, 99);
            this.DoBtn.Name = "DoBtn";
            this.DoBtn.Size = new System.Drawing.Size(77, 23);
            this.DoBtn.TabIndex = 3;
            this.DoBtn.Text = "Do";
            this.DoBtn.UseVisualStyleBackColor = true;
            this.DoBtn.Click += new System.EventHandler(this.doBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.workCombo);
            this.groupBox1.Controls.Add(this.typeCombo);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.typeSelCombo);
            this.groupBox1.Controls.Add(this.workAtText);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.categoryCombo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.DoBtn);
            this.groupBox1.Controls.Add(this.nameLabel);
            this.groupBox1.Controls.Add(this.nameText);
            this.groupBox1.Location = new System.Drawing.Point(12, 322);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(524, 140);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Workspace";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(255, 104);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 12);
            this.label7.TabIndex = 13;
            this.label7.Text = "Work";
            // 
            // workCombo
            // 
            this.workCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.workCombo.FormattingEnabled = true;
            this.workCombo.Location = new System.Drawing.Point(293, 100);
            this.workCombo.Name = "workCombo";
            this.workCombo.Size = new System.Drawing.Size(110, 20);
            this.workCombo.TabIndex = 12;
            // 
            // typeCombo
            // 
            this.typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeCombo.FormattingEnabled = true;
            this.typeCombo.Location = new System.Drawing.Point(410, 65);
            this.typeCombo.Name = "typeCombo";
            this.typeCombo.Size = new System.Drawing.Size(77, 20);
            this.typeCombo.TabIndex = 11;
            this.typeCombo.Click += new System.EventHandler(this.typeCombo_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(255, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "Field Type";
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(55, 69);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(39, 12);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "Name";
            // 
            // nameText
            // 
            this.nameText.Location = new System.Drawing.Point(100, 65);
            this.nameText.Name = "nameText";
            this.nameText.Size = new System.Drawing.Size(121, 21);
            this.nameText.TabIndex = 1;
            this.nameText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nameText_KeyPress);
            // 
            // ModifyProjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 472);
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
        private System.Windows.Forms.ComboBox typeSelCombo;
        private System.Windows.Forms.TextBox workAtText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox projectText;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox categoryCombo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button DoBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox nameText;
        private System.Windows.Forms.ComboBox typeCombo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox workCombo;
        private System.Windows.Forms.Label label7;
    }
}

