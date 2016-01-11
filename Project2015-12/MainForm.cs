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
using AccessFile;
using CheckProject;
using ModifyProject;

namespace Project2015_12
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        private CheckProjectForm checkProjectForm;
        private ModifyProjectForm modifyProjectForm;

        public MainForm()
        {
            InitializeComponent();
            curProjText.Enabled = false;
            checkProjectForm = new CheckProjectForm();
            if (checkProjectForm.getProjectName() != null)
            {
                curProjText.Text = checkProjectForm.getProjectName();
            }
        }
        private void selProjBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            checkProjectForm.ShowDialog();
            this.Show();
            if (checkProjectForm.getProjectName() != null)
            {
                curProjText.Text = checkProjectForm.getProjectName();
            }
            else
            {
                curProjText.Text = null;
            }
        }
        private void modProjBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkProjectForm.getProjectName() != null)
                {
                    modifyProjectForm = new ModifyProjectForm(checkProjectForm.getFileName(), checkProjectForm.getProjectName());
                    this.Hide();
                    modifyProjectForm.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Check a project.");
                    return;
                }
            }
            catch (Exception excep)
            {
                MessageBox.Show("Check a project.");
                return;
            }
        }
    }
}
