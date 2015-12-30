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

namespace ModifyProject
{
    public partial class ModifyProjectForm : Form
    {
        private string fileName;
        private string projectName;
        private SetNamespaceTreeView namespaceTree;
        private SetClassTreeView classTree;

        public ModifyProjectForm(string fileName, string projectName)
        {
            InitializeComponent();
            this.fileName = fileName;
            this.projectName = projectName;
            projectText.Text = this.projectName;
            setCategoryCombo();
            setTypeCombo();
            projectText.Enabled = false;
            workAtText.Enabled = false;
            namespaceTree = new SetNamespaceTreeView(namespaceTreeView, fileName, projectName);
        }
        private void setCategoryCombo()
        {
            categoryCombo.Items.Clear();
            categoryCombo.Items.Add("Namespace");
            categoryCombo.Items.Add("Class");
            categoryCombo.Items.Add("Field");
        }
        private void setTypeCombo()
        {
            typeCombo.Items.Clear();
            typeCombo.Items.Add("short");
            typeCombo.Items.Add("int");
            typeCombo.Items.Add("long");
            typeCombo.Items.Add("float");
            typeCombo.Items.Add("double");
            typeCombo.Items.Add("char");
            typeCombo.Items.Add("string");
            typeCombo.Items.Add("bool");
            typeCombo.Enabled = false;
        }
        private void setTreeViewClear()
        {
            namespaceTreeView.Nodes.Clear();
            classTreeView.Nodes.Clear();
        }
        private void resetAll()
        {
            setCategoryCombo();
            setTypeCombo();
            workAtText.Clear();
            nameText.Clear();
            setTreeViewClear();
            namespaceTree = new SetNamespaceTreeView(namespaceTreeView, fileName, projectName);
        }
        private void categoryCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            workAtText.Clear();
            if (categoryCombo.SelectedIndex == 0)
            {
                workAtText.Text = projectName;
                typeCombo.Enabled = false;
                //nameText.Text = categoryCombo.SelectedItem.ToString();
            }
            else if (categoryCombo.SelectedIndex == 1)
            {
                typeCombo.Enabled = false;
                if (namespaceTreeView.SelectedNode != null)
                {
                    workAtText.Text = namespaceTree.getPath();
                }
            }
            else if (categoryCombo.SelectedIndex == 2)
            {
                typeCombo.Enabled = true;
            }
        }
        private void createBtn_Click(object sender, EventArgs e)
        {
            if (categoryCombo.SelectedItem == null)
            {
                MessageBox.Show("Select a Category for this work.");
                return;
            }
            else if (workAtText.Text == "")
            {
                string msg = "";
                if (categoryCombo.SelectedItem != null && categoryCombo.SelectedItem.ToString() == "Class")
                {
                    msg = "Click a Namespace in View for this work.";
                }
                else if (categoryCombo.SelectedItem != null && categoryCombo.SelectedItem.ToString() == "Field")
                {
                    msg = "Click a Class in View for this work.";
                }
                MessageBox.Show(msg);
                return;
            }
            else if (nameText.Text == "")
            {
                MessageBox.Show("Input a name in Name Textbox for this work.");
                return;
            }
            else
            {
                AccessSqlite sql = new AccessSqlite(fileName);
                if (categoryCombo.SelectedItem != null && categoryCombo.SelectedItem.ToString() == "Namespace")
                {
                    int result = sql.insertNamespace(projectName, nameText.Text);
                    if (result == 0)
                    {
                        MessageBox.Show("Succeed in Creating new Namespace.");
                    }
                    else if (result == -1)
                    {
                        MessageBox.Show("The Namespace is duplicated.");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Creating new Namespace failed.");
                        return;
                    }
                }
                else if (categoryCombo.SelectedItem != null && categoryCombo.SelectedItem.ToString() == "Class")
                {
                    int result = sql.insertClass(projectName, namespaceTree.getPath(), nameText.Text);
                    if (result == 0)
                    {
                        MessageBox.Show("Succeed in Creating new Class.");
                    }
                    else if (result == -1)
                    {
                        MessageBox.Show("The Class is duplicated.");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Creating new Class failed.");
                        return;
                    }
                }
                else if (categoryCombo.SelectedItem != null && categoryCombo.SelectedItem.ToString() == "Field")
                {
                    // add field
                }
            }
            resetAll();
        }
        private void deleteBtn_Click(object sender, EventArgs e)
        {
            int result = -2;
            if (categoryCombo.SelectedItem == null)
            {
                MessageBox.Show("Select a Category for this work.");
                return;
            }
            else if (workAtText.Text == "")
            {
                string msg = "";
                if (categoryCombo.SelectedItem != null && categoryCombo.SelectedItem.ToString() == "Class")
                {
                    msg = "Click a Namespace in View for this work.";
                }
                else if (categoryCombo.SelectedItem != null && categoryCombo.SelectedItem.ToString() == "Field")
                {
                    msg = "Click a Class in View for this work.";
                }
                MessageBox.Show(msg);
                return;
            }
            else if (nameText.Text == "")
            {
                MessageBox.Show("Click a Namespace in Namespace TreeView for this work.");
                return;
            }
            else
            {
                if (categoryCombo.SelectedItem != null && categoryCombo.SelectedItem.ToString() == "Namespace")
                {
                    WarningForm warningForm = new WarningForm();
                    warningForm.ShowDialog();
                    if (!warningForm.getCheckOK())
                    {
                        return;
                    }
                    AccessSqlite sql = new AccessSqlite(fileName);
                    result = sql.deleteNamespace(projectName, nameText.Text);
                    if (result == 0)
                    {
                        MessageBox.Show("Succeed in deleting the Namespace.");
                    }
                    else if (result == -1)
                    {
                        MessageBox.Show("The Namespace dose not exist.");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Deleting the Namespace failed.");
                        return;
                    }
                }
                else if (categoryCombo.SelectedItem != null && categoryCombo.SelectedItem.ToString() == "Class")
                {
                    AccessSqlite sql = new AccessSqlite(fileName);
                    result = sql.deleteClass(projectName, workAtText.Text, classTree.getClassName());
                    if (result == 0)
                    {
                        MessageBox.Show("Succeed in deleting the Class.");
                    }
                    else if (result == -1)
                    {
                        MessageBox.Show("The Class dose not exist.");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Deleting the Class failed.");
                        return;
                    }

                }
                else if (categoryCombo.SelectedItem != null && categoryCombo.SelectedItem.ToString() == "Field")
                {
                    // delete field
                }
            }

            resetAll();
        }
        private void changeBtn_Click(object sender, EventArgs e)
        {
            if (categoryCombo.SelectedItem == null)
            {
                MessageBox.Show("Select a Category for this work.");
                return;
            }
            else if (workAtText.Text == "")
            {
                string msg = "";
                if (categoryCombo.SelectedItem != null && categoryCombo.SelectedItem.ToString() == "Namespace")
                {
                    msg = "Click a Namespace in View for this work.";
                }
                else if (categoryCombo.SelectedItem != null && categoryCombo.SelectedItem.ToString() == "Class")
                {
                    msg = "Click a Class in View for this work.";
                }
                else if (categoryCombo.SelectedItem != null && categoryCombo.SelectedItem.ToString() == "Field")
                {
                    msg = "Click a Field in View for this work.";
                }
                MessageBox.Show(msg);
                return;
            }
            else if (nameText.Text == "")
            {
                MessageBox.Show("Input a name in Name Textbox for this work.");
                return;
            }
            else
            {
                if (categoryCombo.SelectedItem != null && categoryCombo.SelectedItem.ToString() == "Namespace")
                {
                    if (namespaceTree.getPath() == nameText.Text)
                    {
                        MessageBox.Show("Input changed name in Name Textbox for this work.");
                        return;
                    }
                    AccessSqlite sql = new AccessSqlite(fileName);
                    int result = sql.changeNamespace(projectName, namespaceTree.getPath(), nameText.Text);
                    if (result == 0)
                    {
                        MessageBox.Show("Succeed in changing the Namespace.");
                    }
                    else if (result == -1)
                    {
                        MessageBox.Show("The Namespace is duplicated.");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Changing the Namespace failed.");
                        return;
                    }
                }
                else if (categoryCombo.SelectedItem != null && categoryCombo.SelectedItem.ToString() == "Class")
                {
                    if (classTree.getClassName() == nameText.Text)
                    {
                        MessageBox.Show("Input changed name in Name Textbox for this work.");
                        return;
                    }
                    AccessSqlite sql = new AccessSqlite(fileName);
                    int result = sql.changeClass(projectName, workAtText.Text, classTree.getClassName(), nameText.Text);
                    if (result == 0)
                    {
                        MessageBox.Show("Succeed in changing the Class.");
                    }
                    else if (result == -1)
                    {
                        MessageBox.Show("The Class is duplicated.");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Changing the Class failed.");
                        return;
                    }
                }
                else if (categoryCombo.SelectedItem != null && categoryCombo.SelectedItem.ToString() == "Field")
                {

                }
            }
            resetAll();
        }
        private void namespaceTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            classTree = new SetClassTreeView(classTreeView, fileName, projectName, namespaceTree.getPath());
            if (categoryCombo.SelectedItem != null && categoryCombo.SelectedItem.ToString() == "Namespace")
            {
                nameText.Text = namespaceTree.getPath();
            }
            else if (categoryCombo.SelectedItem != null && categoryCombo.SelectedItem.ToString() == "Class")
            {
                workAtText.Text = namespaceTree.getPath();
                nameText.Text = "";
            }
            else if (categoryCombo.SelectedItem != null && categoryCombo.SelectedItem.ToString() == "Field")
            {
                nameText.Text = "";
            }
        }
        private void classTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (categoryCombo.SelectedItem != null && categoryCombo.SelectedItem.ToString() == "Class")
            {
                nameText.Text = classTree.getClassName();
            }
            else if (categoryCombo.SelectedItem != null && categoryCombo.SelectedItem.ToString() == "Field")
            {
                workAtText.Text = namespaceTree.getPath() + " / " + classTree.getClassName();
                string selNode = classTree.getFieldName();
                if (selNode != null)
                {
                    nameText.Text = selNode;
                }
                else
                {
                    nameText.Text = "";
                }
            }
        }
    }
}
