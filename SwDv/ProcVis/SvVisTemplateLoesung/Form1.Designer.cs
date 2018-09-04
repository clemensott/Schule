
namespace vis1
{
  partial class Form1
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.label1 = new System.Windows.Forms.Label();
      this.m_SendEd = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.m_Disp1 = new System.Windows.Forms.Label();
      this.m_Disp2 = new System.Windows.Forms.Label();
      this.m_Disp3 = new System.Windows.Forms.Label();
      this.m_Timer1 = new System.Windows.Forms.Timer(this.components);
      this.m_uCSendChk = new System.Windows.Forms.CheckBox();
      this._Lb1 = new System.Windows.Forms.ListBox();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.menueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.clearMessagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.button1 = new System.Windows.Forms.Button();
      this.menuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(12, 27);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(48, 16);
      this.label1.TabIndex = 0;
      this.label1.Text = "Send:";
      // 
      // m_SendEd
      // 
      this.m_SendEd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_SendEd.Location = new System.Drawing.Point(69, 27);
      this.m_SendEd.Name = "m_SendEd";
      this.m_SendEd.Size = new System.Drawing.Size(87, 22);
      this.m_SendEd.TabIndex = 1;
      this.m_SendEd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnSendEditKeyDown);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(14, 110);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(123, 16);
      this.label2.TabIndex = 2;
      this.label2.Text = "Variable Values:";
      // 
      // m_Disp1
      // 
      this.m_Disp1.AutoSize = true;
      this.m_Disp1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_Disp1.Location = new System.Drawing.Point(143, 110);
      this.m_Disp1.Name = "m_Disp1";
      this.m_Disp1.Size = new System.Drawing.Size(26, 16);
      this.m_Disp1.TabIndex = 3;
      this.m_Disp1.Text = "V1";
      // 
      // m_Disp2
      // 
      this.m_Disp2.AutoSize = true;
      this.m_Disp2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_Disp2.Location = new System.Drawing.Point(223, 110);
      this.m_Disp2.Name = "m_Disp2";
      this.m_Disp2.Size = new System.Drawing.Size(26, 16);
      this.m_Disp2.TabIndex = 4;
      this.m_Disp2.Text = "V2";
      // 
      // m_Disp3
      // 
      this.m_Disp3.AutoSize = true;
      this.m_Disp3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_Disp3.Location = new System.Drawing.Point(298, 110);
      this.m_Disp3.Name = "m_Disp3";
      this.m_Disp3.Size = new System.Drawing.Size(26, 16);
      this.m_Disp3.TabIndex = 5;
      this.m_Disp3.Text = "V3";
      // 
      // m_Timer1
      // 
      this.m_Timer1.Tick += new System.EventHandler(this.OnTimer);
      // 
      // m_uCSendChk
      // 
      this.m_uCSendChk.AutoSize = true;
      this.m_uCSendChk.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_uCSendChk.Location = new System.Drawing.Point(182, 48);
      this.m_uCSendChk.Name = "m_uCSendChk";
      this.m_uCSendChk.Size = new System.Drawing.Size(164, 20);
      this.m_uCSendChk.TabIndex = 8;
      this.m_uCSendChk.Text = "uC sending ON/OFF";
      this.m_uCSendChk.UseVisualStyleBackColor = true;
      this.m_uCSendChk.Click += new System.EventHandler(this.OnUCSendChk);
      // 
      // _Lb1
      // 
      this._Lb1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this._Lb1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._Lb1.FormattingEnabled = true;
      this._Lb1.ItemHeight = 16;
      this._Lb1.Location = new System.Drawing.Point(15, 146);
      this._Lb1.Name = "_Lb1";
      this._Lb1.Size = new System.Drawing.Size(344, 148);
      this._Lb1.TabIndex = 9;
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menueToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(371, 25);
      this.menuStrip1.TabIndex = 10;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // menueToolStripMenuItem
      // 
      this.menueToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearMessagesToolStripMenuItem});
      this.menueToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.menueToolStripMenuItem.Name = "menueToolStripMenuItem";
      this.menueToolStripMenuItem.Size = new System.Drawing.Size(62, 21);
      this.menueToolStripMenuItem.Text = "Menue";
      // 
      // clearMessagesToolStripMenuItem
      // 
      this.clearMessagesToolStripMenuItem.Name = "clearMessagesToolStripMenuItem";
      this.clearMessagesToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
      this.clearMessagesToolStripMenuItem.Text = "Clear Messages";
      this.clearMessagesToolStripMenuItem.Click += new System.EventHandler(this.OnMenueClearMessages);
      // 
      // button1
      // 
      this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.button1.Location = new System.Drawing.Point(18, 66);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(151, 23);
      this.button1.TabIndex = 11;
      this.button1.Text = "Send LED Data";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.OnLedButton);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(371, 312);
      this.Controls.Add(this.button1);
      this.Controls.Add(this._Lb1);
      this.Controls.Add(this.m_uCSendChk);
      this.Controls.Add(this.m_Disp3);
      this.Controls.Add(this.m_Disp2);
      this.Controls.Add(this.m_Disp1);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.m_SendEd);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.menuStrip1);
      this.Name = "Form1";
      this.Text = "R/W Variables via SerPort";
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox m_SendEd;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label m_Disp1;
    private System.Windows.Forms.Label m_Disp2;
    private System.Windows.Forms.Label m_Disp3;
    private System.Windows.Forms.Timer m_Timer1;
    private System.Windows.Forms.CheckBox m_uCSendChk;
    private System.Windows.Forms.ListBox _Lb1;
    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem menueToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem clearMessagesToolStripMenuItem;
    private System.Windows.Forms.Button button1;
  }
}

