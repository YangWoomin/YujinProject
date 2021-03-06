﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccessDB;
using AccessFile;


namespace CheckProject
{
    public partial class CheckProjectForm : Form
    {
        private string fileName;
        private string projectName;

        public CheckProjectForm()
        {
            InitializeComponent();
            curProjText.Enabled = false;
            fileName = "db.db";
            ManageFile mf = new ManageFile();
            int check = mf.checkDBFile(fileName);
            if (check != -1)
            {
                resetList();
                projectName = mf.getCurrentProject("check.txt");
                if(projectName != null)
                {
                    curProjText.Text = projectName;
                }
            }
        }
        private void resetList()
        {
            projectList.Items.Clear();
            AccessSqlite sql = new AccessSqlite();
            string[] rows = sql.getRows("sqlite_master", "name", "type = 'table'");
            if (rows == null)
                return;
            int i = 0;
            while (rows.Length > i)
            {
                projectList.Items.Add(rows[i]);
                i++;
            }
        }
        public string getFileName()
        {
            return fileName;
        }
        public string getProjectName()
        {
            return projectName;
        }
        private void createBtn_Click(object sender, EventArgs e)
        {
            InputForm inputForm = new InputForm(fileName, 0, null);
            inputForm.ShowDialog();
            resetList();
        }
        private void changeBtn_Click(object sender, EventArgs e)
        {
            if (projectList.SelectedItem == null)
            {
                MessageBox.Show("Select a project in Project List.");
                return;
            }
            string beforeName = projectList.SelectedItem.ToString();
            InputForm inputForm = new InputForm(fileName, 1, beforeName);
            inputForm.ShowDialog();
            resetList();
            int i = 0;
            int flag = 0;
            while (projectList.Items.Count > i)
            {
                if (projectList.Items[i].ToString() == beforeName)
                {
                    flag = 1;
                    break;
                }
                i++;
            }
            if (flag == 0)
            {
                if (beforeName == projectName)
                {
                    projectName = inputForm.getAfterName();
                    curProjText.Text = projectName;
                    ManageFile mf = new ManageFile();
                    mf.setCurrentProject("check.txt", projectName);
                }
            }

        }
        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (projectList.SelectedItem == null)
            {
                MessageBox.Show("Select a project in Project List.");
                return;
            }
            AccessSqlite sql = new AccessSqlite();
            int result = sql.deleteTable(projectList.SelectedItem.ToString());
            if (result == 0)
            {
                if (projectName != null && projectList.SelectedItem.ToString() == projectName)
                {
                    projectName = null;
                    curProjText.Text = null;
                    ManageFile mf = new ManageFile();
                    mf.setCurrentProject("check.txt", null);
                }
                resetList();
                MessageBox.Show("Succeed in deleting the project.");
            }
            else
            {
                MessageBox.Show("Deleting the table failed.");
            }
        }
        private void applyBtn_Click(object sender, EventArgs e)
        {
            if (projectList.SelectedItem == null)
            {
                MessageBox.Show("Select a project in Project List.");
                return;
            }
            projectName = projectList.SelectedItem.ToString();
            curProjText.Text = projectName;
            ManageFile mf = new ManageFile();
            mf.setCurrentProject("check.txt", projectName);
            this.Close();
        }
    }
}
