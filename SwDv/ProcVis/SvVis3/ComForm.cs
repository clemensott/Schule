using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace vis1
{
    public partial class ComForm : Form
    {
        public string COM { get; set; }

        public ComForm()
        {
            InitializeComponent();
        }

        public ComForm(string com)
        {
            InitializeComponent();

            COM = com;
        }

        private void ComForm_Load(object sender, EventArgs e)
        {
            tbxCom.Text = COM;
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            COM = tbxCom.Text;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            COM = tbxCom.Text;

            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
