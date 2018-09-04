
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

// Einführung

namespace DBalls1
{
    public class Form2 : System.Windows.Forms.Form
    {
        #region FormMember Variablen
        private Panel _panel1;
        private Label _label1;
        private System.ComponentModel.IContainer components;
        private TextBox _objTypeEd;
        private CheckBox _creDelChk;
        #endregion
        ArrayList _grObjList = new ArrayList();

        public Form2()
        {
            InitializeComponent();
            GraphicObject.SetDefaultColors(_panel1.BackColor, Color.Red);
            _grObjList.Add(new GRectangle(20, 20, Color.Red));
            _grObjList.Add(new GCircle(50, 50, Color.Blue));
            _grObjList.Add(new GRectangle(100, 100, Color.Green));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code
        private void InitializeComponent()
        {
            this._panel1 = new System.Windows.Forms.Panel();
            this._label1 = new System.Windows.Forms.Label();
            this._objTypeEd = new System.Windows.Forms.TextBox();
            this._creDelChk = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // _panel1
            // 
            this._panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this._panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this._panel1.Location = new System.Drawing.Point(12, 42);
            this._panel1.Name = "_panel1";
            this._panel1.Size = new System.Drawing.Size(530, 398);
            this._panel1.TabIndex = 0;
            this._panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPanelPaint);
            this._panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this._panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
            this._panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            // 
            // _label1
            // 
            this._label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._label1.Location = new System.Drawing.Point(22, 9);
            this._label1.Name = "_label1";
            this._label1.Size = new System.Drawing.Size(78, 17);
            this._label1.TabIndex = 1;
            this._label1.Text = "ObjType:";
            // 
            // _objTypeEd
            // 
            this._objTypeEd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._objTypeEd.Location = new System.Drawing.Point(93, 9);
            this._objTypeEd.Name = "_objTypeEd";
            this._objTypeEd.Size = new System.Drawing.Size(95, 22);
            this._objTypeEd.TabIndex = 2;
            this._objTypeEd.TextChanged += new System.EventHandler(this._objTypeEd_TextChanged);
            // 
            // _creDelChk
            // 
            this._creDelChk.AutoSize = true;
            this._creDelChk.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._creDelChk.Location = new System.Drawing.Point(214, 11);
            this._creDelChk.Name = "_creDelChk";
            this._creDelChk.Size = new System.Drawing.Size(124, 20);
            this._creDelChk.TabIndex = 3;
            this._creDelChk.Text = "Create/Delete";
            this._creDelChk.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(552, 455);
            this.Controls.Add(this._creDelChk);
            this.Controls.Add(this._objTypeEd);
            this.Controls.Add(this._label1);
            this.Controls.Add(this._panel1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        [STAThread]
        static void Main()
        {
            Application.Run(new Form2());
        }

        private void OnPanelPaint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            foreach (GraphicObject obj in _grObjList)
                obj.PaintVisible(gr);
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {

        }

        void OnMouseMove(object sender, MouseEventArgs e)
        {

        }

        void OnMouseUp(object sender, MouseEventArgs e)
        {

        }

        private void _objTypeEd_TextChanged(object sender, EventArgs e)
        {

        }
    }
}



















