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

namespace CheckProject
{
    public partial class InputForm : Form
    {
        private int work;
        private string fileName;
        private string beforeName;
        private string afterName;
        private CheckCharacter checkStr;

        public InputForm(string fileName, int work, string beforeName)
        {
            InitializeComponent();
            this.fileName = fileName;
            this.work = work;
            if (beforeName != null)
            {
                this.beforeName = beforeName;
            }
            if (work == 0)
            {
                this.Text = "Create Project";
            }
            else
            {
                this.Text = "Modify Project";
            }
            checkStr = new CheckCharacter();
        }
        public string getAfterName()
        {
            return afterName;
        }
        private void OKBtn_Click(object sender, EventArgs e)
        {
            if (work == 0)
            {
                if (inputText.Text == "")
                {
                    MessageBox.Show("Input a name in Name Textbox for this work.");
                    return;
                }
                else if(checkStr.checkString(inputText.Text) != 0)
                {
                    return;
                }
                else
                {
                    AccessSqlite sql = new AccessSqlite(fileName);
                    int result = sql.createTable(inputText.Text, null);
                    if (result == 0)
                    {
                        this.Close();
                    }
                    else if (result == -1)
                    {
                        MessageBox.Show("The project name is duplicated.");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Creating the table failed.");
                        return;
                    }
                }
            }
            else
            {
                if (inputText.Text == "")
                {
                    MessageBox.Show("Input a name in Name Textbox for this work.");
                    return;
                }
                else if (checkStr.checkString(inputText.Text) != 0)
                {
                    return;
                }
                else
                {
                    AccessSqlite sql = new AccessSqlite(fileName);
                    int result = sql.changeTable(beforeName, inputText.Text);
                    if (result == 0)
                    {
                        afterName = inputText.Text;
                        this.Close();
                    }
                    else if (result == -1)
                    {
                        MessageBox.Show("The project name is duplicated.");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Changing the table failed.");
                        return;
                    }
                }
            }
        }
    }
}
