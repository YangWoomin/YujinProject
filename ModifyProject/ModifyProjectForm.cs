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
        private CheckCharacter checkStr;

        public ModifyProjectForm(string fileName, string projectName)
        {
            InitializeComponent();
            this.fileName = fileName;
            this.projectName = projectName;
            projectText.Text = this.projectName;
            setCategoryCombo();
            setPrimitiveCombo();
            setObjectCombo();
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
        private void setPrimitiveCombo()
        {
            primitiveCombo.Items.Clear();
            primitiveCombo.Items.Add("short");
            primitiveCombo.Items.Add("int");
            primitiveCombo.Items.Add("long");
            primitiveCombo.Items.Add("float");
            primitiveCombo.Items.Add("double");
            primitiveCombo.Items.Add("char");
            primitiveCombo.Items.Add("bool");
            primitiveCombo.Enabled = false;
        }
        private void setObjectCombo()
        {
            objectCombo.Items.Clear();
            objectCombo.Items.Add("string");
            objectCombo.Enabled = false;
        }
        private void setTreeViewClear()
        {
            namespaceTreeView.Nodes.Clear();
            classTreeView.Nodes.Clear();
        }
        private void resetAll()
        {
            setCategoryCombo();
            setPrimitiveCombo();
            setObjectCombo();
            workAtText.Clear();
            nameText.Clear();
            setTreeViewClear();
            namespaceTree = new SetNamespaceTreeView(namespaceTreeView, fileName, projectName);
        }
        private void categoryCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            workAtText.Clear();
            primitiveCombo.SelectedItem = null;
            objectCombo.SelectedItem = null;
            if (categoryCombo.SelectedIndex == 0)
            {
                primitiveCombo.Enabled = false;
                objectCombo.Enabled = false;
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
                primitiveCombo.Enabled = false;
                objectCombo.Enabled = false;
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
                primitiveCombo.Enabled = true;
                objectCombo.Enabled = true;
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
                        primitiveCombo.SelectedItem = null;
                        objectCombo.SelectedItem = null;
                        string selNode = classTree.getFieldName();
                        int point = selNode.IndexOf("(");
                        string type = selNode.Substring(point + 1);
                        type = type.Substring(0, type.Length - 1);
                        selNode = selNode.Substring(0, point);
                        nameText.Text = selNode;
                        int flag = 0;
                        int i = 0;
                        while (primitiveCombo.Items.Count > i)
                        {
                            if (primitiveCombo.Items[i].ToString() == type)
                            {
                                flag = 1;
                                primitiveCombo.SelectedIndex = i;
                                break;
                            }
                            i++;
                        }
                        if (flag == 0)
                        {
                            i = 0;
                            while (objectCombo.Items.Count > i)
                            {
                                if (objectCombo.Items[i].ToString() == type)
                                {
                                    objectCombo.SelectedIndex = i;
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
        private void createBtn_Click(object sender, EventArgs e)
        {
            AccessSqlite sql = new AccessSqlite(fileName);
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
            else if (categoryCombo.SelectedItem.ToString() == "Namespace" && checkStr.checkNamespace(nameText.Text) == 0)
            {
                int result = sql.insertNamespace(projectName, nameText.Text);
                if (result == 0)
                {
                    MessageBox.Show("Succeed in Creating new Namespace.");
                    resetAll();
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
            else if (categoryCombo.SelectedItem.ToString() == "Class" && checkStr.checkString(nameText.Text) == 0)
            {
                int result = sql.insertClass(projectName, namespaceTree.getPath(), nameText.Text);
                if (result == 0)
                {
                    MessageBox.Show("Succeed in Creating new Class.");
                    resetAll();
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
            else if (categoryCombo.SelectedItem.ToString() == "Field" && checkStr.checkString(nameText.Text) == 0)
            {
                string type = null;
                if(primitiveCombo.SelectedItem == null && objectCombo.SelectedItem == null)
                {
                    MessageBox.Show("Select a field type for this work.");
                    return;
                }
                else
                {
                    if(primitiveCombo.SelectedItem != null)
                    {
                        type = primitiveCombo.SelectedItem.ToString();
                    }
                    else if(objectCombo.SelectedItem != null)
                    {
                        type = objectCombo.SelectedItem.ToString();
                    }
                }
                int result = sql.insertField(projectName, namespaceTree.getPath(), classTree.getClassName(), nameText.Text, type);
                if (result == 0)
                {
                    MessageBox.Show("Succeed in Creating new Field.");
                    resetAll();
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
        }
        private void deleteBtn_Click(object sender, EventArgs e)
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
            else if (categoryCombo.SelectedItem.ToString() == "Namespace" && checkStr.checkNamespace(nameText.Text) == 0)
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
                    MessageBox.Show("Succeed in deleting the Namespace.");
                    resetAll();
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
            else if (categoryCombo.SelectedItem.ToString() == "Class" && checkStr.checkString(nameText.Text) == 0)
            {
                result = sql.deleteClass(projectName, workAtText.Text, classTree.getClassName());
                if (result == 0)
                {
                    MessageBox.Show("Succeed in deleting the Class.");
                    resetAll();
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
            else if (categoryCombo.SelectedItem.ToString() == "Field" && checkStr.checkString(nameText.Text) == 0)
            {
                result = sql.deleteField(projectName, namespaceTree.getPath(), classTree.getClassName(), nameText.Text);
                if (result == 0)
                {
                    MessageBox.Show("Succeed in deleting the Field.");
                    resetAll();
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
        }
        private void changeBtn_Click(object sender, EventArgs e)
        {
            AccessSqlite sql = new AccessSqlite(fileName);
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
            else if (categoryCombo.SelectedItem.ToString() == "Namespace" && checkStr.checkNamespace(nameText.Text) == 0)
            {
                if (namespaceTree.getPath() == nameText.Text)
                {
                    MessageBox.Show("Input changed name in Name Textbox for this work.");
                    return;
                }
                
                int result = sql.changeNamespace(projectName, namespaceTree.getPath(), nameText.Text);
                if (result == 0)
                {
                    MessageBox.Show("Succeed in changing the Namespace.");
                    resetAll();
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
            else if (categoryCombo.SelectedItem.ToString() == "Class" && checkStr.checkString(nameText.Text) == 0)
            {
                if (classTree.getClassName() == nameText.Text)
                {
                    MessageBox.Show("Input changed name in Name Textbox for this work.");
                    return;
                }
                int result = sql.changeClass(projectName, workAtText.Text, classTree.getClassName(), nameText.Text);
                if (result == 0)
                {
                    MessageBox.Show("Succeed in changing the Class.");
                    resetAll();
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
            else if (categoryCombo.SelectedItem.ToString() == "Field" && checkStr.checkString(nameText.Text) == 0)
            {
                if(classTree.getFieldName() == null)
                {
                    MessageBox.Show("Click a Field in View for this work.");
                    return;
                }
                int result = sql.changeField(projectName, namespaceTree.getPath(), classTree.getClassName(), classTree.getFieldName(), nameText.Text, (primitiveCombo.SelectedItem == null ? objectCombo.SelectedItem.ToString() : primitiveCombo.SelectedItem.ToString()));
                if (result == 0)
                {
                    MessageBox.Show("Succeed in changing the Field.");
                    resetAll();
                }
                else if (result == -1)
                {
                    MessageBox.Show("The Field is duplicated.");
                    return;
                }
                else if(result == -3)
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
                    primitiveCombo.SelectedItem = null;
                    objectCombo.SelectedItem = null;
                    workAtText.Text = namespaceTree.getPath() + " / " + classTree.getClassName();
                    string selNode = classTree.getFieldName();
                    int point = selNode.IndexOf("(");
                    string type = selNode.Substring(point + 1);
                    type = type.Substring(0, type.Length - 1);
                    selNode = selNode.Substring(0, point);
                    nameText.Text = selNode;
                    int flag = 0;
                    int i = 0;
                    while(primitiveCombo.Items.Count > i)
                    {
                        if(primitiveCombo.Items[i].ToString() == type)
                        {
                            flag = 1;
                            primitiveCombo.SelectedIndex = i;
                            break;
                        }
                        i++;
                    }
                    if(flag == 0)
                    {
                        i = 0;
                        while(objectCombo.Items.Count > i)
                        {
                            if(objectCombo.Items[i].ToString() == type)
                            {
                                objectCombo.SelectedIndex = i;
                                break;
                            }
                            i++;
                        }
                    }
                }
                catch(Exception excep)
                {
                    nameText.Text = null;
                    return;
                }
            }
        }
        private void objectCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            primitiveCombo.SelectedItem = null;
        }
        private void primitiveCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            objectCombo.SelectedItem = null;
        }
    }
}
