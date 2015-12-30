using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccessDB;
using System.IO;

namespace CheckProject
{
    public partial class CheckProjectForm : Form
    {
        private string fileName;
        private string projectName;
        private bool checkApply;

        public CheckProjectForm()
        {
            InitializeComponent();
            curProjText.Enabled = false;
            fileName = "db.db";
            checkApply = false;
            int check = checkDBFile();
            if (check == 0)
            {
                resetList();
            }
            else
            {
                this.Close();
            }
            setCurrentProject();
        }
        private void setCurrentProject()
        {
            try
            {
                FileStream fs = new FileStream("check.txt", FileMode.OpenOrCreate);
                StreamReader sr = new StreamReader(fs);
                projectName = sr.ReadLine();
                checkApply = true;
                sr.Close();
                fs.Close();
            }
            catch (Exception e)
            {
                return;
            }
        }
        public void setCurrentProjectText()
        {
            curProjText.Text = projectName;
        }
        private int checkDBFile()
        {
            AccessSqlite sql = new AccessSqlite(fileName);
            int check = sql.getCheck();
            if (check == -1)
            {
                MessageBox.Show("Unknown exception for creating DB file.");
                return -1;
            }
            return 0;
        }
        private void resetList()
        {
            projectList.Items.Clear();
            AccessSqlite sql = new AccessSqlite(fileName);
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
        public bool getCheckApply()
        {
            return checkApply;
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
            InputForm inputForm = new InputForm(fileName, 1, projectList.SelectedItem.ToString());
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
                    FileStream fs = new FileStream("check.txt", FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                    sw.WriteLine(projectName);
                    sw.Close();
                    fs.Close();
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
            AccessSqlite sql = new AccessSqlite(fileName);
            int result = sql.deleteTable(projectList.SelectedItem.ToString());
            if (result == 0)
            {
                if (projectName != null && projectList.SelectedItem.ToString() == projectName)
                {
                    projectName = null;
                    checkApply = false;
                    curProjText.Text = null;
                    FileStream fs = new FileStream("check.txt", FileMode.Create);
                    fs.Close();
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
            checkApply = true;
            curProjText.Text = projectName;
            FileStream fs = new FileStream("check.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            sw.WriteLine(projectName);
            sw.Close();
            fs.Close();
            this.Close();
        }
    }
}
