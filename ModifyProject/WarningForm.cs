using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModifyProject
{
    public partial class WarningForm : Form
    {
        private bool checkOK;
        public WarningForm()
        {
            InitializeComponent();
            checkOK = false;
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            checkOK = true;
            this.Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            checkOK = false;
            this.Close();
        }
        public bool getCheckOK()
        {
            return checkOK;
        }
    }
}
