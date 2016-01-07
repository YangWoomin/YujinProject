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

namespace ModifyProject
{
    public partial class ModifyProjectForm : Form
    {
        private string fileName;
        private string projectName;
        private string lastClassName;
        private SetNamespaceTreeView namespaceTree;
        private SetClassTreeView classTree;
        private CheckCharacter checkStr;

        public ModifyProjectForm(string fileName, string projectName)
        {
            InitializeComponent();
            this.fileName = fileName;
            this.projectName = projectName;
            projectText.Text = this.projectName;
            setCategoryCombo();
            setTypeSelCombo();
            setWorkCombo();
            projectText.Enabled = false;
            workAtText.Enabled = false;
            namespaceTree = new SetNamespaceTreeView(namespaceTreeView, fileName, projectName);
            checkStr = new CheckCharacter();
        }
        private void setCategoryCombo()
        {
            categoryCombo.Items.Clear();
            categoryCombo.Items.Add("Namespace");
            categoryCombo.Items.Add("Class");
            categoryCombo.Items.Add("Field");
        }
        private void setTypeSelCombo()
        {
            typeSelCombo.Items.Clear();
            typeSelCombo.Items.Add("Primitive");
            typeSelCombo.Items.Add("Object");
            typeSelCombo.Enabled = false;
            typeCombo.Enabled = false;
        }
        private void setWorkCombo()
        {
            workCombo.Items.Clear();
            workCombo.Items.Add("Create");
            workCombo.Items.Add("Delete");
            workCombo.Items.Add("Modify");
        }
        private void categoryCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            workAtText.Clear();
            typeSelCombo.SelectedItem = null;
            typeCombo.SelectedItem = null;
            if (categoryCombo.SelectedIndex == 0)
            {
                typeSelCombo.Enabled = false;
                typeCombo.Enabled = false;
                workAtText.Text = projectName;
                if (namespaceTree.getPath() != null)
                {
                    nameText.Text = namespaceTree.getPath();
                }
                else
                {
                    nameText.Text = null;
                }
            }
            else if (categoryCombo.SelectedIndex == 1)
            {
                typeSelCombo.Enabled = false;
                typeCombo.Enabled = false;
                if (namespaceTree.getPath() != null)
                {
                    workAtText.Text = namespaceTree.getPath();
                    if (classTree.getClassName() != null)
                    {
                        nameText.Text = classTree.getClassName();
                    }
                    else
                    {
                        nameText.Text = null;
                    }
                }
                else
                {
                    workAtText.Text = null;
                    nameText.Text = null;
                }
            }
            else if (categoryCombo.SelectedIndex == 2)
            {
                typeSelCombo.Enabled = true;
                typeCombo.Enabled = true;
                if (namespaceTree.getPath() != null && classTree.getClassName() != null)
                {
                    workAtText.Text = namespaceTree.getPath() + " / " + classTree.getClassName();
                }
                else
                {
                    workAtText.Text = null;
                }
                try {
                    if (classTree.getFieldName() != null)
                    {
                        typeSelCombo.SelectedItem = null;
                        typeCombo.SelectedItem = null;
                        string selNode = classTree.getFieldName();
                        int point = selNode.IndexOf("(");
                        string type = selNode.Substring(point + 1);
                        type = type.Substring(0, type.Length - 1);
                        selNode = selNode.Substring(0, point);
                        nameText.Text = selNode;
                        int flag = 0;
                        int i = 0;
                        while (typeSelCombo.Items.Count > i)
                        {
                            if (typeSelCombo.Items[i].ToString() == type)
                            {
                                flag = 1;
                                typeSelCombo.SelectedIndex = i;
                                break;
                            }
                            i++;
                        }
                        if (flag == 0)
                        {
                            i = 0;
                            while (typeCombo.Items.Count > i)
                            {
                                if (typeCombo.Items[i].ToString() == type)
                                {
                                    typeCombo.SelectedIndex = i;
                                    break;
                                }
                                i++;
                            }
                        }
                    }
                    else
                    {
                        nameText.Text = null;
                    }
                }
                catch(Exception excep)
                {
                    nameText.Text = null;
                }
            }
        }
        private void namespaceTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            classTree = new SetClassTreeView(classTreeView, fileName, projectName, namespaceTree.getPath());
            if(categoryCombo.SelectedItem == null)
            {
                categoryCombo.SelectedIndex = 0;
            }
            else if(categoryCombo.SelectedIndex == 2)
            {
                categoryCombo.SelectedIndex = 0;
            }
            if (categoryCombo.SelectedIndex == 0)
            {
                nameText.Text = namespaceTree.getPath();
            }
            else if (categoryCombo.SelectedIndex == 1)
            {
                workAtText.Text = namespaceTree.getPath();
                nameText.Text = "";
            }
        }
        private void classTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(categoryCombo.SelectedIndex == 0 || categoryCombo.SelectedIndex == 1)
            {
                if(classTree.getFieldName() == null)
                {
                    categoryCombo.SelectedIndex = 1;
                }
                else
                {
                    categoryCombo.SelectedIndex = 2;
                }
            }
            if (categoryCombo.SelectedIndex == 1)
            {
                nameText.Text = classTree.getClassName();
            }
            else if (categoryCombo.SelectedIndex == 2)
            {
                try {
                    typeSelCombo.SelectedItem = null;
                    typeCombo.SelectedItem = null;
                    workAtText.Text = namespaceTree.getPath() + " / " + classTree.getClassName();
                    string nameStr = classTree.getFieldName();
                    if(nameStr == null)
                    {
                        return;
                    }
                    int point = nameStr.IndexOf("(");
                    string type = nameStr.Substring(point + 1);
                    type = type.Substring(0, type.Length - 1);
                    nameStr = nameStr.Substring(0, point);
                    nameText.Text = nameStr;
                    typeSelCombo.SelectedIndex = 0;
                    typeSelCombo_SelectionChangeCommitted(null, null);
                    int i = 0;
                    while(typeCombo.Items.Count > i)
                    {
                        if(typeCombo.Items[i].ToString() == type)
                        {
                            typeCombo.SelectedIndex = i;
                            return;
                        }
                        i++;
                    }
                    typeSelCombo.SelectedIndex = 1;
                    typeSelCombo_SelectionChangeCommitted(null, null);
                    i = 0;
                    while (typeCombo.Items.Count > i)
                    {
                        if (typeCombo.Items[i].ToString() == type)
                        {
                            typeCombo.SelectedIndex = i;
                            break;
                        }
                        i++;
                    }
                }
                catch(Exception excep)
                {
                    nameText.Text = null;
                    return;
                }
            }
        }
        private void namespaceTreeView_Leave(object sender, EventArgs e)
        {
            if (namespaceTreeView.SelectedNode != null)
            {
                namespaceTreeView.SelectedNode.BackColor = Color.Orange;
                namespaceTreeView.SelectedNode.ForeColor = Color.White;
            }
        }
        private void namespaceTreeView_Enter(object sender, EventArgs e)
        {
            if(namespaceTreeView.SelectedNode != null)
            {
                namespaceTreeView.SelectedNode.BackColor = Color.White;
                namespaceTreeView.SelectedNode.ForeColor = Color.Black;
            }
        }
        private void typeSelCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            typeCombo.Items.Clear();
            if (typeSelCombo.SelectedIndex == 0)
            {
                typeCombo.Items.Add("short");
                typeCombo.Items.Add("int");
                typeCombo.Items.Add("long");
                typeCombo.Items.Add("float");
                typeCombo.Items.Add("double");
                typeCombo.Items.Add("char");
                typeCombo.Items.Add("bool");

            }
            else if (typeSelCombo.SelectedIndex == 1)
            {
                typeCombo.Items.Add("string");
            }
        }
        private void typeCombo_Click(object sender, EventArgs e)
        {
            if(typeSelCombo.SelectedItem == null)
            {
                MessageBox.Show("Select a type of type");
            }
        }
        private void nameText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                doBtn_Click(null, null);
            }
        }
        private void doBtn_Click(object sender, EventArgs e)
        {
            AccessSqlite sql = new AccessSqlite(fileName);
            int result = -2;
            if (categoryCombo.SelectedItem == null)
            {
                MessageBox.Show("Select a Category for this work.");
                return;
            }
            else if (workAtText.Text == "")
            {
                string msg = "";
                if (categoryCombo.SelectedItem.ToString() == "Class")
                {
                    msg = "Click a Namespace in View for this work.";
                }
                else if (categoryCombo.SelectedItem.ToString() == "Field")
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
            else if(checkStr.checkNamespace(nameText.Text) != 0)
            {
                return;
            }
            else if(workCombo.SelectedItem == null)
            {
                MessageBox.Show("Select a work in Work Combo box for this work.");
                return;
            }
            else if (categoryCombo.SelectedItem.ToString() == "Namespace" && workCombo.SelectedItem.ToString() == "Create")
            {
                result = sql.insertNamespace(projectName, nameText.Text);
                if (result == 0)
                {
                    namespaceTree = new SetNamespaceTreeView(namespaceTreeView, fileName, projectName);
                    namespaceTree.expandNode(nameText.Text);
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
            else if (categoryCombo.SelectedItem.ToString() == "Class" && workCombo.SelectedItem.ToString() == "Create")
            {
                result = sql.insertClass(projectName, namespaceTree.getPath(), nameText.Text);
                if (result == 0)
                {
                    classTree = new SetClassTreeView(classTreeView, fileName, projectName, namespaceTree.getPath());
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
            else if (categoryCombo.SelectedItem.ToString() == "Field" && workCombo.SelectedItem.ToString() == "Create")
            {
                if (classTree.getClassName() != null)
                {
                    lastClassName = classTree.getClassName();
                }
                string type = null;
                if (typeCombo.SelectedItem == null)
                {
                    MessageBox.Show("Select a field type for this work.");
                    return;
                }
                else
                {
                    type = typeCombo.SelectedItem.ToString();
                }
                result = sql.insertField(projectName, namespaceTree.getPath(), lastClassName, nameText.Text, type);
                if (result == 0)
                {
                    classTree = new SetClassTreeView(classTreeView, fileName, projectName, namespaceTree.getPath());
                    classTree.expandNode(lastClassName);
                }
                else if (result == -1)
                {
                    MessageBox.Show("The Field is duplicated.");
                    return;
                }
                else
                {
                    MessageBox.Show("Creating new Field failed.");
                    return;
                }
            }
            else if (categoryCombo.SelectedItem.ToString() == "Namespace" && workCombo.SelectedItem.ToString() == "Delete")
            {
                WarningForm warningForm = new WarningForm();
                warningForm.ShowDialog();
                if (!warningForm.getCheckOK())
                {
                    return;
                }
                result = sql.deleteNamespace(projectName, nameText.Text);
                if (result == 0)
                {
                    namespaceTree = new SetNamespaceTreeView(namespaceTreeView, fileName, projectName);
                    namespaceTree.expandNode(nameText.Text);
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
            else if (categoryCombo.SelectedItem.ToString() == "Class" && workCombo.SelectedItem.ToString() == "Delete")
            {
                result = sql.deleteClass(projectName, workAtText.Text, nameText.Text);
                if (result == 0)
                {
                    classTree = new SetClassTreeView(classTreeView, fileName, projectName, namespaceTree.getPath());
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
            else if (categoryCombo.SelectedItem.ToString() == "Field" && workCombo.SelectedItem.ToString() == "Delete")
            {
                if (classTree.getClassName() != null)
                {
                    lastClassName = classTree.getClassName();
                }
                result = sql.deleteField(projectName, namespaceTree.getPath(), lastClassName, nameText.Text);
                if (result == 0)
                {
                    classTree = new SetClassTreeView(classTreeView, fileName, projectName, namespaceTree.getPath());
                    classTree.expandNode(lastClassName);
                }
                else if (result == -1)
                {
                    MessageBox.Show("The Field dose not exist.");
                    return;
                }
                else
                {
                    MessageBox.Show("Deleting the Field failed.");
                    return;
                }
            }
            else if (categoryCombo.SelectedItem.ToString() == "Namespace" && workCombo.SelectedItem.ToString() == "Modify")
            {
                if (namespaceTree.getPath() == null)
                {
                    MessageBox.Show("Click a namespace in View for this work.");
                    return;
                }
                else if (namespaceTree.getPath() == nameText.Text)
                {
                    MessageBox.Show("Input changed name in Name Textbox for this work.");
                    return;
                }
                result = sql.changeNamespace(projectName, namespaceTree.getPath(), nameText.Text);
                if (result == 0)
                {
                    namespaceTree = new SetNamespaceTreeView(namespaceTreeView, fileName, projectName);
                    nameText.Text = null;
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
            else if (categoryCombo.SelectedItem.ToString() == "Class" && workCombo.SelectedItem.ToString() == "Modify")
            {
                if (classTree.getClassName() == null)
                {
                    MessageBox.Show("Click a class in View for this work.");
                    return;
                }
                else if (classTree.getClassName() == nameText.Text)
                {
                    MessageBox.Show("Input changed name in Name Textbox for this work.");
                    return;
                }
                result = sql.changeClass(projectName, workAtText.Text, classTree.getClassName(), nameText.Text);
                if (result == 0)
                {
                    classTree = new SetClassTreeView(classTreeView, fileName, projectName, namespaceTree.getPath());
                    nameText.Text = null;
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
            else if (categoryCombo.SelectedItem.ToString() == "Field" && workCombo.SelectedItem.ToString() == "Modify")
            {
                if (classTree.getClassName() != null)
                {
                    lastClassName = classTree.getClassName();
                }
                if (classTree.getFieldName() == null)
                {
                    MessageBox.Show("Click a Field in View for this work.");
                    return;
                }
                else if (classTree.getFieldName() == nameText.Text)
                {
                    MessageBox.Show("Input changed name in Name Textbox for this work.");
                    return;
                }
                else if (typeCombo.SelectedItem == null)
                {
                    MessageBox.Show("Select a field type in Type for this work.");
                    return;
                }
                result = sql.changeField(projectName, namespaceTree.getPath(), classTree.getClassName(), classTree.getFieldName(), nameText.Text, typeCombo.SelectedItem.ToString());
                if (result == 0)
                {
                    classTree = new SetClassTreeView(classTreeView, fileName, projectName, namespaceTree.getPath());
                    classTree.expandNode(lastClassName);
                    nameText.Text = null;
                }
                else if (result == -1)
                {
                    MessageBox.Show("The Field is duplicated.");
                    return;
                }
                else if (result == -3)
                {
                    MessageBox.Show("Input changed value.");
                    return;
                }
                else
                {
                    MessageBox.Show("Changing the Field failed.");
                    return;
                }
            }
            nameText.Focus();
        }
        private void classTreeView_Enter(object sender, EventArgs e)
        {
            if(classTreeView.SelectedNode != null)
            {
                classTreeView.SelectedNode.BackColor = Color.White;
                classTreeView.SelectedNode.ForeColor = Color.Black;
            }
        }
        private void classTreeView_Leave(object sender, EventArgs e)
        {
            if(classTreeView.SelectedNode != null)
            {
                classTreeView.SelectedNode.BackColor = Color.Orange;
                classTreeView.SelectedNode.ForeColor = Color.White;
            }
        }
    }
}
